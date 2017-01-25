using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Tests
{
    internal class Basket
    {
        private List<Item> items = new List<Item>();

        public int Total { get { return items.Sum(i => i.Price); } }

        internal void AddItemToBasket(Item item)
        {
            items.Add(item);            
        }
    }
}