namespace Talabat.Core.Specification
{
    public class ProductSpecParams
    {
        // make const value for max size 
        private const int MaxPageSize = 10;

        //make full prop for can check for value  that fron end send it as i bigger than 10 or not 
        // take 5 default value for if fornt end didn't send value then i will send 5 products
        private int pageSize= 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        //for  page number take default vlaue 1 as if he didn't send value i will give him first page
        public int PageIndex { get; set; } = 1;


        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }


    }
}
