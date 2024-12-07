using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using HotChocolate;

namespace Back_End.Model.Crust_db;
public class Group : IGroup
{
	public required string Name { get; set; }
    public string Description { get; set; } = "";
}