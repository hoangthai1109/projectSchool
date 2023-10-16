using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    [Table("MusicUser")]
    public class MusicUser {
        [Key]
        public int Id {get;set;}
        public string MusicUserCode {get;set;}
        public DateTime CreatedDate {get;set;}
        public string MadeBy{get;set;}
    }
}