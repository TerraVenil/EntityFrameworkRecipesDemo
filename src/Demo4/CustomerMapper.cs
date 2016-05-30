using System.Collections.Generic;
using System.Linq;
using Demo2.Models;

namespace Demo2
{
    public static class CustomerMapper
    {
        public static CustomerModel ToCustomerModel(this Customer customer)
        {
            return new CustomerModel { CustomerId = customer.CustomerID, Orders = customer.Orders.Select(x => x.ToOrderModel()) };
        }

        public static IEnumerable<CustomerModel> ToCustomerModels(this IEnumerable<Customer> customer)
        {
            return customer.Select(x => x.ToCustomerModel());
        }
    }
}