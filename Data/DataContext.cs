using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> appUsers {get;set;}
        public DbSet<DynamicMenu> dynamicMenu {get; set;}
        public DbSet<Music> Musics {get;set;}
        public DbSet<Cart> Carts {get;set;}
        public DbSet<MusicUser> MusicUsers {get;set;}
        public DbSet<SubcribeItem> SubcribeItems {get;set;}
        public DbSet<image> Images {get;set;}
        public DbSet<PlayList> PlayLists {get;set;}
        public DbSet<PlaylistUser> PlaylistUsers {get;set;}
        public DbSet<Categories> Categories {get;set;}
        public DbSet<CategoriesSubItem> CategoriesSubItems {get;set;}
        public DbSet<MusicCart> MusicCarts {get;set;}
        public DbSet<MusicPlaylist> MusicPlaylists {get;set;}
        public DbSet<MusicPlaylistUser> MusicPlaylistUsers {get;set;}
        public DbSet<MusicForUser> MusicForUsers {get;set;}
    }
}