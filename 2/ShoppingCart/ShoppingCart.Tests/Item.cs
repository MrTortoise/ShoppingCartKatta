namespace ShoppingCart.Tests
{
    internal class Item
    {
        private string sku1;
        private int price;

        public Item(string sku1, int price)
        {
            this.sku1 = sku1;
            this.price = price;
        }

        public int Price => price;
    }
}