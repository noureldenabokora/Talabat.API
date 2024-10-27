namespace Talabat.ApIs.Extension
{
    public static class SwaggerSrevicesExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


        public static WebApplication UseSwaggerMidlleWares(this WebApplication app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
