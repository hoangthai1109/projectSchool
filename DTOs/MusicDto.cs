using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class MusicDto
    {
        [Required]
        public string owner {get;set;}
        public string MusicCode {get;set;}
        public string musicName {get;set;}
        public string SongPath {get;set;}
        public string TrustSongPath {get;set;}
        public int isMain {get;set;}
        public string Url{get;set;}
        public int isAlbum {get;set;}
        public string? AlbumName{get;set;}
        public string? AlbumImageUrl {get;set;}
        public int TotalSong {get;set;}
        public int TotalListen {get;set;}
        public string MusicType {get;set;}
        public string MusicStatus {get;set;}
        public DateTime ReleaseDate {get;set;}
        public DateTime CreateDate {get;set;}
        public DateTime ModifiedDate {get;set;}
        public string ModifiedBy {get;set;}
    }
}