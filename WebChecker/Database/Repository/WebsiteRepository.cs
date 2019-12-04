using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Database.Entity;
using WebChecker.Model;

namespace WebChecker.Database.Repository
{
    public class WebsiteRepository
    {
        public void Add(Website website)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.WebsiteEntities.Add(new WebsiteEntity()
                {
                    MainUrl = website.MainUrl,
                    PriceXPath = website.PriceXPath,
                    NameXPath = website.NameXPath,
                    CartButtonXPatch = website.CartButtonXPatch
                });
                dbContext.SaveChanges();
            }
        }

        public List<Website> GetAll()
        {
            var list = new List<Website>();
            using (var dbContext = new AppDbContext())
            {
                list.AddRange(dbContext.WebsiteEntities.Select(dbContextWebsiteEntity => new Website()
                {
                    MainUrl = dbContextWebsiteEntity.MainUrl,
                    PriceXPath = dbContextWebsiteEntity.PriceXPath,
                    NameXPath = dbContextWebsiteEntity.NameXPath,
                    CartButtonXPatch = dbContextWebsiteEntity.CartButtonXPatch,
                    Id = dbContextWebsiteEntity.Id
                }));
            }

            return list;
        }

        public void Edit(Website website)
        {
            using (var dbContext = new AppDbContext())
            {
                var websiteEntity = dbContext.WebsiteEntities.Single(x => x.Id == website.Id);
                websiteEntity.MainUrl = website.MainUrl;
                websiteEntity.CartButtonXPatch = website.CartButtonXPatch;
                websiteEntity.NameXPath = website.NameXPath;
                websiteEntity.PriceXPath = website.PriceXPath;
                dbContext.SaveChanges();
            }
        }

        public void Delete(Website website)
        {
            using (var dbContext = new AppDbContext())
            {
                dbContext.WebsiteEntities.Remove(new WebsiteEntity()
                {
                    Id = website.Id,
                    CartButtonXPatch = website.CartButtonXPatch,
                    MainUrl = website.MainUrl,
                    NameXPath = website.NameXPath,
                    PriceXPath = website.PriceXPath
                });
                dbContext.SaveChanges();
            }
        }
        
    }
}