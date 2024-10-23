using System;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Model.Crust_db
{
	public class User
	{
		public ulong Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}