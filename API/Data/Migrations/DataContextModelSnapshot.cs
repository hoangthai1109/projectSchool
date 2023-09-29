﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ListQaCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("userName")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("appUsers");
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
#pragma warning restore 612, 618
        }
    }
}
