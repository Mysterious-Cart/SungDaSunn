using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Model.Crust_db;
public class Messages
{
    [GraphQLType(typeof(UnsignedLongType))] 
    public ulong Id { get; set; }

    [GraphQLType(typeof(UnsignedLongType))]
    public ulong SenderId {get; set;}
    
    public User? Sender {get; set;}

    [GraphQLType(typeof(UnsignedLongType))]
    public ulong GroupId { get; set; }
    public Group? Group { get; set; }
    public DateTime DateTime { get; set; }
    public string? Message { get; set; }
}