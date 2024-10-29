using Talabat.ApIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.ApIs.Extension
{
    public static class ApplicationServicesExtension
    {    
        //extension method  must be static
                                               //this here to make parameter called method 
        public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
        {
            //allow  caching configuration
            services.AddSingleton<IResponseCacheSrevice,ResponseCacheService>();
            
            //allow auto mapper configuration
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IpaymentService, PaymentService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrderService, OrderService>();
            // register to allow CLR inject object  
            services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            // register to allow CLR inject object  
        //    services.AddScoped(typeof(IGenricRepository<>), typeof(GenricRepository<>));
            // by gertrate token
            services.AddScoped<ITokenService,TokenService>();
            return services;
        }
    }
}
