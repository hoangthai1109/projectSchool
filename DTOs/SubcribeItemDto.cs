using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class SubcribeItemDto
    {
        public int PakageType {get;set;}
        public string PakageDescript{get;set;}
        public string PakageValue {get;set;}
        public int UserId {get;set;}
    }
}