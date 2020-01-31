using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChecker.Tool;

namespace WebChecker.Database.Repository
{
    class DateRepository
    {
        public DateTime[] TwoLastDateCheck(string webUrl)
        {
            DateTime[] dateTimes = null;
            try
            {
                using (var dbContext = new AppDbContext())
                {
                    dateTimes = dbContext.ProductEntity.Where(x => x.Link.Contains(webUrl)).Select(x => x.CheckDate).GroupBy(x => x).Select(x => x.Key).OrderByDescending(x => x).Take(2).ToArray();
                }

            }
            catch (SqlException e)
            {
                //Error.ShowError(e.Message);
                Debug.WriteLine(e.Message);
            }
            return dateTimes;
        }
    }
}
