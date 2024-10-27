using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {

            var query = inputQuery; // query = _dbcontext.set<product>()

            //check if has where condition or not 
            if (spec.Criteria is not null) // p => p.id == 1
            {
                query = query.Where(spec.Criteria);
                // query = _dbcontext.set<product>().Where(p => p.id == 1)
            }

            //هضيف الاول شغل الترتيب 
            //ADD order by 
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // for pagination 
            if (spec.IsPaginationEnabled)
            {
              query = query.Skip(spec.Skip).Take(spec.Take);
            }



            //هنا هعمل اصافة لل انكلود مع الوير 
            // for each to list of includes

            query = spec.Includes.Aggregate(query, (currentquery,includeExpersion)  => currentquery.Include(includeExpersion));


            return query; 
        }
    }
}
