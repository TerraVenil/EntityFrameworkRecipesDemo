using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace Demo6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Future queries
            LoadingOptimization();

            // Load-Update scenario
            //LoadUpdateOptimization();
        }

        private static void LoadingOptimization()
        {
            using (var dbContext = GetDbContext())
            {
                var employeeId = 3;
                // Step 1
                var employees = dbContext.Employees.Include(x => x.Orders).Where(x => x.EmployeeID == employeeId);

                // Step 2
                /*var employees = dbContext.Employees.Where(x => x.EmployeeID == employeeId);
                var orders = employees.SelectMany(x => x.Orders);

                employees.ToList();
                orders.ToList();
                */

                // Step 3
                /*
                var employees = dbContext.Employees.Where(x => x.EmployeeID == employeeId).Future();
                var orders = dbContext.Employees.Where(x => x.EmployeeID == employeeId).SelectMany(x => x.Orders).Future();
                orders.ToList();
                */
            }
        }

        private static void LoadUpdateOptimization()
        {
            using (var dbContext = GetDbContext())
            {
                var employeeId = 3;
                var title = "Sales Representative";

                var employees = dbContext.Employees
                    .Where(x => x.EmployeeID == employeeId)
                    .Update(x => new Employee { Title = title });
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
