using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class PlaylistUserDto
    {
        public string PlayListUserCode {get;set;}
        public string PLayListName {get;set;}
        public string OwnerPl {get;set;}
        public string? ImageUrlFolder {get;set;}
        public int isUserUpload {get;set;}
        public string Status {get;set;}
        public int Rating {get;set;}
        public int TotalListon {get;set;}
        public int TotalSong {get;set;}
        public string PlaylistType {get;set;}
        public DateTime ReleaseDatePls {get;set;}
        public DateTime CreatedDate {get;set;}
        public string CreateBy {get;set;}
        public IList<Music> Musics {get;set;}
    }
}