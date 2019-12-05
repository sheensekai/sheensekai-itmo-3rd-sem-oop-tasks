using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using MyLabs.Lab4.Structure;
using MyLabs.Lab4.Management;
using MyLabs.Lab4.DAO;
using MyLabs.Lab4.Exceptions;
using MyLabs.Lab4.Structure.UpdateQuery;
using MyLabsCopy.Lab4.Structure;

namespace MyLabs.Lab4.DAO
{
    class FileDatabase : IDAO
    {
        private string shops_file;
        private string products_file;
        private char sect_separator;
        private char field_separator;
        private string init_mode;

        public FileDatabase(string shops_file, string products_file, char sect_sep, char field_sep, string init_mode, DataBase db = null)
        {
            this.shops_file = shops_file;
            this.products_file = products_file;
            this.sect_separator = sect_sep;
            this.field_separator = field_sep;
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
            if (init_mode == "DeleteOld")
            {
                if (File.Exists(shops_file))
                {
                    File.Delete(shops_file);
                }
                if (File.Exists((products_file)))
                {
                    File.Delete(products_file);
                }
            }

            if (!File.Exists(shops_file))
            {
                File.Create(shops_file).Close();

            }
            if (!File.Exists((products_file)))
            {
                File.Create(products_file).Close();
            }
        }

        public Shop AddShop(string name)
        {
            try
            {
                foreach (var line in File.ReadAllLines(shops_file))
                {
                    if (line == name)
                    {
                        throw new DAOException("Trying to add already existing shop");
                    }
                }

                int id = File.ReadAllLines(shops_file).Length;
                using (StreamWriter stream = File.AppendText((shops_file)))
                {
                    stream.WriteLine(name);
                    return new Shop(name, id + 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public List<Shop> AddShop(List<string> names)
        {
            List<Shop> shops = new List<Shop>();
            foreach (string name in names)
            {
                shops.Add(AddShop(name));
            }

            return shops;
        }


        // wrong. еще нужно удалить этот шоп из всех продуктов
        public bool DelShop(Shop shop)
        {
            return DelShop(shop.Name);
        }

        public bool DelShop(string name)
        {
            try
            {
                File.WriteAllLines(shops_file, File.ReadAllLines(shops_file).Where(line => line != name));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public Shop GetShop(Shop shop)
        {
            return GetShop(shop.Name);
        }

        public Shop GetShop(string name)
        {
            try
            {
                int id = 0;
                bool found = false;
                foreach (string line in File.ReadAllLines(shops_file))
                {
                    ++id;
                    if (line == name)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    return new Shop(name, id);
                }
                else
                {
                    throw new DAOException("Getting not existing shop");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private Shop GetShop(int id)
        {
            try
            {
                return new Shop(File.ReadAllLines(shops_file)[id - 1], id);
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
            var lines = File.ReadAllLines(shops_file);
            for (int i = 0; i < lines.Length; i++)
            {
                result.Add(new Shop(lines[i], i + 1));
            }

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
                foreach (var line in File.ReadAllLines(products_file))
                {
                    if (line.Split(sect_separator)[0] == type + field_separator + name)
                    {
                        throw new DAOException("Trying to add already existing shop");
                    }
                }

                using (StreamWriter stream = File.AppendText((products_file)))
                {
                    stream.WriteLine(type + field_separator + name + sect_separator);
                    return new AProduct(type, name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public List<AProduct> GetProduct()
        {
            var lines = File.ReadAllLines(products_file);
            List<AProduct> result = new List<AProduct>();
            for (int i = 0; i < lines.Length; i++)
            {
                var type_and_name = lines[i].Split(sect_separator)[0].Split(field_separator);
                result.Add(new AProduct(type_and_name[0], type_and_name[1]));
            }

            return result;
        }

        public bool DelProduct(AProduct product)
        {
            try
            {
                File.WriteAllLines(shops_file, File.ReadAllLines(shops_file).Where(
                    line => line.Split(sect_separator)[0] !=
                            product.Type + field_separator + product.Name + sect_separator));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public Product GetProduct(string shop, string type, string name)
        {
            return GetProduct(GetShop(shop), type, name);
        }

        public Product GetProduct(Shop shop, AProduct product)
        {
            return GetProduct(shop, product.Type, product.Name);
        }

        public Product GetProduct(Shop shop, string type, string name)
        {
            try
            {
                var pair = GetProductLine(type, name);
                string product_line = pair.First;
                int id = pair.Second;

                string shop_line = GetShopLine(shop, product_line);


                if (shop_line != null)
                {
                    var splitted = shop_line.Split(field_separator);
                    int.TryParse(splitted[1], out int amount);
                    double.TryParse(splitted[2], out double price);
                    return new Product(type, name, pair.Second, amount, price);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public List<Pair<Shop, Product>> GetProduct(AProduct product)
        {
            return GetProduct(product.Type, product.Name);
        }

        public List<Pair<Shop, Product>> GetProduct(string type, string name)
        {
            var pair = GetProductLine(type, name);
            string product_line = pair.First;
            int id = pair.Second;

            var list = new List<Pair<Shop, Product>>();

            var splitted = product_line.Split(sect_separator);

            for (int i = 1; i < splitted.Length - 1; i++)
            {
                var splitted2 = splitted[i].Split(field_separator);
                int.TryParse(splitted2[0], out int shop_id);
                Shop shop = GetShop(shop_id);
                int.TryParse(splitted2[1], out int amount);
                double.TryParse(splitted2[2], out double price);
                Product product = new Product(type, name, id, amount, price);
                list.Add(new Pair<Shop, Product>(shop, product));
            }

            return list;
        }

        public List<Product> GetProducts(Shop shop)
        {
            try
            {
                var list = new List<Product>();
                int id = 0;
                foreach (string line in File.ReadAllLines(products_file))
                {
                    ++id;
                    var split_product = line.Split(sect_separator);
                    string shop_line = GetShopLine(shop, split_product);
                    if (shop_line != null)
                    {
                        var split_shop = shop_line.Split(field_separator);
                        int.TryParse(split_shop[1], out int amount);
                        double.TryParse(split_shop[1], out double price);

                        var split_split_product = split_product[0].Split(field_separator);
                        string type = split_split_product[0];
                        string name = split_split_product[1];

                        list.Add(new Product(type, name, id, amount, price));
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public void UpdateProduct(Shop shop, AProduct product, UpdateQuery query)
        {
            List<AProduct> products = new List<AProduct>();
            products.Add(product);

            List<UpdateQuery> queries = new List<UpdateQuery>();
            queries.Add(query);

            UpdateProduct(shop, products, queries);
        }

        public void UpdateProduct(Shop shop, List<AProduct> products, List<UpdateQuery> queries)
        {
            List<Shop> shops = new List<Shop>();
            shops.Add(shop);

            List<List<UpdateQuery>> queries_for_shop = new List<List<UpdateQuery>>();
            queries_for_shop.Add(queries);

            UpdateProduct(shops, products, queries_for_shop);
        }

        public void UpdateProduct(List<Shop> shops, AProduct product, List<UpdateQuery> queries)
        {
            List<AProduct> products = new List<AProduct>();
            products.Add(product);

            List<List<UpdateQuery>> queries_for_shop = new List<List<UpdateQuery>>();
            queries_for_shop.Add(queries);

            UpdateProduct(shops, products, queries_for_shop);
        }

        public void UpdateProduct(List<Shop> shops, List<AProduct> products,
            List<List<UpdateQuery>> queries_for_every_shop)
        {
            try
            {
                var lines = File.ReadAllLines(products_file);
                File.WriteAllText(products_file, "", Encoding.Unicode);
                string result = "";
                List<string> list = new List<string>();
                foreach (string line in lines)
                {
                    string new_line = MakeUpdatedProductLine(line, shops, products, queries_for_every_shop);
                    list.Add(new_line);
                }
                File.WriteAllLines(products_file, list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private string MakeUpdatedProductLine(string product_line, List<Shop> shops, List<AProduct> products,
            List<List<UpdateQuery>> queries_for_every_shop)
        {
            // заменить на стрингбилдер
            // мб при нахождении магазина удалять найденный из список, чтобы упрощать дальнейший поиск
            string result = "";
            for (int i = 0; i < products.Count; i++)
            {
                List<Shop> copy_shops = new List<Shop>(shops);
                AProduct product = products[i];
                var splitted = product_line.Split(sect_separator);
                if (splitted[0] != product.Type + field_separator + product.Name)
                {
                    continue;
                }

                result += splitted[0] + sect_separator;
                for (int j = 1; j < splitted.Length - 1; j++)
                {
                    string shop_line = splitted[j];
                    var splitted_shop = shop_line.Split(field_separator);
                    int shop_pos = FindShopWithShopLine(splitted_shop, copy_shops);
                    if (shop_pos == -1)
                    {
                        result += shop_line + sect_separator;
                        continue;
                    }

                    Shop shop = copy_shops[shop_pos];
                    UpdateQuery query = queries_for_every_shop[shop_pos][i];
                    result += MakeUpdatedShopLine(splitted_shop, query);
                    copy_shops.RemoveAt(shop_pos);
                }

                if (copy_shops.Count != 0)
                {
                    for (int j = 0; j < copy_shops.Count; j++)
                    {
                        string[] fake_splitshop = { copy_shops[j].Id.ToString(), 0.ToString(), 0.ToString() };
                        result += MakeUpdatedShopLine(fake_splitshop, queries_for_every_shop[j][i]);
                    }
                }

                return result;
            }

            return product_line;
        }

        private int FindShopWithShopLine(string[] splitted_shop, List<Shop> shops)
        {
            int.TryParse(splitted_shop[0], out int id);
            Shop example = GetShop(id);

            for (int i = 0; i < shops.Count; i++)
            {
                if (example == shops[i])
                {
                    return i;
                }
            }

            return -1;
        }

        private string MakeUpdatedShopLine(string[] splitted_shop, UpdateQuery query)
        {
            // заменить на стрингбилдер
            string result = splitted_shop[0] + field_separator;

            int.TryParse(splitted_shop[1], out int amount);
            if (query.for_amount != QueryType.None)
            {
                if (query.for_amount == QueryType.ChangeBy)
                {
                    amount += query.amount;
                }

                else if (query.for_price == QueryType.Update)
                {
                    amount = query.amount;
                }
            }

            double.TryParse(splitted_shop[2], out double price);
            if (query.for_price != QueryType.None)
            {
                if (query.for_price == QueryType.ChangeBy)
                {
                    price += query.price;
                }

                else if (query.for_price == QueryType.Update)
                {
                    price = query.price;
                }
            }

            result += amount.ToString() + field_separator + price.ToString() + sect_separator;

            return result;
        }

        private Pair<string, int> GetProductLine(string type, string name)
        {
            string[] splitted = new string[1];
            int id = 0;

            foreach (var line in File.ReadAllLines(products_file))
            {
                ++id;
                splitted = line.Split(sect_separator);
                if (splitted[0] == type + field_separator + name)
                {
                    return new Pair<string, int>(line, id);
                }
            }

            throw new DAOException("Trying to get not existing product");
        }

        private string GetShopLine(Shop shop, string product_line)
        {
            string[] splitted = product_line.Split(sect_separator);
            return GetShopLine(shop, splitted);
        }

        private string GetShopLine(Shop shop, string[] product_line_splitted)
        {
            for (int i = 1; i < product_line_splitted.Length; i++)
            {
                if (product_line_splitted[i].Split(field_separator)[0] == shop.Id.ToString())
                {
                    return product_line_splitted[i];
                }
            }

            return null;
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
    }
}