using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace MyLabsCopy.Lab5
{
    class TriangleDB
    {
        private SqlConnection conn { get; }

        public TriangleDB(SqlConnection conn)
        {
            this.conn = conn;
        }

        public List<Triangle> GetAllTriangles()
        {
            string selected = "Po1.X, Po1.Y, Po1.Z, Po2.X, Po2.Y, Po2.Z, Po3.X, Po3.Y, Po3.Z";
            string query = $"SELECT {selected} FROM dbo.Triangles As Tr "
                           + " JOIN dbo.Point As Po1 ON Tr.Point1Id = Po1.Id "
                           + " JOIN dbo.Point As Po2 ON Tr.Point2Id = Po2.Id "
                           + "  JOIN dbo.Point As Po3 ON Tr.Point3Id = Po3.Id ";
            conn.Open();
            SqlCommand comm = new SqlCommand(query, conn);
            List<Triangle> result = new List<Triangle>();
            using (var reader = comm.ExecuteReader())
            {
                while (reader.Read())
                {
                    double x, y, z;

                    x = reader.GetFloat(0);
                    y = reader.GetFloat(1);
                    z = reader.GetFloat(2);
                    Point a = new Point(x, y, z);

                    x = reader.GetFloat(3);
                    y = reader.GetFloat(4);
                    z = reader.GetFloat(5);
                    Point b = new Point(x, y, z);

                    x = reader.GetFloat(6);
                    y = reader.GetFloat(7);
                    z = reader.GetFloat(8);
                    Point c = new Point(x, y, z);

                    result.Add(new Triangle(a, b, c));
                }
            }

            conn.Close();
            return result;
        }

        private int GetLastId(string table)
        {
            string query = $"SELECT COUNT(*) As Value FROM {table}";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                int value = reader.GetInt32(0);

                conn.Close();
                return value;
            }
        }

        private void AddPoint(Point point, int id)
        {
            string query = $"INSERT INTO [dbo].[Point] VALUES ({id}, {point.x}, {point.y}, {point.z}) ";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void AddTriangle(int tr_id, int po1_id)
        {
            string query = $"INSERT INTO [dbo].[Triangles] VALUES ({tr_id}, {po1_id}, {po1_id + 1}, {po1_id + 2}) ";
            conn.Open();
            SqlCommand command = new SqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void AddTriangle(Triangle triangle)
        {
            int tr_id = GetLastId("dbo.Triangles") + 1;
            int po1_id = GetLastId("dbo.Point") + 1;

            AddPoint(triangle.a, po1_id);
            AddPoint(triangle.b, po1_id + 1);
            AddPoint(triangle.c, po1_id + 2);

            AddTriangle(tr_id, po1_id);
        }

    }
}
