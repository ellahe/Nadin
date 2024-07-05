using ApplicationService.Products;
using AutoMapper;
using Domain;

namespace ApplicationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();
        }
    }

}
