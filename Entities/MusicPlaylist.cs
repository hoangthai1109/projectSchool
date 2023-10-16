using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("MusicPlayList")]
    public class MusicPlaylist
    {
        [Key]
        public int Id {get;set;}
        public string MusicCode {get;set;}
        public string PlayListCode {get;set;}
    }
}