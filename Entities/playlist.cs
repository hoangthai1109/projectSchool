using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities {
    [Table("Playlist")]
    public class PlayList {
        public int Id {get;set;}
        public string PlayListCode {get;set;}
        public string PlaylistName {get;set;}
        public string ImagePlaylist {get;set;}
        public int Rating {get;set;}
        public int TotalSong {get;set;}
        public int TotalListon {get;set;}
        public string PlaylistType {get;set;}
        public DateTime CreatedDate {get;set;}
    }
}