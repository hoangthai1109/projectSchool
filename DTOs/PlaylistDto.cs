using API.Entities;

namespace API.DTOs
{
    public class PlaylistDto
    {
        public string PlayListCode {get;set;}
        public string PlaylistName {get;set;}
        public string ImagePlaylist {get;set;}
        public int Rating {get;set;}
        public int TotalListon {get;set;}
        public int TotalSong {get;set;}
        public string PlaylistType {get;set;}
        public DateTime CreatedDate {get;set;}
        public IList<Music> Musics {get;set;}
    }
}