using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Model.Crust_db;
public class Messages
{
    [GraphQLType("Int!")] 
    public ulong Id { get; set; }
    [GraphQLType("Int!")]
    public ulong GroupId { get; set; }
    public Group Group { get; set; }
    public DateTime DateTime { get; set; }
    public string Message { get; set; }
}