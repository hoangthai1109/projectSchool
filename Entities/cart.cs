using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    [Table("Cart")]
    public class Cart {
        [Key]
        public int Id {get;set;}
        public string CartCode {get;set;}
        public string createdBy {get;set;}
        public int status {get;set;}
        public int IdMusic {get;set;}
        public Music Music {get;set;}
    }
}