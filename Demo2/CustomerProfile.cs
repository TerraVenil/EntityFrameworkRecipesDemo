using AutoMapper;
using Demo2.Models;

namespace Demo2
{
    public class CustomerProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}