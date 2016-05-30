using Demo2.Models;

namespace Demo2
{
    public static class OrderMapper
    {
        public static OrderModel ToOrderModel(this Order order)
        {
            return new OrderModel { OrderID = order.OrderID };
        }
    }
}