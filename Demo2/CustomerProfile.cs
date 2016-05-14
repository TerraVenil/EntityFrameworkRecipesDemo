using AutoMapper;
using AutoMapper.EquivilencyExpression;
using Demo2.Models;

namespace Demo2
{
    public class CustomerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>().EqualityComparision((src, dest) => src.OrderID == dest.OrderID);

            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}