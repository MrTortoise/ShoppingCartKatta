namespace ShoppingCart.Tests
{
    internal class Item
    {
        private int price;
        private string name;

        public int Price => price;

        public Item(string name,int price)
        {           
            this.name = name;
            this.price = price;
        }
    }
}