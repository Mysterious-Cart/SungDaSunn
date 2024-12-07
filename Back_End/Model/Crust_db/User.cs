using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Model.Crust_db
{
	public class User : IUser
	{
		[GraphQLType(typeof(UnsignedLongType))]
        public ulong Id { get; set; }
		public required string Name { get; set; }

		[GraphQLType(typeof(EmailAddressType))]
		public string Email { get; set; } = "";

		public required string Password { get; set; }
		
		public ICollection<UserGroups>? Groups { get; set; }

		[GraphQLIgnore]
		public ICollection<Messages>? Messages {get; set;}

		[GraphQLDescription("The list of user friend")]	
		public ICollection<FriendList>? Friends_List {get; set;}

		[GraphQLDescription("The list of people that befriended with user. In simple term, the list of people, that user have accepted as friend (accepted the friend request).")]	
		public ICollection<FriendList>? Friended_List {get; set;}

		[GraphQLDescription("The list of friend request. In simple term, the list of people that send friend request to user.")]	
		public ICollection<FriendRequest>? RequestsFrom_List {get; set;}

		[GraphQLDescription("The list of people, the user send friend request to.")]	
		public ICollection<FriendRequest>? RequestsTo_List {get; set;}

	}
}