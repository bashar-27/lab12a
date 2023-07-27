﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lab12a.Data;

#nullable disable

namespace lab12a.Migrations
{
    [DbContext(typeof(AsyncInnContext))]
    partial class AsyncInnContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("lab12a.Models.Amenities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AmenitiesId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AmenitiesId");

                    b.ToTable("amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "AC"
                        },
                        new
                        {
                            Id = 2,
                            Name = "coffeeBar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Fridge"
                        });
                });

            modelBuilder.Entity("lab12a.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Amman",
                            Country = "JOR",
                            Name = "Royal",
                            Phone = "079****",
                            State = "Zahran",
                            StreetAddress = "5th.Circle"
                        },
                        new
                        {
                            Id = 2,
                            City = "Amman",
                            Country = "JOR",
                            Name = "4Seasons",
                            Phone = "079****",
                            State = "KFC",
                            StreetAddress = "5th.Circle"
                        },
                        new
                        {
                            Id = 3,
                            City = "Amman",
                            Country = "JOR",
                            Name = "Reds Carlton",
                            Phone = "077****",
                            State = "SHEC",
                            StreetAddress = "5th.Circle"
                        });
                });

            modelBuilder.Entity("lab12a.Models.HotelRoom", b =>
                {
                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<bool>("PetFriendly")
                        .HasColumnType("bit");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("HotelId", "RoomNumber");

                    b.HasIndex("RoomId");

                    b.ToTable("hotel_rooms");
                });

            modelBuilder.Entity("lab12a.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("rooms");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Layout = 1,
                            Name = "honey"
                        },
                        new
                        {
                            ID = 2,
                            Layout = 2,
                            Name = "red"
                        },
                        new
                        {
                            ID = 3,
                            Layout = 3,
                            Name = "white"
                        });
                });

            modelBuilder.Entity("lab12a.Models.RoomAmenities", b =>
                {
                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("AmenitiesID")
                        .HasColumnType("int");

                    b.HasKey("RoomID", "AmenitiesID");

                    b.HasIndex("AmenitiesID");

                    b.ToTable("room_amenities");
                });

            modelBuilder.Entity("lab12a.Models.Amenities", b =>
                {
                    b.HasOne("lab12a.Models.Amenities", null)
                        .WithMany("amenities")
                        .HasForeignKey("AmenitiesId");
                });

            modelBuilder.Entity("lab12a.Models.HotelRoom", b =>
                {
                    b.HasOne("lab12a.Models.Hotel", "hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lab12a.Models.Room", "room")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");

                    b.Navigation("room");
                });

            modelBuilder.Entity("lab12a.Models.RoomAmenities", b =>
                {
                    b.HasOne("lab12a.Models.Amenities", "amenities")
                        .WithMany()
                        .HasForeignKey("AmenitiesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lab12a.Models.Room", "room")
                        .WithMany("roomAmen")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("amenities");

                    b.Navigation("room");
                });

            modelBuilder.Entity("lab12a.Models.Amenities", b =>
                {
                    b.Navigation("amenities");
                });

            modelBuilder.Entity("lab12a.Models.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("lab12a.Models.Room", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("roomAmen");
                });
#pragma warning restore 612, 618
        }
    }
}
