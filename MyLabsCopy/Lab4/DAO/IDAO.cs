using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MyLabs.Lab4.Structure;
using MyLabs.Lab4.Management;
using MyLabs.Lab4.DAO;
using MyLabs.Lab4.Structure.UpdateQuery;
using MyLabsCopy.Lab4.Structure;

namespace MyLabs.Lab4.DAO
{
    interface IDAO
    {
        void InitDataBase();
        Shop AddShop(string name);

        List<Shop> AddShop(List<string> names);

        bool DelShop(Shop shop);

        bool DelShop(string name);
        Shop GetShop(string name);

        List<Shop> GetShop();

        AProduct AddProduct(string type, string name);

        AProduct AddProduct(AProduct product);
        bool DelProduct(AProduct product);

        List<AProduct> GetProduct();

        Product GetProduct(string shop, string type, string name);

        Product GetProduct(Shop shop, string type, string name);

        Product GetProduct(Shop shop, AProduct product);

        List<Pair<Shop, Product>> GetProduct(AProduct product);

        List<Pair<Shop, Product>> GetProduct(string type, string name);

        List<Product> GetProducts(Shop shop);

        void UpdateProduct(Shop shop, AProduct product, UpdateQuery query);

        void UpdateProduct(Shop shop, List<AProduct> products, List<UpdateQuery> queries);

        void UpdateProduct(List<Shop> shops, AProduct product, List<UpdateQuery> queries);

        void UpdateProduct(List<Shop> shops, List<AProduct> products, List<List<UpdateQuery>> queries_for_every_shop);

        DataBase ExportData();
        bool ImportData(DataBase db);
    }
}