using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("MusicPLayListUser")]
    public class MusicPlaylistUser
    {
        [Key]
        public int Id {get;set;}
        public string MusicCode {get;set;}
        public string PlayListUserCode {get;set;}
    }
}