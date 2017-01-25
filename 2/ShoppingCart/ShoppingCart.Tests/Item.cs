namespace ShoppingCart.Tests
{
    internal class Item
    {
        private string sku;
        private int price;

        public Item(string sku1, int price)
        {
            this.sku = sku1;
            this.price = price;
        }

        public int Price => price;

        public object Sku => sku;
    }
}