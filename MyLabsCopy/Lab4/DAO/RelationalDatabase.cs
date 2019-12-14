using System;
using System.Collections.Generic;
using System.Text;
using MyLabs.Lab4.Structure;
using MyLabs.Lab4.Management;
using MyLabs.Lab4.DAO;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.IO;
using MyLabs.Lab4.Structure.UpdateQuery;
using MyLabsCopy.Lab4.Structure;

namespace MyLabs.Lab4.DAO
{
    class RelationalDatabase : IDAO
    {
        private SqlConnection connection;
        private string shops_table;
        private string products_table;
        private string shops_products_table;
        private string init_mode;

        public RelationalDatabase(SqlConnection connetion, string shops_table, string prod_table, string shop_prod_table, string init_mode, DataBase db = null)
        {
            this.connection = connetion;
            this.shops_table = shops_table;
            this.products_table = prod_table;
            this.shops_products_table = shop_prod_table;
            this.init_mode = init_mode;

            try
            {
                if (db != null)
                {
                    ImportData(db);
                }
                else
                {
                    InitDataBase();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void InitDataBase()
        {
            InitTable(shops_table, "[Id] [int] NOT NULL, [Name] [nvarchar](50) NOT NULL");
            InitTable(products_table,
                "[Id] [int] NOT NULL, [AccountType][nvarchar](50) NOT NULL, [Name][nvarchar](50) NOT NULL");
            InitTable(shops_products_table,
                "[ShopId] [int] NOT NULL, [ProductId][int] NOT NULL, [Amount][int] NOT NULL, [Price][real] NOT NULL");
        }

        public bool InitTable(string table, string columns)
        {
            string action = "PRINT(0)";
            if (init_mode == "DeleteOld")
                action = $"DELETE FROM {table} WHERE 1 = 1 ";

            string query =
                $"IF Object_ID('{table}', 'U') IS NOT NULL " +
                $" BEGIN {action} END ELSE " +
                $" BEGIN CREATE TABLE {table}( {columns} ) ON [PRIMARY] END";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }


        private int GetShopsLastId()
        {
            string query = $"SELECT COUNT(*) As Value FROM {shops_table}";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                int value = reader.GetInt32(0);

                connection.Close();
                return value;
            }

        }

        private int GetProductsLastId()
        {
            string query = $"SELECT COUNT(*) As Value FROM {products_table}";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                int value = reader.GetInt32(0);

                connection.Close();
                return value;
            }
        }
        
        private int GetProductID(AProduct product)
        {
            string query = $"SELECT {products_table}.Id FROM {products_table} WHERE " +
                           $"{products_table}.AccountType = '{product.Type}' AND {products_table}.Name = '{product.Name}'";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    connection.Close();

                    return -1;
                }

                reader.Read();
                int id = reader.GetInt32(0);

                connection.Close();
                return id;
            }
        }

        private bool ExecuteNonQuery(string query)
        {
           
            return true;
        }

        private SqlDataReader ExecuteQuery(string query)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }

        private bool DoInsert(string table, string values)
        {

            try
            {
                string query = $"INSERT INTO {table} VALUES {values}";
                return ExecuteNonQuery(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        // ввести condition - обощение field и value 
        private bool DoDelete(string table, string field, string value)
        {
            try
            {
                string query = $"DELETE FROM {table} WHERE {field} = {value}";
                return ExecuteNonQuery(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private SqlDataReader DoSelect(string table, string field, string value)
        {
            try
            {
                string query = $"SELECT * FROM {table} WHERE {field} = {value}";
                return ExecuteQuery(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public Shop AddShop(string name)
        {
            try
            {
                List<string> names = new List<string>();
                names.Add(name);
                var shops = AddShop(names);
                return shops[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public List<Shop> AddShop(List<string> names)
        {
            try
            {
                int id = GetShopsLastId();
                string values = "";
                List<Shop> shops = new List<Shop>();
                for (int i = 0; i < names.Count - 1; i++)
                {
                    string name = names[i];
                    ++id;
                    values += $"({id},'{name}'), ";
                    shops.Add(new Shop(name, id));
                }
                ++id;
                string last = names[names.Count - 1];
                values += $"({id}, '{last}')";
                shops.Add(new Shop(names[names.Count - 1], id));
                DoInsert(shops_table, values);
                return shops;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public bool DelShop(Shop shop)
        {
            // можно по id быстрее искать
            return DelShop(shop.Name);
        }

        public bool DelShop(string name)
        {
            try
            {
                // можно быстрее через id
                return DoDelete(shops_table, $"{shops_table}.Name", $"'{name}'");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public Shop GetShop(string name)
        {
            try
            {
                using (var reader = DoSelect(shops_table, $"{shops_table}.Name", $"'{name}'"))
                {
                    if (!reader.HasRows)
                    {
                        connection.Close();

                        return null;
                    }
                    reader.Read();
                    int shop_id = reader.GetInt32(0);
                    string shop_name = reader.GetString(1);
                    connection.Close();
                    return new Shop(shop_name, shop_id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public List<Shop> GetShop()
        {
            List<Shop> result = new List<Shop>();
            using (var reader =DoSelect(shops_table, "0", "0"))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    result.Add(new Shop(name, id));
                }
            }
            connection.Close();
            return result;
        }

        public AProduct AddProduct(AProduct product)
        {
            return AddProduct(product.Type, product.Name);
        }


        public AProduct AddProduct(string type, string name)
        {
            try
            {
                int id = GetProductsLastId();
                ++id;
                string values = $"({id}, '{type}', '{name}')";
                DoInsert(products_table, values);
                return new AProduct(type, name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public bool DelProduct(AProduct product)
        {
            try
            {
                // можно быстрее через id\ тут вообще нужен еще и accountType для однозначности
                return DoDelete(products_table, $"{products_table}.Name", $"'{product.Name}'");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public List<AProduct> GetProduct()
        {
            List<AProduct> result = new List<AProduct>();
            using (var reader = DoSelect(products_table, "0", "0"))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string type = reader.GetString(1);
                    string name = reader.GetString(2);

                    result.Add(new AProduct(type, name));
                }
            }
            connection.Close();

            return result;
        }
        public Product GetProduct(Shop shop, string type, string name)
        {
            return GetProduct(shop.Name, type, name);
        }

        public Product GetProduct(string shop, string type, string name)
        {
            try
            {
                int shop_id = GetShop(shop).Id;
                string selected =
                    $"{products_table}.Id, {products_table}.AccountType, {products_table}.Name, {shops_products_table}.Amount, {shops_products_table}.Price";
                string query = $"SELECT {selected} FROM {products_table} JOIN {shops_products_table} ON " +
                               $"{products_table}.Name = '{name}' AND {shops_products_table}.ProductId = {products_table}.Id " +
                               $" AND {shops_products_table}.ShopId = {shop_id} ";

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        connection.Close();

                        return null;
                    }
                    reader.Read();
                    int prod_id = reader.GetInt32(0);
                    string prod_type = reader.GetString(1);
                    string prod_name = reader.GetString(2);
                    int prod_amount = reader.GetInt32(3);
                    double prod_price = reader.GetFloat(4);

                    connection.Close();
                    return new Product(prod_type, prod_name, prod_id, prod_amount, prod_price);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public Product GetProduct(Shop shop, AProduct product)
        {
            return GetProduct(shop.Name, product.Type, product.Name);
        }

        public List<Pair<Shop, Product>> GetProduct(AProduct product)
        {
            return GetProduct(product.Type, product.Name);
        }

        public List<Pair<Shop, Product>> GetProduct(string type, string name)
        {
            try
            {
                string selected = $"{shops_products_table}.ProductId, " +
                                  $"{shops_products_table}.Amount, {shops_products_table}.Price, " +
                                  $"{shops_table}.Id, {shops_table}.Name";
                string product_query =
                    $"SELECT {products_table}.Id FROM {products_table} WHERE " +
                    $"{products_table}.AccountType = '{type}' AND {products_table}.Name = '{name}' ";
                string sql_query = $"SELECT {selected} FROM {shops_products_table} INNER JOIN {shops_table} ON " +
                                   $"{shops_products_table}.ProductID = ( {product_query} ) AND " +
                                   $"{shops_table}.Id = {shops_products_table}.ShopId";
                connection.Open();
                SqlCommand command = new SqlCommand(sql_query, connection);
                List<Pair<Shop, Product>> list = new List<Pair<Shop, Product>>();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        connection.Close();

                        return null;
                    }

                    while (reader.Read())
                    {

                        int prod_id = reader.GetInt32(0);
                        int prod_amount = reader.GetInt32(1);
                        double prod_price = reader.GetFloat(2);
                        int shop_id = reader.GetInt32(3);
                        string shop_name = reader.GetString(4);

                        Shop new_shop = new Shop(shop_name, shop_id);
                        Product new_product = new Product(type, name, prod_id, prod_amount, prod_price);
                        Pair<Shop, Product> pair = new Pair<Shop, Product>(new_shop, new_product);
                        list.Add(pair);
                    }
                }

                connection.Close();
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Product> GetProducts(Shop shop)
        {
            Shop new_shop = GetShop(shop.Name);
            string selected =
                $"{products_table}.Id, {products_table}.AccountType, {products_table}.Name, {shops_products_table}.Amount, {shops_products_table}.Price";
            string sql_query = $"SELECT {selected} FROM {shops_products_table} INNER JOIN {products_table} ON " +
                               $"{shops_products_table}.ShopId = {new_shop.Id} AND {shops_products_table}.ProductId = {products_table}.ID";

            connection.Open();
            SqlCommand command = new SqlCommand(sql_query, connection);
            List<Product> products = new List<Product>();
            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    connection.Close();

                    return null;

                }

                while (reader.Read())
                {
                    int prod_id = reader.GetInt32(0);
                    string prod_type = reader.GetString(1);
                    string prod_name = reader.GetString(2);
                    int prod_amount = reader.GetInt32(3);
                    double prod_price = reader.GetFloat(4);

                    Product new_product = new Product(prod_type, prod_name, prod_id, prod_amount, prod_price);
                    products.Add(new_product);
                }
            }

            connection.Close();
            return products;
        }

        public void UpdateProduct(Shop shop, AProduct product, UpdateQuery query)
        {
            try
            {
                Shop curr_shop = GetShop(shop.Name);
                Product curr_prod = GetProduct(curr_shop, product.Type, product.Name);

                if (curr_prod == null)
                {
                    int prod_id = GetProductID(product);
                    string value = $"({shop.Id}, {prod_id}, 0, 0)";
                    DoInsert(shops_products_table, value);
                    UpdateProduct(shop, product, query);
                }
                else
                {

                    int amount = 0;
                    double price = 0;

                    if (query.for_amount != QueryType.None)
                    {
                        if (query.for_amount == QueryType.ChangeBy)
                        {
                            amount = curr_prod.Amount + query.amount;
                        }
                        else if (query.for_amount == QueryType.Update)
                        {
                            amount = query.amount;
                        }
                    }
                    else
                    {
                        amount = curr_prod.Amount;
                    }

                    if (query.for_price != QueryType.None)
                    {
                        if (query.for_price == QueryType.ChangeBy)
                        {
                            price = curr_prod.Price + query.price;
                        }
                        else if (query.for_price == QueryType.Update)
                        {
                            price = query.price;
                        }
                    }
                    else
                    {
                        price = curr_prod.Price;
                    }

                    string prod_info =
                        $"{shops_products_table}.ShopId = {curr_shop.Id} AND {shops_products_table}.ProductId = {curr_prod.Id}";
                    string sql_query =
                        $"UPDATE {shops_products_table} SET {shops_products_table}.Amount = {amount}, {shops_products_table}.Price = {price} "
                        + $" WHERE {prod_info}";

                    connection.Open();
                    SqlCommand command = new SqlCommand(sql_query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateProduct(Shop shop, List<AProduct> products, List<UpdateQuery> queries)
        {
            for (int i = 0; i < products.Count; i++)
            {
                UpdateProduct(shop, products[i], queries[i]);
            }
        }

        public void UpdateProduct(List<Shop> shops, AProduct product, List<UpdateQuery> queries)
        {
            for (int i = 0; i < shops.Count; i++)
            {
                UpdateProduct(shops[i], product, queries[i]);
            }
        }

        public void UpdateProduct(List<Shop> shops, List<AProduct> products,
            List<List<UpdateQuery>> queries_for_every_shop)
        {
            for (int i = 0; i < shops.Count; i++)
            {
                Shop shop = shops[i];
                List<UpdateQuery> queires = queries_for_every_shop[i];
                UpdateProduct(shop, products, queires);
            }
        }

        public bool ImportData(DataBase db)
        {
            string copy_init_mode = init_mode;
            init_mode = "DeleteOld";
            InitDataBase();
            init_mode = copy_init_mode;

            foreach (AProduct product in db.a_products)
            {
                AddProduct(product);
            }


            foreach (Pair<Shop, List<Product>> pair in db.shops_products)
            {
                Shop shop = pair.First;
                AddShop(shop.Name);
                List<Product> products = pair.Second;
                List<AProduct> prods = new List<AProduct>();
                List<UpdateQuery> queries = new List<UpdateQuery>();
                foreach (Product product in products)
                {
                    prods.Add(product);
                    QueryType for_amount = QueryType.Update;
                    QueryType for_price = QueryType.Update;
                    queries.Add(new UpdateQuery(for_amount, product.Amount, for_price, product.Price));
                }

                UpdateProduct(shop, prods, queries);

            }

            return true;
        }

        public DataBase ExportData()
        {
            List<Shop> shops = GetShop();
            List<AProduct> prods = GetProduct();
            List<Pair<Shop, List<Product>>> shops_products = new List<Pair<Shop, List<Product>>>();
            foreach (Shop shop in shops)
            {
                List<Product> products = GetProducts(shop);
                shops_products.Add(new Pair<Shop, List<Product>>(shop, products));
            }
            DataBase db = new DataBase();
            db.a_products = prods;
            db.shops_products = shops_products;

            return db;
        }
    }
}