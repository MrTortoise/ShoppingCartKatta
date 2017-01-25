using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public class ShoppingBasketTest
    {
        private static Item sku1 = new Item(50);
        private static Item sku2 = new Item(70);
        private static Item sku3 = new Item(4);

        [Test]
        public void AddItemCheckTotal()
        {
            var basket = new Basket();
            basket = basket.AddItem(sku1);
            Assert.That(basket.Total, Is.EqualTo(50));
        }

        [Test]
        public void AddMultipleItemsCheckTotal()
        {
            var basket = new Basket();
            basket = basket
                .AddItem(sku1)
                .AddItem(sku2);

            Assert.That(basket.Total, Is.EqualTo(120));
        }

        [Test]
        public void AddItemsOnPromotion()
        {
            var promo = new Promotion(sku1, 3, 100);
            var basket = new Basket();
            basket = basket
                .AddPromotion(promo)
                .AddItem(sku1, 3);

            Assert.That(basket.Total, Is.EqualTo(100));
        }

        [Test]
        public void AddMultipleItemsOnPromotion()
        {
            var promo = new Promotion(sku1, 3, 100);
            var promo2 = new Promotion(sku2, 3, 140);
            var basket = new Basket();
            basket = basket
                .AddPromotion(promo)
                .AddPromotion(promo2)
                .AddItem(sku1, 3)
                .AddItem(sku2, 3);

            Assert.That(basket.Total, Is.EqualTo(240));
        }

        [Test]
        public void AddMultipleItemsOnPromotionAndSomeOff()
        {
            var promo = new Promotion(sku1, 3, 100);
            var promo2 = new Promotion(sku2, 3, 140);
            var basket = new Basket();
            basket = basket
                .AddPromotion(promo)
                .AddPromotion(promo2)
                .AddItem(sku1, 4)
                .AddItem(sku2, 4)
                .AddItem(sku3);

            Assert.That(basket.Total, Is.EqualTo(150 + 210 + 4));
        }
    }

    internal class Basket
    {
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private Dictionary<Item, Promotion> promotions = new Dictionary<Item, Promotion>();

        public int Total()
        {
            int total = items.Keys.Aggregate(0, (acc, item) => acc += SubTotal(item));
            return total;            
        }

        private int SubTotal(Item item)
        {
            if (promotions.ContainsKey(item))
            {

                return promotions[item].Apply(items[item]);
            }
            else
            {
                return item.Price * items[item];
            } 
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
        private int price;

        public Item(int price)
        {
            this.price = price;
        }

        public int Price => price;
    }
}
