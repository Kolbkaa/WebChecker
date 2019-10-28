namespace WebChecker.Model
{
    internal class Product
    {
        public Product(string name, string price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get; private set; }
        public string Price { get; private set; }
    }
}