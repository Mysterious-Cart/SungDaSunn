using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using HotChocolate;

namespace Back_End.Model.Crust_db;
public class Group
{
    [GraphQLType(typeof(UnsignedLongType))]
    public ulong Id { get; set; }
	public required string Name { get; set; }
    public string Description { get; set; } = "";

    public ICollection<UserGroups>? Users { get; set; }
    public ICollection<Messages>? Messages { get; set; }
}