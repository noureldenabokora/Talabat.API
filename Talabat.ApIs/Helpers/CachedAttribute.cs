using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing.Printing;
using System.Text;
using Talabat.Core.Services;

namespace Talabat.ApIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        // this context have all things related with end point which work on it 
        // this method work before end point start work and after data model bindind to paramater
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // create object by dependece injection 
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheSrevice>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            
            var cachedResponse = await cachedService.GetCachedResponseAsync(cacheKey);
         
            // if it cached
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contectResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200,
                };
                context.Result = contectResult;
                return;
            }
            
            // ref to end point   will execute endpoint
             var executedEndpointContext =  await next();
                                                                // his name 
            if (executedEndpointContext.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds)); 
            }
        
        }


        // want this key be has meaning name and be uniq
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
             //           PATH
            // {url}/api/Producst?pageIndex=1&pageSize=6&sort=name

            // use stingbukder here to avoid string take many places in heap
            var keyBuilder = new StringBuilder();

          
            keyBuilder.Append(request.Path); //api/Producst


            // pageIndex = 1
            // pageSize = 6
            // sort = name

            //   /api/Producst
            foreach (var (key,value) in request.Query.OrderBy(x=> x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
                //    /api/Producst|pageIndex-1
                //    /api/Producst|pageIndex-1|pageSize-6
                //    /api/Producst|pageIndex-1|pageSize-6|sort-name

                // every loop add pair of key and value
            }
            return keyBuilder.ToString();
        }
    }
}
