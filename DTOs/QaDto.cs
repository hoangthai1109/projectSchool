using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class QaDto
    {
        public int Id{get;set;}
        public int Question {get;set;}
        public string Answer {get;set;}
    }
}