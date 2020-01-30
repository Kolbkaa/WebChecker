using System;
using System.Collections.Generic;
using System.Linq;

namespace WebChecker.Model
{
    public class Product
    {
        private DateTime _checkDate;

        public Product(string link, string name, string price, DateTime date)
        {
            Name = name;
            Price = new Price(price);
            Link = link;
            CheckDate = date;
        }

        public string Name { get; private set; }
        public Price Price { get; private set; }
        public string Link { get; private set; }

        public DateTime CheckDate
        {
            get => _checkDate.Date;
            set => _checkDate = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Name == product.Name &&
                   EqualityComparer<Price>.Default.Equals(Price, product.Price);
        }

        public override int GetHashCode()
        {
            var hashCode = -44027456;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Price>.Default.GetHashCode(Price);
            return hashCode;
        }

        public static bool operator ==(Product left, Product right)
        {
            return EqualityComparer<Product>.Default.Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !(left == right);
        }
    }
}