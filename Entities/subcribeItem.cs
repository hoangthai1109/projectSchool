using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    [Table("SubcribeItem")]
    public class SubcribeItem {
        [Key]
        public int Id {get;set;}
        public int PakageType {get;set;}
        public string PakageDescript{get;set;}
        public string PakageValue {get;set;}
    }
}