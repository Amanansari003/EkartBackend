using AutoMapper;
using Core.Entities.Identity;
using Ekart.Dtos;
using Ekart.Entities;

namespace Ekart.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand!.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType!.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
