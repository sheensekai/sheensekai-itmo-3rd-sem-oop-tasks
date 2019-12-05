using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using MyLabs.Lab4.Structure;
using MyLabs.Lab4.Management;
using MyLabs.Lab4.DAO;
using MyLabs.Lab4.Structure.UpdateQuery;
using MyLabs.Lab3;
using MyLabs.Lab4.Exceptions;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data.SqlClient;
using MyLabsCopy.Lab4.Structure;

namespace MyLabs.Lab4.Management
{
    class ShopService : IShopService
    {
        private IDAO dao;

        public Shop AddShop(string name)
        {
            return dao.AddShop(name);
        }

        public Shop GetShop(string name)
        {
            return dao.GetShop(name);
        }

        public List<Shop> GetShop()
        {
            return dao.GetShop();
        }

        public AProduct AddProduct(string type, string name)
        {
            return dao.AddProduct(type, name);
        }

        public bool SetProductPrice(Shop shop, string type, string name, double price)
        {
            return SetProductPrice(shop, new AProduct(type, name), price);
        }

        public bool SetProductPrice(Shop shop, AProduct product, double price)
        {
            QueryType for_amount = QueryType.None;
            QueryType for_price = QueryType.Update;
            dao.UpdateProduct(shop, product, new UpdateQuery(for_amount, 0, for_price, price));
            return true;
        }

        public bool SupplyShop(Shop shop, AProduct product, int amount)
        {
            QueryType for_amount = QueryType.ChangeBy;
            QueryType for_price = QueryType.None;
            dao.UpdateProduct(shop, product, new UpdateQuery(for_amount, amount, for_price, 0));
            return true;
        }

        public bool SupplyShop(Shop shop, string type, string name, int amount)
        {
            return SupplyShop(shop, new AProduct(type, name), amount);
        }

        public bool SupplyShop(Shop shop, List<AProduct> products, List<int> amounts)
        {
            List<UpdateQuery> queries = new List<UpdateQuery>();
            for (int i = 0; i < products.Count; i++)
            {
                QueryType for_amount = QueryType.ChangeBy;
                QueryType for_price = QueryType.None;
                queries.Add(new UpdateQuery(for_amount, amounts[i], for_price, 0));
            }

            dao.UpdateProduct(shop, products, queries);
            return true;
        }

        public bool SupplyShop(List<Shop> shops, List<AProduct> products, List<List<int>> amounts_for_shop)
        {
            List<List<UpdateQuery>> queries_for_shop = new List<List<UpdateQuery>>();
            for (int i = 0; i < shops.Count; i++)
            {
                List<UpdateQuery> queries = new List<UpdateQuery>();
                List<int> amounts = amounts_for_shop[i];

                for (int j = 0; j < products.Count; j++)
                {
                    QueryType for_amount = QueryType.ChangeBy;
                    QueryType for_price = QueryType.None;
                    queries.Add(new UpdateQuery(for_amount, amounts[i], for_price, 0));
                }

                queries_for_shop.Add(queries);
            }

            dao.UpdateProduct(shops, products, queries_for_shop);
            return true;
        }

        public Pair<Shop, Product> FindCheapest(string type, string name)
        {
            var pair_list = dao.GetProduct(type, name);
            // как в строчку найти минимальный??
            double price = pair_list[0].Second.Price;
            var result = pair_list[0];

            foreach (var pair in pair_list)
            {
                if (pair.Second.Price < price)
                {
                    price = pair.Second.Price;
                    result = pair;
                }
            }

            return result;
        }

        public Pair<Shop, Product> FindCheapest(AProduct product)
        {
            return FindCheapest(product.Type, product.Name);
        }

        public List<Product> FindProductsForMoney(Shop shop, double money)
        {
            List<Product> products = dao.GetProducts(shop);
            List<Product> result = new List<Product>();

            foreach (Product product in products)
            {
                result.Add(UpdateForMoney(product, money));
            }

            return result;
        }

        static private Product UpdateForMoney(Product product, double money)
        {
            if (product.Cost < money)
            {
                return new Product(product);
            }

            else
            {
                int Amount = (int) (money / product.Price);
                return new Product(product, Amount);
            }
        }

        // обдумать возвращаемое значение

        public double BuyProduct(Shop shop, AProduct product, int amount)
        {
            List<AProduct> list = new List<AProduct>();
            List<int> list2 = new List<int>();
            list.Add(product);
            list2.Add(amount);
            return BuyProduct(shop, list, list2);
        }

        public double BuyProduct(Shop shop, List<AProduct> products, List<int> amounts)
        {
            List<UpdateQuery> queries = new List<UpdateQuery>();
            double result_price = 0;
            for (int i = 0; i < products.Count; i++)
            {
                Product product = dao.GetProduct(shop, products[i]);
                if (product.Amount < amounts[i])
                {
                    return -1;
                }

                result_price += product.Price * amounts[i];

                QueryType for_amount = QueryType.ChangeBy;
                QueryType for_price = QueryType.None;
                int amount = -amounts[i];
                double price = 0;

                queries.Add(new UpdateQuery(for_amount, amount, for_price, price));
            }

            dao.UpdateProduct(shop, products, queries);
            return result_price;
        }

        public Pair<Shop, List<Product>> FindCheapestAmount(List<AProduct> products, List<int> amounts)
        {
            Dictionary<Shop, List<Product>> shops = FindAllShopsWithProduct(products);

            Dictionary<Shop, double> shops2 = FindAppropriateShops(shops, products, amounts);

            double min = double.MaxValue;
            Shop curr_shop = null;
            foreach (KeyValuePair<Shop, double> pair in shops2)
            {
                double cost = pair.Value;

                if (cost < min)
                {
                    curr_shop = pair.Key;
                    min = cost;
                }
            }

            shops.TryGetValue(curr_shop, out List<Product> list2);
            Pair<Shop, List<Product>> answer = new Pair<Shop, List<Product>>(curr_shop, list2);
            return answer;
        }

        private Dictionary<Shop, List<Product>> FindAllShopsWithProduct(List<AProduct> products)
        {
            ShopEqualityComparer comparer = new ShopEqualityComparer();
            Dictionary<Shop, List<Product>> shops = new Dictionary<Shop, List<Product>>(comparer);
            for (int i = 0; i < products.Count; i++)
            {
                AProduct prod = products[i];
                var pair_list = dao.GetProduct(prod.Type, prod.Name);
                foreach (Pair<Shop, Product> pair in pair_list)
                {
                    Shop shop = pair.First;
                    Product product = pair.Second;

                    if (!shops.ContainsKey(shop))
                    {
                        shops.Add(shop, new List<Product>());
                    }

                    shops.TryGetValue(shop, out List<Product> list);
                    list.Add(product);

                }
            }

            return shops;
        }

        private Dictionary<Shop, double> FindAppropriateShops(Dictionary<Shop, List<Product>> shops,
            List<AProduct> products, List<int> amounts)
        {
            ShopEqualityComparer comparer = new ShopEqualityComparer();
            Dictionary<Shop, double> result = new Dictionary<Shop, double>(comparer);
            foreach (KeyValuePair<Shop, List<Product>> pair in shops)
            {
                List<Product> list = pair.Value;
                if (list.Count != products.Count)
                {
                    continue;
                }

                bool fine = true;
                double cost = 0;
                for (int i = 0; i < products.Count; i++)
                {
                    if (list[i].Amount < amounts[i])
                    {
                        fine = false;
                        break;
                    }

                    cost += amounts[i] * list[i].Price;
                }

                if (!fine)
                {
                    continue;
                }

                result.Add(pair.Key, cost);
            }

            return result;
        }

    public void InitializeDAO(string file, DataBase db = null)
        {
            var dao_info = IniParser.Parse(file);
            string mode = dao_info["DAOSettings"]["DatabaseMode"].ToString();
            if (mode == DAOInfo.FileDBMode)
            { 
                InitFileDB(dao_info, db);

            }
            else if (mode == DAOInfo.RelativeDBMode)
            { 
                InitRDB(dao_info, db);
            }
            else
            {
                throw new DAOException("Wrong DB mode in properties file");
            }

        }

        private IDAO InitFileDB(IniFile dao_info, DataBase db = null)
        {
            string init_mode = dao_info["DAOSettings"]["InitializeMode"].ToString();
            IniSection filedb = dao_info["FileDBSettings"];

            string shops_file = filedb["ShopsFileName"].ToString();
            string products_file = filedb["ProductsFileName"].ToString();
            char sect_sep = filedb["SectionSeparator"].ToString()[1];
            char field_sep = filedb["FieldSeparator"].ToString()[1];

            dao = new FileDatabase(shops_file, products_file, sect_sep, field_sep, init_mode, db);
            return dao;
        }

        private IDAO InitRDB(IniFile dao_info, DataBase db = null)
        {
            string init_mode = dao_info["DAOSettings"]["InitializeMode"].ToString();
            IniSection rdb = dao_info["RelativeDBSettings"];

            string data_source = rdb["DataSource"].ToString();
            string catalog = rdb["InitialCatalog"].ToString();
            string userid = rdb["UserID"].ToString();
            string password = rdb["Password"].ToString();

            IniSection tables = dao_info["RelativeDBTables"];
            string shop_table = tables["ShopsTableName"].ToString();
            string product_table = tables["ProductsTableName"].ToString();
            string shop_products_table = tables["ShopsProductsTableName"].ToString();

            string connection_string = $"Data Source={data_source};" +
                                       $"Initial Catalog={catalog};" +
                                       $"User ID={userid};" +
                                       $"Password={password}";
            SqlConnection connection = new SqlConnection(connection_string);

            dao = new RelationalDatabase(connection, shop_table, product_table, shop_products_table, init_mode, db);
            return dao;
        }

        public DataBase GetData()
        {
            return dao.ExportData();
        }
    }
}