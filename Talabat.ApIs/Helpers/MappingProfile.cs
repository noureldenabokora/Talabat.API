using AutoMapper;
using Talabat.ApIs.Dtos;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.ApIs.Helpers
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductToRetuenDto>()
             .ForMember( d => d.ProductBrand, o => o.MapFrom( s => s.productBrand.Name)) 
             .ForMember( d => d.ProductType, o => o.MapFrom( s => s.productType.Name))
             .ForMember(d=> d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Core.Entites.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto,CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto,Core.Entites.Order_Aggregate. Address>();
        
        }
    }
}
