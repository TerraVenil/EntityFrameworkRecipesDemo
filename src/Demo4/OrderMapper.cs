using Demo4.Models;

namespace Demo4
{
    public static class OrderMapper
    {
        public static OrderModel ToOrderModel(this Order order)
        {
            return new OrderModel { OrderID = order.OrderID };
        }
    }
}