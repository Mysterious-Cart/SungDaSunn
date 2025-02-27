﻿using Microsoft.EntityFrameworkCore;
using Back_End.Model.Crust_db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Back_End.Model;

public class CrustDb_Context : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<UserGroups> UserGroups { get; set; }
    public DbSet<FriendList> FriendLists {get; set;}
    public DbSet<FriendRequest> FriendRequests {get; set;}
    public DbSet<LoginToken> SessionToken {get; set;}
    public CrustDb_Context()
    {

    }

    public CrustDb_Context(DbContextOptions<CrustDb_Context> options) : base(options)
    {
 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var UserGroup = modelBuilder.Entity<UserGroups>();
        UserGroup.ToTable("UserGroups");
        UserGroup.HasKey(i => new { i.UserId, i.GroupId });
        UserGroup.Property(i => i.GroupId).IsRequired();
        UserGroup.Property(i => i.UserId).IsRequired();
        UserGroup.HasOne(i => i.User).WithMany(i => i.Groups).HasForeignKey(i => i.UserId);
        UserGroup.HasOne(i => i.GroupInfo).WithMany(i => i.Users).HasForeignKey(i => i.GroupId);

        var Group = modelBuilder.Entity<Group>();
        Group.Property(id => id.Id).IsRequired();
        Group.Property(name => name.Name).IsRequired();
        Group.HasKey(i => i.Id);
        Group.HasMany(i => i.Messages).WithOne(i => i.Group);

        var User = modelBuilder.Entity<User>();
        User.Property(i => i.Id).IsRequired();
        User.Property(i => i.Name).IsRequired();
        User.Property(i => i.Password).IsRequired();
        User.HasKey(i => i.Id);

        var Messages = modelBuilder.Entity<Messages>();
        Messages.Property(i => i.Id).IsRequired();
        Messages.Property(i => i.SenderId).IsRequired();
        Messages.Property(i => i.GroupId).IsRequired();
        Messages.Property(i => i.DateTime).IsRequired();
        Messages.HasOne(i => i.Group).WithMany(i => i.Messages);
        Messages.HasKey(i => i.Id);
        Messages.HasOne(i => i.Sender).WithMany(i => i.Messages);

        var FriendList = modelBuilder.Entity<FriendList>();
        FriendList.HasKey(i => i.Id);
        FriendList.HasOne(i => i.User).WithMany(i => i.Friends_List).HasForeignKey(i => i.UserId);
        FriendList.HasOne(i => i.Friend).WithMany(i => i.Friended_List).HasForeignKey(i => i.FriendId);

        var FriendRequest = modelBuilder.Entity<FriendRequest>();
        FriendRequest.HasKey(i => i.Id);
        FriendRequest.HasOne(i => i.Sender).WithMany(i => i.RequestsTo_List).HasForeignKey(i => i.SenderId);
        FriendRequest.HasOne(i => i.RequestTo).WithMany(i => i.RequestsFrom_List).HasForeignKey(i => i.RequestToId);

        var LoginToken = modelBuilder.Entity<LoginToken>();
        LoginToken.HasKey(i => i.SessionId);
        LoginToken.HasOne(i => i.user);
    }
}