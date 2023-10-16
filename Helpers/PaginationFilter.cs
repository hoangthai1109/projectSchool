using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PaginationFilter
    {
        public int PageNumber {get;set;}
        public int PageSize {get;set;}
        public string? KeySearch {get;set;}

        public PaginationFilter() {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.KeySearch = String.Empty;
        }

        public PaginationFilter(int PageNumber, int PageSize) {
            this.PageNumber = PageNumber < 1 ? 1 : PageNumber;
            this.PageSize = PageSize > 10 ? 10 : PageSize;
        }
    }
}