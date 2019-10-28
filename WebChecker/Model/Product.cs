using System.Linq;

namespace WebChecker.Model
{
    internal class Product
    {
        private string price;

        public Product(string name, string price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get; private set; }
        public string Price
        {
            get => price;
            private set
            {
                price = new string(value.Where(x=> (char.IsDigit(x) || x == ',' || x=='.')).ToArray());
            }
        }
    }
}