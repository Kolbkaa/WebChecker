using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Model;

namespace WebChecker.Database.Entity
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public Price Price { get; set; }
        public DateTime CheckDate { get; set; }

    }
}
