using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChecker.Database.Entity
{
    class ProductEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
