﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SaitynaiAPI;

namespace SaitynaiAPI.Migrations
{
    [DbContext(typeof(SaitynaiDbContext))]
    [Migration("20200922193326_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SaitynaiAPI.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Animals"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cars"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Games"
                        });
                });

            modelBuilder.Entity("SaitynaiAPI.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(10000);

                    b.Property<int>("ThreadId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Cute :)",
                            ThreadId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Body = "Fast car go vrooom haha",
                            ThreadId = 2,
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            Body = "Hahaha",
                            ThreadId = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            Body = "Damn she's cute",
                            ThreadId = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 5,
                            Body = "Cutie",
                            ThreadId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 6,
                            Body = "True",
                            ThreadId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SaitynaiAPI.Entities.Thread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasMaxLength(10000);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Threads");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "Look at her",
                            CategoryId = 1,
                            Title = "I have a cute cat",
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            Body = "Vrooom",
                            CategoryId = 2,
                            Title = "Fast car vroom vroom",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Body = "brrrrr",
                            CategoryId = 2,
                            Title = "Slow car :(",
                            UserId = 3
                        });
                });

            modelBuilder.Entity("SaitynaiAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            Password = "admin",
                            isAdmin = true
                        },
                        new
                        {
                            Id = 2,
                            Email = "user1@user.com",
                            Password = "user1",
                            isAdmin = false
                        },
                        new
                        {
                            Id = 3,
                            Email = "user2@user.com",
                            Password = "user2",
                            isAdmin = false
                        });
                });

            modelBuilder.Entity("SaitynaiAPI.Entities.Comment", b =>
                {
                    b.HasOne("SaitynaiAPI.Entities.Thread", "Thread")
                        .WithMany("Comments")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaitynaiAPI.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SaitynaiAPI.Entities.Thread", b =>
                {
                    b.HasOne("SaitynaiAPI.Entities.Category", "Category")
                        .WithMany("Threads")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SaitynaiAPI.Entities.User", "User")
                        .WithMany("Threads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
