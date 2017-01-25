using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Tests
{
    internal class Basket
    {
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private int total;

        public int Total { get { return total; } }

        private void CalculateTotal()
        {
            
            var runningTotal = 0
            foreach(var item in items.Keys)
            {                
                if (item.HasPromotion)
                {
                    runningTotal = runningTotal + item.Promotion.Apply(items[item]);
                }
                else
                {
                    runningTotal = runningTotal + 1;
                }
            }
            total = runningTotal;
        }

        internal void AddItemToBasket(Item item)
        {
            if (!items.ContainsKey(item))
            {
                items.Add(item, 0);                
            }
            items[item] = items[item] + 1;

            CalculateTotal();
        }
    }
}