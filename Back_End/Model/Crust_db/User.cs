using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Model.Crust_db
{
	public class User
	{
        [GraphQLType("Int!")]
        public ulong Id { get; set; }
		public required string Name { get; set; }
		public string Email { get; set; } = "";
		public required string Password { get; set; }
		public ICollection<UserGroups>? Groups { get; set; }

		public ICollection<Messages>? Messages {get; set;}

		public ICollection<FriendList>? Friend {get; set;}

		[AllowNull]
		public ICollection<FriendList> _Friend {get; set;}

		public ICollection<FriendRequest>? RequestsFrom {get; set;}

		[AllowNull]
		public ICollection<FriendRequest> _RequestsTo {get; set;}
	}
}