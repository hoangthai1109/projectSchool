using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class DynamicMenu
    {
        public int id {get; set;}
        public byte[] role {get; set;}
        public string MenuName {get; set;}
        public string MenuUri {get; set;}
        public int isParent {get; set;}
        public int idParent {get; set;}
        public int MenuLv {get; set;}
    }
}