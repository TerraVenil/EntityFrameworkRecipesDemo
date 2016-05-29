using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorThis.GraphDiff;

namespace Demo3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = GetDbContext())
            {
                var customerId = "ANATR";
                var customer = dbContext.Customers
                    .Include(x => x.Orders.Select(d => d.Order_Details))
                    .AsNoTracking().
                    Single(x => x.CustomerID == customerId);

                customer.ContactTitle = "Owner 1";
                
                var order = customer.Orders.First();
                customer.Orders.Remove(order);

                dbContext.UpdateGraph(customer, map => map.OwnedCollection(c => c.Orders, o => o.OwnedCollection(d => d.Order_Details)));

                dbContext.SaveChanges();
            }
        }

        private static NorthwindEntities GetDbContext()
        {
            var dbContext = new NorthwindEntities();

            dbContext.Configuration.AutoDetectChangesEnabled = false;
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ProxyCreationEnabled = false;

            return dbContext;
        }
    }
}
