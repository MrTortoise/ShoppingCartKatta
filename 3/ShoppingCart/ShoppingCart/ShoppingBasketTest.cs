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

        [Test]
        public void AddItemsOnPromotion()
        {
            var item1 = new Item("sku1", 50);
            var item2 = new Item("sku2", 70);

            var promo = new Promotion(item1, 3, 100);
            var basket = new Basket();
            basket = basket
                .AddPromotion(promo)
                .AddItem(item1,3);

            Assert.That(basket.Total, Is.EqualTo(100));
        }

        [Test]
        public void AddMultipleItemsOnPromotion()
        {
            var item1 = new Item("sku1", 50);
            var item2 = new Item("sku2", 70);

            var promo = new Promotion(item1, 3, 100);
            var promo2 = new Promotion(item2, 3, 140);
            var basket = new Basket();
            basket = basket
                .AddPromotion(promo)
                .AddPromotion(promo2)
                .AddItem(item1, 3)
                .AddItem(item2,3);

            Assert.That(basket.Total, Is.EqualTo(240));
        }

        [Test]
        public void AddMultipleItemsOnPromotionAndSomeOff()
        {
            var item1 = new Item("sku1", 50);
            var item2 = new Item("sku2", 70);
            var item3 = new Item("sku3", 4);

            var promo = new Promotion(item1, 3, 100);
            var promo2 = new Promotion(item2, 3, 140);
            var basket = new Basket();
            basket = basket
                .AddPromotion(promo)
                .AddPromotion(promo2)
                .AddItem(item1, 4)
                .AddItem(item2, 4)
                .AddItem(item3);

            Assert.That(basket.Total, Is.EqualTo(150+210+4));
        }
    }

    internal class Basket
    {
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private Dictionary<Item, Promotion> promotions = new Dictionary<Item, Promotion>();

        public int Total()
        {
            int runningTotal = 0;
            foreach (var item in items.Keys)
            {
                if (promotions.ContainsKey(item))
                {
                    var promotion = promotions[item];
                    runningTotal += promotion.Apply(items[item]);
                }
                else
                {
                    runningTotal += item.Price * items[item];
                }
            }

            return runningTotal;
        }


        public Basket()
        {
        }

        private Basket(Basket basket)
        {
            this.items = new Dictionary<Item, int>(basket.items);
            this.promotions = new Dictionary<Item, Promotion>(basket.promotions);
        }

        internal Basket AddItem(Item item)
        {
            return AddItem(item, 1);
        }
        internal Basket AddItem(Item item, int quantity)
        {
            var basket = new Basket(this);
            if (!basket.items.ContainsKey(item))
            {
                basket.items.Add(item, 0);
            }
            basket.items[item] = basket.items[item] + quantity;

            return basket;
        }

        internal Basket AddPromotion(Promotion promotion)
        {
            var basket = new Basket(this);
            if (!basket.promotions.ContainsKey(promotion.Item))
            {
                basket.promotions.Add(promotion.Item, promotion);
            }
            else
                basket.promotions[promotion.Item] = promotion;

            return basket;
        }

    
    }

    internal class Item
    {
        private string sku;
        private int price;

        public Item(string sku, int price)
        {
            this.sku = sku;
            this.price = price;
        }

        public int Price => price;

        public object Sku => sku;
    }
}
