using System;
using System.Collections.Generic;
using System.Text;
using MyLabsCopy.Lab4.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using MyLabs.Lab4;
using MyLabs.Lab4.Structure;
using MyLabs.Lab4.Management;
using MyLabs.Lab4.DAO;
using MyLabs.Lab4.Exceptions;
using MyLabs.Lab4.Structure.UpdateQuery;

namespace MyLabsCopy.Lab4.Structure
{
    class DataBase
    {
        public List<AProduct> a_products;
        public List<Pair<Shop, List<Product>>> shops_products;
    }
}
