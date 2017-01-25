using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Tests
{
    internal class Basket
    {
        private List<Item> items = new List<Item>();
        private int total;

        public int Total { get { return total; } }

        private void CalculateTotal()
        {
            total = items.Sum(i => i.Price);
        }

        internal void AddItemToBasket(Item item)
        {
            items.Add(item);
            CalculateTotal();
        }
    }
}