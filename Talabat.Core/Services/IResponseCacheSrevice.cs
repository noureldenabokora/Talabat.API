using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponseCacheSrevice
    {
        //                       // store data as key and value         // time this date apper in ram then deleted 
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
