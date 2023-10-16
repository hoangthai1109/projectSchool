using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class musicUserDto
    {
        public string MusicUserCode {get;set;}
        public DateTime CreatedDate {get;set;}
        public string MadeBy{get;set;}
        public IList<Music> Musics {get;set;}
    }
}