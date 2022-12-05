﻿// <auto-generated />
using System;
using Dating.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dating.Infrastructure.Migrations
{
    [DbContext(typeof(DatingDbContext))]
    [Migration("20221203172316_PicturesAdd")]
    partial class PicturesAdd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dating.Domain.Models.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Dating.Domain.Models.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Имя гендера");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("Dating.Domain.Models.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PairFk")
                        .HasColumnType("uuid");

                    b.Property<int>("PairStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserFk")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PairFk");

                    b.HasIndex("UserFk");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Dating.Domain.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatFk")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserFk")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChatFk");

                    b.HasIndex("UserFk");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Dating.Domain.Models.Pair", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatFk")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MatchedUserFk")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserFk")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChatFk");

                    b.HasIndex("MatchedUserFk");

                    b.HasIndex("UserFk");

                    b.ToTable("Pairs");
                });

            modelBuilder.Entity("Dating.Domain.Models.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Dating.Domain.Models.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasComment("Возраст пользователя");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Описание пользователя");

                    b.Property<Guid>("GenderFk")
                        .HasColumnType("uuid")
                        .HasComment("Пол пользователя");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Имя пользователя");

                    b.Property<Guid?>("PictureFk")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GenderFk");

                    b.HasIndex("PictureFk")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Dating.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Пароль пользователя");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasComment("Логин пользователя");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dating.Domain.Models.UserOrientation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GenderFk")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserFk")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GenderFk");

                    b.HasIndex("UserFk");

                    b.ToTable("UserOrientations");
                });

            modelBuilder.Entity("Dating.Domain.Models.Like", b =>
                {
                    b.HasOne("Dating.Domain.Models.Pair", null)
                        .WithMany()
                        .HasForeignKey("PairFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dating.Domain.Models.Message", b =>
                {
                    b.HasOne("Dating.Domain.Models.Chat", null)
                        .WithMany()
                        .HasForeignKey("ChatFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dating.Domain.Models.Pair", b =>
                {
                    b.HasOne("Dating.Domain.Models.Chat", null)
                        .WithMany()
                        .HasForeignKey("ChatFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MatchedUserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dating.Domain.Models.Profile", b =>
                {
                    b.HasOne("Dating.Domain.Models.Gender", null)
                        .WithMany()
                        .HasForeignKey("GenderFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Dating.Domain.Models.Profile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.Picture", null)
                        .WithOne()
                        .HasForeignKey("Dating.Domain.Models.Profile", "PictureFk");
                });

            modelBuilder.Entity("Dating.Domain.Models.UserOrientation", b =>
                {
                    b.HasOne("Dating.Domain.Models.Gender", null)
                        .WithMany()
                        .HasForeignKey("GenderFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dating.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
