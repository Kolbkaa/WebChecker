using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChecker.Database.Entity
{
    class WebsiteEntity
    {
        public int Id { get; set; }
        public string MainUrl { get; set; }
        public string NameXPath { get; set; }
        public string PriceXPath { get; set; }
        public string CartButtonXPatch { get; set; }
    }
}
