﻿// <auto-generated />
using System;
using Back_End.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Back_End.Migrations
{
    [DbContext(typeof(CrustDb_Context))]
    [Migration("20241127132737_AddFriendTb")]
    partial class AddFriendTb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Back_End.Model.Crust_db.FriendList", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong>("FriendId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("UserId");

                    b.ToTable("FriendList");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.FriendRequest", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<ulong>("RequestToId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("SenderId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("RequestToId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendRequest");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.Group", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.Messages", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("GroupId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<ulong>("SenderId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.User", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<ulong>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.UserGroups", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("GroupId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups", (string)null);
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.FriendList", b =>
                {
                    b.HasOne("Back_End.Model.Crust_db.User", "Friend")
                        .WithMany("_Friend")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Back_End.Model.Crust_db.User", "User")
                        .WithMany("Friend")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Friend");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.FriendRequest", b =>
                {
                    b.HasOne("Back_End.Model.Crust_db.User", "RequestTo")
                        .WithMany("RequestsFrom")
                        .HasForeignKey("RequestToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Back_End.Model.Crust_db.User", "Sender")
                        .WithMany("_RequestsTo")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RequestTo");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.Messages", b =>
                {
                    b.HasOne("Back_End.Model.Crust_db.Group", "Group")
                        .WithMany("Messages")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Back_End.Model.Crust_db.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.UserGroups", b =>
                {
                    b.HasOne("Back_End.Model.Crust_db.Group", "GroupInfo")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Back_End.Model.Crust_db.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupInfo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.Group", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Back_End.Model.Crust_db.User", b =>
                {
                    b.Navigation("Friend");

                    b.Navigation("Groups");

                    b.Navigation("Messages");

                    b.Navigation("RequestsFrom");

                    b.Navigation("_Friend");

                    b.Navigation("_RequestsTo");
                });
#pragma warning restore 612, 618
        }
    }
}
