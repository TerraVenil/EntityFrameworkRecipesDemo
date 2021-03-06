﻿using System.Data.Entity;
using System.Linq;
using Demo5.Models;

namespace Demo5
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = MappingRegistrations.RegisterMappings();
            var mapper = config.CreateMapper();

            using (var dbContext = GetDbContext())
            {
                var customerId = "AROUT";
                var customer = dbContext.Customers.Include(x => x.Orders.Select(o => o.Order_Details)).Single(x => x.CustomerID == customerId);

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
            var orphanOrders = localOrders.Where(x => x.CustomerID == null).ToList();
            foreach (var orphanOrder in orphanOrders)
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
