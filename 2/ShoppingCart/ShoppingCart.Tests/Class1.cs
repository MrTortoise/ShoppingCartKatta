using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests
{
  
    public class Class1
    {
        private const string Sku1 = "sku1";

        private const string Sku2 = "sku2";

        [Test]
        public void ScanItemGetItem()
        {
            var item = new Item(Sku1, 30);
            var thing = new Basket();            
            var basket = thing.ScanItem(item);
            Assert.That(basket.Total, Is.EqualTo(item.Price));
        }

        [Test]
        public void ScanMultipleItemsGetTotal()
        {
            var item1 = new Item(Sku1, 30);
            var item2 = new Item(Sku2, 50);
            var basket = new Basket();
            basket = basket.ScanItem(item1);
            basket = basket.ScanItem(item2);
            Assert.That(basket.Total, Is.EqualTo(80));
        }
    }
    internal class Basket
    {
        private List<Item> items;

        public Basket()
        {
            items = new List<Item>();
        }

        public Basket(IEnumerable<Item> items,Item item) : this()
        {
            this.items.AddRange(items);
            this.items.Add(item);
        }

        internal Basket ScanItem(Item item)
        {
            return new Basket(items, item);            
        }

        internal int Total()
        {
            return items.Sum(i => i.Price);
        }
    }
}
