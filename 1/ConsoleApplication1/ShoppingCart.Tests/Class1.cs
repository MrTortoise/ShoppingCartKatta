using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests
{
    class ShoppingCartTests
    {
        private const string Sku1 = "item1";
        private const string Sku2 = "item2";

        [Test]
        public void AddItemCartHasRightPrice()
        {
            var item1 = new Item(Sku1, 2);
            var item2 = new Item(Sku2, 5);
            var basket = new Basket();

            basket.AddItemToBasket(item1);
            basket.AddItemToBasket(item2);
            Assert.That(basket.Total, Is.EqualTo(7));
        }

        [Test]
        public void AddItemsWithPromotion()
        {
            var item1 = new Item(Sku1, 50, new Promotion(3, 130));
            var item2 = new Item(Sku2, 80);

            var basket = new Basket();
            basket.AddItemToBasket(item1);
            basket.AddItemToBasket(item1);
            basket.AddItemToBasket(item1);
            basket.AddItemToBasket(item2);

            Assert.That(basket.Total, Is.EqualTo(210));
        }
        
    }
}
