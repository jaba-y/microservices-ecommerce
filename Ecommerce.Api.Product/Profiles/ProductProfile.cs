using AutoMapper;
using Ecommerce.Api.Product.Db;
using Ecommerce.Api.Product.Models;

namespace Ecommerce.Api.Product.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductItem, Products>();
        }
    }
}