using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{

   // بحاول احول جملة الكويري بتاع انكليود لبروبرتي دينامك
    public interface ISpecification<T> where T : BaseEntity
    {
        // exeprission for where condition هنا اكسبريشن عشان تعبر عن اللامدا اللي جوه الشرط  الوير 

        // بتاخد تي عشان اي كان برودكت او تايب اواي كان وهترجع بولين يترو ي فولس 
        //take t as a product or type or any thing and return bool 
        public Expression<Func<T,bool>> Criteria { get; set; }

        // list return list of exeprission takes func of T and return object 
        // ليسته من انكليود حلو  بتاخد فن سي بتاخد 
        // t وترجع اوبجكت 
        public List<Expression<Func<T,object>>> Includes { get; set; }

        // to order by make expersion take func that take T and return object (name or price)
        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDescending { get; set; }

        // for skip using Pagination
        public int Skip { get; set; }
        //for take using pagination
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }
}
