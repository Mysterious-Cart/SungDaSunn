using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using HotChocolate;

namespace Back_End.Model.Crust_db;
public class Group : IGroups
{
    [GraphQLType("Int!")]
    public ulong Id { get; set; }

	public required string Name { get; set; }
    public string Description { get; set; } = "";

    public required ICollection<UserGroups> Users { get; set; }
    public required ICollection<Messages> Messages { get; set; }
}