using System;

namespace ShoppingCart.Tests
{
    internal class Promotion
    {
        private int number;
        private int price;

        public Promotion(int number, int price)
        {
            this.number = number;
            this.price = price;
        }

        internal int Apply(int quantity)
        {
            throw new NotImplementedException();
        }
    }
}