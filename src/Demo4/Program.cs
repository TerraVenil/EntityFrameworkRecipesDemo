using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            LazyLoadingSample();
            //EagerLoadingSample();
            //ExplicitLoadingSample();
        }

        private static void LazyLoadingSample()
        {
            using (var dbContext = GetLazyDbContext())
            {
                var customerId = "ANTON";
                var customer = dbContext.Customers.Where(x => x.CustomerID == customerId);

                var customerModels = customer.ToCustomerModels().ToList();
            }
        }

        private static NorthwindEntities GetLazyDbContext()
        {
            var dbContext = new NorthwindEntities();

            dbContext.Database.Log = s => Debug.WriteLine(s);

            dbContext.Configuration.LazyLoadingEnabled = true;
            dbContext.Configuration.ProxyCreationEnabled = true;

            return dbContext;
        }

        private static void EagerLoadingSample()
        {
            using (var dbContext = GetDbContext())
            {
                var customerId = "ANTON";
                var customer = dbContext.Customers
                    .Include(x => x.Orders)
                    .Single(x => x.CustomerID == customerId);

                var customerModel = customer.ToCustomerModel();
            }
        }

        private static void ExplicitLoadingSample()
        {
            using (var dbContext = GetDbContext())
            {
                var categoryId = 7;
                var product = new Product
                              {
                                  ProductID = 1,
                                  ProductName = "Chai",
                                  SupplierID = 1,
                                  CategoryID = categoryId, // 1
                                  QuantityPerUnit = "10 boxes x 20 bags",
                                  UnitPrice = 18.00M,
                                  UnitsInStock = 39,
                                  UnitsOnOrder = 0,
                                  ReorderLevel = 10,
                                  Discontinued = false
                              };

                dbContext.Set<Product>().Attach(product);
                dbContext.Entry(product).State = EntityState.Modified;

                dbContext.SaveChanges();

                dbContext.Entry(product).Reference(x => x.Category).Load();
            }
        }

        private static NorthwindEntities GetDbContext()
        {
            var dbContext = new NorthwindEntities();

            dbContext.Database.Log = s => Debug.WriteLine(s);

            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ProxyCreationEnabled = false;

            return dbContext;
        }
    }
}
