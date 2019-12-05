using System;
using System.Collections.Generic;
using System.Text;
using MyLabs.Lab4.Structure;
using MyLabs.Lab4.Management;
using MyLabs.Lab4.DAO;
using MyLabsCopy.Lab4.Structure;

namespace MyLabs.Lab4.Management
{
    interface IShopService
    {
        Shop AddShop(string name);
        AProduct AddProduct(string type, string name);
        bool SetProductPrice(Shop shop, AProduct product, double price);
        bool SupplyShop(Shop shop, AProduct product, int amount);
        bool SupplyShop(Shop shop, List<AProduct> products, List<int> amounts);

        bool SupplyShop(List<Shop> shops, List<AProduct> products, List<List<int>> amounts_for_shop);
        //bool SupplyShops(List<Pair<Shop, Product>> list);

        // supply список магазин с списком товаров для каждого
        Pair<Shop, Product> FindCheapest(AProduct product);

        // Pair<Shop, List<Product>> FindCheapest(List<AProduct> products); потом
        List<Product> FindProductsForMoney(Shop shop, double money);

        // buy products тоже обобщить
        // подумать над возвращаемым значением
        double BuyProduct(Shop shop, List<AProduct> products, List<int> amounts);
        void InitializeDAO(string file, DataBase db);
    }
}