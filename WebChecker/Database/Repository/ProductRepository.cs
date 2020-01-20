﻿using System;
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
    public class ProductRepository
    {
        public void SaveAll(Dictionary<string, Product> products)
        {
            try
            {
                using (var dbContext = new AppDbContext())
                {

                    foreach (var product in products)
                    {
                        dbContext.ProductEntity.Add(new ProductEntity()
                        {
                            CheckDate = product.Value.CheckDate,
                            Name = product.Value.Name,
                            Price = product.Value.Price,
                            Link = product.Key
                        });
                    }

                    dbContext.SaveChanges();
                }

            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }

        }

        public List<Product> GetProductsByUrl(string url)
        {
            var list = new List<Product>();
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var products = dbContext.ProductEntity?.Where(x => x.Link.Contains(url))?.ToList();
                    list.AddRange(products.Select(productEntity => new Product(productEntity.Link, productEntity.Name,
                        productEntity.Price, productEntity.CheckDate)));
                }
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }


            return list;
        }
        public List<Product> GetProductsByUrlAndName(string url, string name)
        {
            var list = new List<Product>();

            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var products = dbContext.ProductEntity?.Where(x => x.Link.Contains(url) && x.Name.Contains(name))?.ToList();
                    list.AddRange(products.Select(productEntity => new Product(productEntity.Link, productEntity.Name, productEntity.Price, productEntity.CheckDate)));
                }
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
            return list;
        }
        public List<Product> GetProductsByName(string name)
        {
            var list = new List<Product>();
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var products = dbContext.ProductEntity?.Where(x => x.Name.Equals(name))?.ToList();
                    list.AddRange(products.Select(productEntity => new Product(productEntity.Link, productEntity.Name, productEntity.Price, productEntity.CheckDate)));
                }
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
            return list;
        }
        public List<Product> GetProductsByNameFromYear(string name)
        {
            var list = new List<Product>();
            try
            {
                var endDate = DateTime.Now;
                var startDate = endDate.AddYears(-1);
                using (var dbContext = new AppDbContext())
                {
                    var products = dbContext.ProductEntity?.Where(x => x.Name.Equals(name) && x.CheckDate >= startDate && x.CheckDate <= endDate)?.ToList();
                    list.AddRange(products.Select(productEntity => new Product(productEntity.Link, productEntity.Name, productEntity.Price, productEntity.CheckDate)));
                }
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
            return list;
        }
        public List<Product> GetProductsByNameFromMonth(string name)
        {
            var list = new List<Product>();
            try
            {
                var endDate = DateTime.Now;
                var startDate = endDate.AddMonths(-1);
                using (var dbContext = new AppDbContext())
                {
                    var products = dbContext.ProductEntity?.Where(x => x.Name.Equals(name) && x.CheckDate >= startDate && x.CheckDate <= endDate)?.ToList();
                    list.AddRange(products.Select(productEntity => new Product(productEntity.Link, productEntity.Name, productEntity.Price, productEntity.CheckDate)));
                }
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
            return list;
        }

        public Dictionary<string,Product> GetProductsByUrlFromDate(string name, DateTime date)
        {
            var dictionary = new Dictionary<string, Product>();
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    var products = dbContext.ProductEntity?.Where(x => x.Name.Equals(name) && x.CheckDate == date )?.ToList();

                    foreach (var product in products)
                    {
                        dictionary.Add(product.Name, new Product(product.Link, product.Name, product.Price, product.CheckDate));
                      
                    }                    
                }
            }
            catch (SqlException e)
            {
                Error.ShowError(e.Message);
            }
            return dictionary;
        }
    }
}
