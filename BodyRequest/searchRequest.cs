using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.BodyRequest
{
    public class SearchRequest
    {
        public string KeySearch {get;set;}
        public string CreateBy {get;set;}
        public int PageNumber {get;set;}
        public int PageSize {get;set;}
    }
}