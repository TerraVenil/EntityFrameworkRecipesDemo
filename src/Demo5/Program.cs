using System.Data.Entity;
using System.Diagnostics;

namespace Demo3
{
    class Program
    {
        static void Main(string[] args)
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

                dbContext.ExcludeProductPropertiesFromUpdate(product);

                //dbContext.Configuration.ValidateOnSaveEnabled = false;

                dbContext.SaveChanges();
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
