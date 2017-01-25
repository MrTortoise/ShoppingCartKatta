using System;

namespace ShoppingCart
{
    internal class Promotion
    {
        private Item item;
        private int quantity;
        private int price;

        public Item Item => item;

        public Promotion(Item item, int quantity, int price)
        {
            this.item = item;
            this.quantity = quantity;
            this.price = price;
        } 

        internal int Apply(int quantity)
        {
            
            int rem = quantity - this.quantity;
            if (rem >= 0)
            {
                return rem * item.Price + price;
            }
            else
                return item.Price * quantity;
        }
    }
}