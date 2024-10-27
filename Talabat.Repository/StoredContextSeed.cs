using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public static class StoredContextSeed
    {


        public static  async Task SeedAsync(StroreContext dbcontext)
        {
         if (!dbcontext.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(brand);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }


         // check if producttypes has any value in db as if has  don't add any thing
            if (!dbcontext.ProductTypes.Any())
            {
                //read textfile first
                var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                // convert json to list of product type
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Count > 0)
                {
                    foreach (var type in types)
                    {
                        //add type in database 
                        await dbcontext.Set<ProductType>().AddAsync(type);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Products.Any())
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await dbcontext.Set<Product>().AddAsync(product);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }
       
            if (!dbcontext.DeliveryMethods.Any())
            {
                var deleviryMethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deleviryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deleviryMethodData);
                if (deleviryMethods is not null && deleviryMethods.Count > 0)
                {
                    foreach (var deleviryMethod in deleviryMethods)
                    {
                        await dbcontext.Set<DeliveryMethod>().AddAsync(deleviryMethod);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }


        }
    }
}
