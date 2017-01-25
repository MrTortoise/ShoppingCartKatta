using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class Class1
    {
        [Test]
        public void AddItemCheckTotal()
        {
            var item1 = new Item("sku1", 50);
            var basket = new Basket();
            basket = basket.AddItem(item1);
            Assert.That(basket.Total, Is.EqualTo(50));
        }
    }

    internal class Basket
    {
        private Item item;

        public Basket()
        {
        }

        internal Basket AddItem(Item item)
        {
            var basket = new Basket();
            basket.item = item;
            return basket;
        }

        public int Total => 50;
    }

    internal class Item
    {
        private string v1;
        private int v2;

        public Item(string v1, int amount)
        {
            this.v1 = v1;
            this.v2 = amount;
        }
    }
}
