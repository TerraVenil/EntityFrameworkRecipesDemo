using AutoMapper;
using AutoMapper.Mappers;

namespace Demo5
{
    public static class MappingRegistrations
    {
        public static MapperConfiguration RegisterMappings()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<CollectionProfile>();
                    cfg.AddProfile<CustomerProfile>();
                });

            return config;
        }
    }
}