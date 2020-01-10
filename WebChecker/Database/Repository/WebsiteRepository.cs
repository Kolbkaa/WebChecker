using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Database.Entity;
using WebChecker.Model;
using WebChecker.Tool;

namespace WebChecker.Database.Repository
{
    public class WebsiteRepository
    {
        public void Add(Website website)
        {
            try
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
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
        }

        public List<Website> GetAll()
        {
            var list = new List<Website>();
            try
            {
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
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }

            return list;
        }

        public void Edit(Website website)
        {
            try
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
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
        }

        public void Delete(Website website)
        {
            try
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
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
        }

    }
}