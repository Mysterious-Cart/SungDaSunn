using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Model.Crust_db
{
	public class User
	{
        [GraphQLType("Int!")]
        public ulong Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public ICollection<UserGroups> Groups { get; set; }
	}
}