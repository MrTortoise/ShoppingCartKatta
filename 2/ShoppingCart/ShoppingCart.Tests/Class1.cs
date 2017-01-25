using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests
{
  
    public class Class1
    {
        private const string Sku1 = "sku1";

        [Test]
        public void ScanItemGetItem()
        {
            var item = new Item(Sku1, 30);
            var thing = new Thing();            
            var basket = thing.ScanItem(item);
            Assert.That(basket.Total, Is.EqualTo(item.Price));
        }
    }

    internal class Thing
    {
        public Thing()
        {
        }

        internal Basket ScanItem(Item item)
        {
            return new Tests.Basket(item);
        }
    }

    internal class Basket
    {
        private Item item;

        public Basket(Item item)
        {
            this.item = item;
        }

        internal int Total()
        {
            return item.Price;
        }
    }
}
