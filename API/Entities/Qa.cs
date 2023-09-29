using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Qa
    {
        public int Id{get;set;}
        public int Question {get;set;}
        public string Answer {get;set;}
        public int AppUserId{get;set;}
        public AppUser AppUser {get;set;}
    }
}