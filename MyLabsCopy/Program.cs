using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using MyLabs.Lab4;
using MyLabs.Lab4.Management;
using MyLabs.Lab2;
using MyLabs.Lab4.Structure;
using MyLabsCopy.Lab4.Structure;
using MyLabsCopy.Lab5;
using MyLabsCopy.Lab5.Serializer;
using MyLabsCopy.Lab6;
using MyLabsCopy.Lab6.Account;
using MyLabsCopy.Lab6.Client;
using MyLabsCopy.Lab6.Requests;

namespace MyLabsCopy
{
    class Program
    {
        public static bool check = false;
        static void Main(string[] args)
        {

            checkLab6();


        }

        static void checkLab2()
        {
            Catalog catalog = new Catalog();

            Genre g1 = new Genre("Rock");
            Genre g2 = new Genre("Rapcore", g1);
            Genre g3 = new Genre("Trash Metal", g1);
            Genre g4 = new Genre("Math Metal", g3);

            Artist a1 = new Artist("Linkin Park", Genre.FindGenre("Rapcore"));
            Artist a2 = new Artist("Metallica", Genre.FindGenre("Trash Metal"));
            Artist a3 = new Artist("AC/DC", Genre.FindGenre("Rock"));
            Artist a4 = new Artist("The Beatles", Genre.FindGenre("Rock"));
            Artist a5 = new Artist("Meshuggah", Genre.FindGenre("Math Metal"));

            List<Artist> a_list = new List<Artist>();
            a_list.Add(a1);
            a_list.Add(a2);
            a_list.Add(a3);
            a_list.Add(a4);
            a_list.Add(a5);

            Album col1 = a1.AddAlbum("Best Hits", 2000);
            col1.AddSong("In The End");
            col1.AddSong("New Divide");
            col1.AddSong("Faint");
            col1.AddSong("Guilty All The Same");
            col1.AddSong("A Good Song");

            SongList list = new SongList();
            list.AddSong(a1, "Fade To Black", 2000);
            list.AddSong(a2, "Back In Black", 2000);
            list.AddSong(a3, "Crawling", 2005);
            Collection col2 = Collection.MakeCollection(catalog, "Best Hits", Genre.FindGenre("Rock"), 2000, list);

            Album col3 = a2.AddAlbum("Best Hits", 2003);
            col3.AddSong("Enter Sandman");
            col3.AddSong("One");
            col3.AddSong("A Good Song");
            col3.AddSong("Master Of Puppets");
            col3.AddSong("Mama Said");

            MyLabs.Lab2.Single col4 = a5.AddSingle("Bleed", 2003);
            MyLabs.Lab2.Single col5 = a5.AddSingle("A Good Song", 2005);

            foreach (Artist artist in a_list)
            {
                catalog.AddArtist(artist);
            }


            catalog.Find();
        }

        static void checkLab4()
        {
            ShopService logic = new ShopService();
            logic.InitializeDAO("dao2.ini");

            Shop shop1 = logic.AddShop("Пятерочка");
            Shop shop2 = logic.AddShop("Заря");
            Shop shop3 = logic.AddShop("Юнион");

            AProduct prod1 = logic.AddProduct("Шоколадка", "Аленка");
            AProduct prod2 = logic.AddProduct("Молоко", "Простоквашино");
            AProduct prod3 = logic.AddProduct("Колбаса", "Докторская");

            logic.SetProductPrice(shop2, prod2, 60);
            logic.SetProductPrice(shop3, prod2, 55);
            logic.SetProductPrice(shop1, prod1, 40);

            logic.SupplyShop(shop1, prod1, 20);
            logic.SupplyShop(shop3, prod2, 15);

            logic.SetProductPrice(shop3, prod1, 45);
            logic.SetProductPrice(shop1, prod2, 60);

            logic.SupplyShop(shop1, prod2, 10);
            logic.SupplyShop(shop3, prod1, 20);

            var pair = logic.FindCheapest(prod1);
            Console.WriteLine("{0}, {1}, {2}", pair.First.Name, pair.Second.Name, pair.Second.Price.ToString());
            pair = logic.FindCheapest(prod2);
            Console.WriteLine("{0}, {1}, {2}", pair.First.Name, pair.Second.Name, pair.Second.Price.ToString());

            List<AProduct> list = new List<AProduct>();
            list.Add(prod1);
            list.Add(prod2);
            List<int> amounts = new List<int>();
            amounts.Add(15);
            amounts.Add(5);
            check = true;
            logic.BuyProduct(shop1, list, amounts);

            list.RemoveAt(0);
            amounts.RemoveAt(0);
            Console.WriteLine("This {0}", logic.FindCheapestAmount(list, amounts).First.Name);
            logic.SupplyShop(shop1, prod2, 20);
            amounts[0] = 20;
            Console.WriteLine("This {0}", logic.FindCheapestAmount(list, amounts).First.Name);

            DataBase db = logic.GetData();
            logic.InitializeDAO("dao1.ini", db);
        }

        static void checkFileDataBase()
        {
            ShopService logic = new ShopService();
            logic.InitializeDAO("dao.ini");

            Shop shop1 = logic.AddShop("Пятерочка");
            Shop shop2 = logic.AddShop("Заря");
            Shop shop3 = logic.AddShop("Юнион");

            AProduct prod1 = logic.AddProduct("Шоколадка", "Аленка");
            AProduct prod2 = logic.AddProduct("Молоко", "Простоквашино");
            AProduct prod3 = logic.AddProduct("Колбаса", "Докторская");

            logic.SetProductPrice(shop2, prod2, 60);
            logic.SetProductPrice(shop3, prod2, 55);
            logic.SetProductPrice(shop1, prod1, 40);

            logic.SupplyShop(shop1, prod1, 20);
            logic.SupplyShop(shop3, prod2, 15);

            logic.SetProductPrice(shop3, prod1, 45);
            logic.SetProductPrice(shop1, prod2, 60);

            logic.SupplyShop(shop1, prod2, 10);
            logic.SupplyShop(shop3, prod1, 20);

            var pair = logic.FindCheapest(prod1);
            Console.WriteLine("{0}, {1}, {2}", pair.First.Name, pair.Second.Name, pair.Second.Price.ToString());
            pair = logic.FindCheapest(prod2);
            Console.WriteLine("{0}, {1}, {2}", pair.First.Name, pair.Second.Name, pair.Second.Price.ToString());

            List<AProduct> list = new List<AProduct>();
            list.Add(prod1);
            list.Add(prod2);
            List<int> amounts = new List<int>();
            amounts.Add(15);
            amounts.Add(5);
            check = true;
            logic.BuyProduct(shop1, list, amounts);

            list.RemoveAt(0);
            amounts.RemoveAt(0);
            Console.WriteLine("This {0}", logic.FindCheapestAmount(list, amounts).First.Name);
            logic.SupplyShop(shop1, prod2, 20);
            amounts[0] = 20;
            Console.WriteLine("This {0}", logic.FindCheapestAmount(list, amounts).First.Name);
        }

        static void checkRelativeDataBase()
        {

        }

        static void checkLab5()
        {
            

            Triangle tr = new Triangle(new Point(0, 2, 3),
                new Point(2, 0, 6),
                new Point(3, 6, 0));

            Console.WriteLine("xml");
            var xmlserializer = new XMLSerializer<Triangle>();
            var stream = File.OpenWrite("Triangles.xml");
            xmlserializer.Serialize(tr, stream);
            stream.Close();
            Triangle tr2 = xmlserializer.Deserialize(File.OpenRead("Triangles.xml"));
            Console.WriteLine(tr2);

            Console.WriteLine("binary");
            var binaryserializer = new BinarySerializer<Triangle>();
            stream = File.OpenWrite("Triangles.bin");
            binaryserializer.Serialize(tr, stream);
            stream.Close();
            Triangle tr3 = binaryserializer.Deserialize(File.OpenRead("Triangles.bin"));
            Console.WriteLine(tr3);

            Console.WriteLine("db");
            string connection_string = "Data Source=ZVERDVD-EKLQ97V\\SQLEXPRESS;Initial Catalog=Lab5;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connection_string);
            TriangleDB db = new TriangleDB(conn);
            db.AddTriangle(tr);
            var triangles = db.GetAllTriangles();
            foreach (Triangle triangle in triangles)
            {
                Console.WriteLine(triangle);
            }

        }

        static void checkLab6()
        {
            Client client1 = new ClientBuilder("Denis", "Fevralev")
                .Address("Serebrystiy Pr., 9k1")
                .Passport(12341234).Build();
            Client client2 = new ClientBuilder("Iraklyi", "Ubilava").Build();
            client1.AddAccount(new AccountFabric().CreateAccount(0.04));
            IAccount account = client1.Accounts[0];
            account.Replenishment(100);
            PercentRequest.GetInterest((CurrentAccount)account);
            Console.WriteLine(account.GetBalance());
        }
    }
}
