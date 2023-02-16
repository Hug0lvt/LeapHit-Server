﻿// <auto-generated />
using System;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBase.Migrations
{
    [DbContext(typeof(PongDbContextWithStub))]
    partial class PongDbContextWithStubModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("DataBase.Entity.Chat", b =>
                {
                    b.Property<int>("chatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("player1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("player2")
                        .HasColumnType("INTEGER");

                    b.HasKey("chatId");

                    b.HasIndex("player1");

                    b.HasIndex("player2");

                    b.ToTable("Chat");

                    b.HasData(
                        new
                        {
                            chatId = 1,
                            player1 = 1,
                            player2 = 2
                        });
                });

            modelBuilder.Entity("DataBase.Entity.Game", b =>
                {
                    b.Property<int>("gameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("durationGame")
                        .HasColumnType("INTEGER");

                    b.Property<int>("loser")
                        .HasColumnType("INTEGER");

                    b.Property<int>("nbMaxEchanges")
                        .HasColumnType("INTEGER");

                    b.Property<int>("winner")
                        .HasColumnType("INTEGER");

                    b.HasKey("gameId");

                    b.HasIndex("loser");

                    b.HasIndex("winner");

                    b.ToTable("Game");

                    b.HasData(
                        new
                        {
                            gameId = 1,
                            durationGame = 65,
                            loser = 2,
                            nbMaxEchanges = 5,
                            winner = 1
                        });
                });

            modelBuilder.Entity("DataBase.Entity.Message", b =>
                {
                    b.Property<int>("messageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("chat")
                        .HasColumnType("INTEGER");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("player")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("messageId");

                    b.HasIndex("chat");

                    b.HasIndex("player");

                    b.ToTable("Message");

                    b.HasData(
                        new
                        {
                            messageId = 1,
                            chat = 1,
                            message = "Salut mon gars !",
                            player = 1,
                            timestamp = new DateTime(2023, 2, 16, 17, 5, 12, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            messageId = 2,
                            chat = 1,
                            message = "Comment tu vas ?",
                            player = 2,
                            timestamp = new DateTime(2023, 2, 16, 17, 12, 35, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("DataBase.Entity.Player", b =>
                {
                    b.Property<int>("playerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("nbBallTouchTotal")
                        .HasColumnType("INTEGER");

                    b.Property<int>("timePlayed")
                        .HasColumnType("INTEGER");

                    b.HasKey("playerId");

                    b.ToTable("Player");

                    b.HasData(
                        new
                        {
                            playerId = 1,
                            name = "Rami",
                            nbBallTouchTotal = 20,
                            timePlayed = 120
                        },
                        new
                        {
                            playerId = 2,
                            name = "Hugo",
                            nbBallTouchTotal = 90,
                            timePlayed = 250
                        });
                });

            modelBuilder.Entity("DataBase.Entity.Chat", b =>
                {
                    b.HasOne("DataBase.Entity.Player", "PlayerId1")
                        .WithMany()
                        .HasForeignKey("player1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Entity.Player", "PlayerId2")
                        .WithMany()
                        .HasForeignKey("player2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerId1");

                    b.Navigation("PlayerId2");
                });

            modelBuilder.Entity("DataBase.Entity.Game", b =>
                {
                    b.HasOne("DataBase.Entity.Player", "PlayerLoser")
                        .WithMany()
                        .HasForeignKey("loser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Entity.Player", "PlayerWinner")
                        .WithMany()
                        .HasForeignKey("winner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerLoser");

                    b.Navigation("PlayerWinner");
                });

            modelBuilder.Entity("DataBase.Entity.Message", b =>
                {
                    b.HasOne("DataBase.Entity.Chat", "ChatId")
                        .WithMany()
                        .HasForeignKey("chat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataBase.Entity.Player", "PlayerId")
                        .WithMany()
                        .HasForeignKey("player")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatId");

                    b.Navigation("PlayerId");
                });
#pragma warning restore 612, 618
        }
    }
}
