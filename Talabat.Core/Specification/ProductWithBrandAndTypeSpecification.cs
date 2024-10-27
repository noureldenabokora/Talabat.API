using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification: BaseSpecification<Product>
    {
        //constructor used for GET ALL PRODUCTS  
        public ProductWithBrandAndTypeSpecification(ProductSpecParams specParams)
            : base(
                  //here to apply filitration
                  p =>    
                  // to apply search  
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId.Value) &&
            (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId.Value)
            
            )
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
            
            // for sort
            if(!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p=> p.Name);
                        break;
                }
            }


            // for pagination 

            // عايز 3 صفحة 
            // totalproducts = 100
            // pagesize = 10
            // pageindex = 3 
            ApplyPagination(specParams.PageSize * (specParams.PageIndex-1), specParams.PageSize);
        }

        //constructor used for GET SPECIFIC PRODUCT
        public ProductWithBrandAndTypeSpecification(int id):base(p => p.Id == id) 
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);

        }
    }
}
