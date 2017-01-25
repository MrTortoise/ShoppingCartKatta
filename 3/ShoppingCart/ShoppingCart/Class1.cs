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

        [Test]
        public void AddMultipleItemsCheckTotal()
        {
            var item1 = new Item("sku1", 50);
            var item2 = new Item("sku2", 70);
            var basket = new Basket();
            basket = basket
                .AddItem(item1)
                .AddItem(item2);

            Assert.That(basket.Total, Is.EqualTo(120));
        }
    }

    internal class Basket
    {
        private List<Item> items = new List<Item>();

        public Basket()
        {
        }

        private Basket(IEnumerable<Item> items)
        {
            this.items.AddRange(items);
        }

        internal Basket AddItem(Item item)
        {
            var basket = new Basket(items);
            basket.items.Add(item);
            return basket;
        }

        public int Total => items.Sum(i => i.Price);
    }

    internal class Item
    {
        private string v1;
        private int price;

        public Item(string v1, int price)
        {
            this.v1 = v1;
            this.price = price;
        }

        public int Price => price;
    }
}
