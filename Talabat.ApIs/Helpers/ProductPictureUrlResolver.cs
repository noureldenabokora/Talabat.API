﻿using AutoMapper;
using Talabat.ApIs.Dtos;
using Talabat.Core.Entites;
using static System.Net.WebRequestMethods;

namespace Talabat.ApIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToRetuenDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToRetuenDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))

                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";

            return string.Empty ;
            
        }
    }
}
