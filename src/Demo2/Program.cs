using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo2.Models;

namespace Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = MappingRegistrations.RegisterMappings();
            var mapper = config.CreateMapper();

            using (var dbContext = GetDbContext())
            {
                var customerId = "ALFKI";
                var customer = dbContext.Customers.Include(x => x.Orders).Single(x => x.CustomerID == customerId);

                var customerModel = mapper.Map<CustomerModel>(customer);

                customerModel.ContactTitle = "Sales Representative 1";
                customerModel.Orders.RemoveAt(0);

                mapper.Map(customerModel, customer);

                dbContext.ChangeTracker.DetectChanges();

                RemoveOrphanOrders(dbContext);

                dbContext.SaveChanges();
            }
        }

        // https://lostechies.com/jimmybogard/2014/05/08/missing-ef-feature-workarounds-cascade-delete-orphans/
        private static void RemoveOrphanOrders(NorthwindEntities dbContext)
        {
            var localOrders = dbContext.Orders.Local;
            foreach (var orphanOrder in localOrders.Where(x => x.CustomerID == null).ToList())
                localOrders.Remove(dbContext.Orders.Find(orphanOrder.OrderID));
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
