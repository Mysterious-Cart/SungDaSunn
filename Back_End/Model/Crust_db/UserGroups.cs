using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Back_End.Model.Crust_db
{
	public class UserGroups
	{
        [GraphQLType("Int!")]
        public ulong UserId { get; set; }

        [AllowNull]
		public User User { get; set; }

        [GraphQLType("Int!")]
        public ulong GroupId { get; set; }
        
        [AllowNull]
		public Group GroupInfo { get; set; }
    }
}