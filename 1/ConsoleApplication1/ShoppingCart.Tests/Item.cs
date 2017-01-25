namespace ShoppingCart.Tests
{
    internal class Item
    {
        private int price;
        private string name;
        private Promotion promotion;

        public int Price => price;

        public Item(string name,int price)
        {           
            this.name = name;
            this.price = price;
        }

        public Item(string name, int price, Promotion promotion) : this(name, price)
        {
            this.promotion = promotion;
        }
    }
}