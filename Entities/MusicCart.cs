using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("MusicCart")]
    public class MusicCart
    {
        [Key]
        public int Id {get;set;}
        public string MusicCode {get;set;}
        public string CartCode {get;set;}
    }
}