using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChecker.Model
{
    public class Price
    {
        public int Id { get; set; }
        public int? Unity { get; private set; }
        public int? Dec { get; private set; }

        public Price() { }
        public Price(string value)
        {
            if (value == null)
            {
                Unity = null;
                Dec = null;
                return;
            }

            string newValue = ConvertToRightFormat(ref value);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                Unity = null;
                Dec = null;
                return;
            }
            var tempArr = newValue.Split('.');
            Unity = int.Parse(tempArr[0]);

            if (tempArr.Length == 2)
            {
                Dec = int.Parse(tempArr[1]);
            }
            else
            {
                Dec = 0;
            }

        }

        private static string ConvertToRightFormat(ref string value)
        {
            var newListChar = new List<char>();
            bool findFirstDot = false;

            value = value.Replace(',', '.');
            var listChar = new List<char>(value.ToCharArray());
            listChar.Reverse();

            foreach (var c in listChar)
            {
                if (c == '.' && findFirstDot == false)
                {
                    findFirstDot = true;
                    newListChar.Add(c);
                }
                else if (char.IsDigit(c))
                {
                    newListChar.Add(c);
                }
            }
            newListChar.Reverse();
            return new string(newListChar.ToArray());
        }

        public decimal? GetDecimal()
        {
            if (Dec == null && Unity == null)
                return null;
            return (decimal)(Unity + (Dec / 100m));
        }

        public override string ToString()
        {
            return GetDecimal().ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Price price &&
                   Unity == price.Unity &&
                   Dec == price.Dec;
        }

        public override int GetHashCode()
        {
            var hashCode = -1342795395;
            hashCode = hashCode * -1521134295 + Unity.GetHashCode();
            hashCode = hashCode * -1521134295 + Dec.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Price left, Price right)
        {
            return EqualityComparer<Price>.Default.Equals(left, right);
        }

        public static bool operator !=(Price left, Price right)
        {
            return !(left == right);
        }
    }
}
