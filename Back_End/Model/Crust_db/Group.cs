using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Model.Crust_db {
	public class Group
	{
		public ulong Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public ICollection<User> Users { get; set; }
	}
}