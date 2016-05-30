namespace Demo3
{
    public static class NorthwindEntitiesExtensions
    {
        public static void ExcludeProductPropertiesFromUpdate(this NorthwindEntities dbContext, Product product)
        {
            dbContext.Entry(product).Property(p => p.ProductName).IsModified = false;
            dbContext.Entry(product).Property(p => p.SupplierID).IsModified = false;
            dbContext.Entry(product).Property(p => p.QuantityPerUnit).IsModified = false;
            dbContext.Entry(product).Property(p => p.UnitPrice).IsModified = false;
            dbContext.Entry(product).Property(p => p.UnitsInStock).IsModified = false;
            dbContext.Entry(product).Property(p => p.UnitsOnOrder).IsModified = false;
            dbContext.Entry(product).Property(p => p.ReorderLevel).IsModified = false;
            dbContext.Entry(product).Property(p => p.Discontinued).IsModified = false;
        }
    }
}