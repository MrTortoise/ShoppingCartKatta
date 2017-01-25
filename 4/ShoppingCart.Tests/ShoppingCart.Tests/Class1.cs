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

        private ItemSource itemSource = new ItemSource(new List<Item>() {
            new Item(Sku1,10),
            new Item(Sku2,21),
            new Item(Sku3,32)
            });

        [Test]
        public void ScanAnOrderGetAPrice()
        {
            var thing = new Thing(itemSource, new PromotionSource(new List<IPromotion>()));
            thing.Scan(Sku1);
            var order = thing.CheckOut();
            Assert.That(order.Total, Is.EqualTo(10));
        }

        [Test]
        public void ScanOrdersGetAPrice()
        {
            var thing = new Thing(itemSource, new PromotionSource(new List<IPromotion>()));
            thing.Scan(Sku1);
            thing.Scan(Sku2);
            var order = thing.CheckOut();
            Assert.That(order.Total, Is.EqualTo(31));
        }

        [Test]
        public void ScanOrdersGetAPriceApplyAPromotion()
        {
            var promotionSource = new PromotionSource(new List<IPromotion>()
            {
                new BuyXGetY(itemSource.GetItemFromSku(Sku1),3,1)
            });
            var thing = new Thing(itemSource, promotionSource);
            thing.Scan(Sku1);
            thing.Scan(Sku1);
            thing.Scan(Sku1);
            thing.Scan(Sku1);
            thing.Scan(Sku2);
            var order = thing.CheckOut();
            Assert.That(order.Total, Is.EqualTo(51));
        }


    }
    internal class BuyXGetY : IPromotion
    {
        private Item item;
        private int x;
        private int y;

        public BuyXGetY(Item item, int x, int y)
        {
            this.item = item;
            this.x = x;
            this.y = y;
        }

        public void Apply(Order order)
        {
            if ()
        }
    }
    internal interface IPromotion
    {
        void Apply(Order order);
    }

    internal class PromotionSource
    {
        private List<IPromotion> promotions;

        public PromotionSource(List<IPromotion> promotions)
        {
            this.promotions = promotions;
        }

        public List<IPromotion> Promotions => promotions;
    }

    internal class Thing
    {
        private Dictionary<Item, int> items = new Dictionary<Item, int>();
        private ItemSource itemSource;
        private PromotionSource promotionSource;

        public Thing(ItemSource itemSource, PromotionSource promotionSource)
        {
            this.itemSource = itemSource;
            this.promotionSource = promotionSource;
        }

        internal Order CheckOut()
        {
            var order = new Order(items);
            order.ApplyPromotions(promotionSource);

            return order;
        }

        internal void Scan(string sku)
        {
            var item = itemSource.GetItemFromSku(sku);
            if (!items.ContainsKey(item))
            {
                items.Add(item, 0);
            }
            items[item]++;
        }
    }

    internal class Order
    {
        private Dictionary<Item, int> items;

        public Order(Dictionary<Item, int> items, PromotionSource promotionSource)
        {
            this.items = items;
            var promotions = promotionSource.Promotions;
            foreach (var promotion in promotions)
            {
                promotion.Apply(this);
            }
        }

        public int Total => items.Keys.Aggregate(0, (t, i) => t += i.Price * items[i]);

        internal void ApplyPromotions(PromotionSource promotionSource)
        {

        }
    }

    internal class ItemSource
    {
        private Dictionary<string, Item> items;

        public ItemSource(List<Item> list)
        {
            this.items = new Dictionary<string, Item>(list.ToDictionary(i => i.Sku));
        }

        internal Item GetItemFromSku(string sku)
        {
            return items[sku];
        }
    }

    internal class Item
    {
        public int Price => price;

        public string Sku => sku;

        private string sku;
        private int price;

        public Item(string sku, int price)
        {
            this.sku = sku;
            this.price = price;
        }
    }
}
