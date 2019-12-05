using System;
using System.Collections.Generic;
using System.Text;

namespace MyLabs.Lab4.Structure.UpdateQuery
{
    class UpdateQuery
    {
        public QueryType for_amount;
        public QueryType for_price;
        public int amount;
        public double price;

        public UpdateQuery(QueryType for_amount, int amount, QueryType for_price, double price)
        {
            this.for_amount = for_amount;
            this.for_price = for_price;
            this.amount = amount;
            this.price = price;
        }
    }
}