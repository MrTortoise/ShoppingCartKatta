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

        [Test]
        public void ScanItemsWithPromotionGetTotal()
        {
            var item1 = new Item(Sku1, 30);
            var item2 = new Item(Sku2, 50);
            var promotion = new Promotion(item1, 3, 70);
            var basket = new Basket();
            basket = basket.AddPromotion(promotion);
            basket = basket.ScanItem(item1);
            basket = basket.ScanItem(item1);
            basket = basket.ScanItem(item1);
            Assert.That(basket.Total, Is.EqualTo(70));
        }
    }

    internal class Promotion
    {
        private Item item;
        private int threshold;
        private int price;

        public Item Item => item;

        public Promotion(Item item, int quantity, int price)
        {
            this.item = item;
            this.threshold = quantity;
            this.price = price;
        }

        internal int Apply(int quantity)
        {
            if (quantity>=threshold)
            {
                int rem = quantity - threshold;
                return price + rem * item.Price;
            }

            return item.Price * quantity;
        }
    }

    internal class Basket
    {
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private Promotion promotion;

        public Basket()
        {
            
        }

        private Basket(Basket basket)
        {
            this.promotion = basket.promotion;
            items = new Dictionary<Item, int>(basket.items);         
        }

        internal Basket AddPromotion(Promotion promotion)
        {
            var basket = new Basket(this);
            basket.promotion = promotion;
            return basket;
        }

        internal Basket ScanItem(Item item)
        {
            var basket =  new Basket(this);
            if (!basket.items.ContainsKey(item))
            {
                basket.items.Add(item, 0);
            }
            basket.items[item]++;
            return basket;
        }

        internal int Total()
        {
            int runningTotal = 0;
            foreach(var item in items.Keys)
            {
                if (promotion != null && promotion.Item.Sku == item.Sku)
                {
                    runningTotal = promotion.Apply(items[item]);
                }
                else
                {
                    runningTotal += item.Price * items[item];
                }
            }

            return runningTotal;
        }
    }
}
