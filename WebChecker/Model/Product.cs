using System;
using System.Collections.Generic;
using System.Linq;

namespace WebChecker.Model
{
    public class Product
    {
        private string _price;
        private DateTime _checkDate;

        public Product(string link, string name, string price, DateTime date)
        {
            Name = name;
            Price = price;
            Link = link;
            CheckDate = date;
        }

        public string Name { get; private set; }
        public string Price
        {
            get => _price;
            private set
            {
                _price = new string(value.Where(x => (char.IsDigit(x) || x == ',' || x == '.')).ToArray()).Replace(",", ".");
            }
        }
        public string Link { get; private set; }

        public DateTime CheckDate
        {
            get => _checkDate.Date;
            set => _checkDate = value;
        }

        public static IEqualityComparer<Product> PriceNameComparer { get; } = new PriceNameEqualityComparer();
        private sealed class PriceNameEqualityComparer : IEqualityComparer<Product>
        {
            public bool Equals(Product x, Product y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x._price == y._price && x.Name == y.Name;
            }

            public int GetHashCode(Product obj)
            {
                unchecked
                {
                    return ((obj._price != null ? obj._price.GetHashCode() : 0) * 397) ^ (obj.Name != null ? obj.Name.GetHashCode() : 0);
                }
            }
        }
    }
}