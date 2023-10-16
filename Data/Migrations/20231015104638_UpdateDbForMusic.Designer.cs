﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231015104638_UpdateDbForMusic")]
    partial class UpdateDbForMusic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ListQaCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleDefault")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("userName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("appUsers");
                });

            modelBuilder.Entity("API.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CartCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdMusic")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MusicId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("createdBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MusicId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("API.Entities.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CatName")
                        .HasColumnType("TEXT");

                    b.Property<string>("CatParentName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("isParent")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("API.Entities.CategoriesSubItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoriesId");

                    b.ToTable("CategoriesSubItem");
                });

            modelBuilder.Entity("API.Entities.DynamicMenu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuLv")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MenuName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MenuUri")
                        .HasColumnType("TEXT");

                    b.Property<int>("idParent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("isParent")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("role")
                        .HasColumnType("BLOB");

                    b.HasKey("id");

                    b.ToTable("dynamicMenu");
                });

            modelBuilder.Entity("API.Entities.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlbumImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("AlbumName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MusicCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("MusicType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SongPath")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalListen")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrustSongPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<int>("isAlbum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("isMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("musicName")
                        .HasColumnType("TEXT");

                    b.Property<string>("owner")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Music");
                });

            modelBuilder.Entity("API.Entities.MusicCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CartCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("MusicCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MusicCart");
                });

            modelBuilder.Entity("API.Entities.MusicPlaylist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MusicCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayListCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MusicPlayList");
                });

            modelBuilder.Entity("API.Entities.MusicPlaylistUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MusicCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayListUserCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MusicPLayListUser");
                });

            modelBuilder.Entity("API.Entities.MusicUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("MadeBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("MusicCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("ViewerSong")
                        .HasColumnType("INTEGER");

                    b.Property<int>("isArtist")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MusicUser");
                });

            modelBuilder.Entity("API.Entities.PlayList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagePlaylist")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayListCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlaylistName")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlaylistType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalListon")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalSong")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("API.Entities.PlaylistUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreateBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrlFolder")
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerPl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PLayListName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayListUserCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlaylistType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ReleaseDatePls")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalListon")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalSong")
                        .HasColumnType("INTEGER");

                    b.Property<int>("isUserUpload")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PlaylistUser");
                });

            modelBuilder.Entity("API.Entities.Qa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Answer")
                        .HasColumnType("TEXT");

                    b.Property<int>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Question")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Qa");
                });

            modelBuilder.Entity("API.Entities.SubcribeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PakageDescript")
                        .HasColumnType("TEXT");

                    b.Property<int>("PakageType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PakageValue")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SubcribeItem");
                });

            modelBuilder.Entity("API.Entities.image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AppUserId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("API.Entities.Cart", b =>
                {
                    b.HasOne("API.Entities.Music", "Music")
                        .WithMany()
                        .HasForeignKey("MusicId");

                    b.Navigation("Music");
                });

            modelBuilder.Entity("API.Entities.CategoriesSubItem", b =>
                {
                    b.HasOne("API.Entities.Categories", "Categories")
                        .WithMany("CategoriesSubItems")
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("API.Entities.Qa", b =>
                {
                    b.HasOne("API.Entities.AppUser", "AppUser")
                        .WithMany("Qas")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.image", b =>
                {
                    b.HasOne("API.Entities.AppUser", "AppUser")
                        .WithMany("Images")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Qas");
                });

            modelBuilder.Entity("API.Entities.Categories", b =>
                {
                    b.Navigation("CategoriesSubItems");
                });
#pragma warning restore 612, 618
        }
    }
}
