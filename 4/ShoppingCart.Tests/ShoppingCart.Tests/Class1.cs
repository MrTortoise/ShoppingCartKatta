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
        private const string Sku2 = "sku2";
        private const string Sku3 = "sku3";

        private ItemSource itemSource = new ItemSource();

        [Test]
        public void ScanAnOrderGetAPrice()
        {
            var thing = new Thing(itemSource);           
            thing.Scan(Sku1);
            var order = thing.CheckOut();
            Assert.That(order.Total, Is.EqualTo(5));            
        }

        [Test]
        public void ScanOrdersGetAPrice()
        {
            var thing = new Thing(itemSource);           
            thing.Scan(Sku1);
            thing.Scan(Sku2);
            var order = thing.CheckOut();
            Assert.That(order.Total, Is.EqualTo(5));
        }
    }

    internal class Thing
    {
        private ItemSource itemSource;

        public Thing(ItemSource itemSource)
        {
            this.itemSource = itemSource;
        }

        internal Order CheckOut()
        {
            return new Order();
        }

        internal void Scan(string sku)
        {
            var item = itemSource.GetItemFromSku(sku);
        }
    }

    internal class Order
    {
        public int Total => 5;
    }

    internal class ItemSource
    {
        public ItemSource()
        {
        }

        internal Item GetItemFromSku(string sku)
        {
            return new Tests.Item();
        }
    }

    internal class Item
    {
    }
}
