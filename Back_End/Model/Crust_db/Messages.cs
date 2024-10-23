using Microsoft.EntityFrameworkCore;
using System;

namespace Back_End.Model.Crust_db
{

    public class Messages
    {
        [PrimaryKey]
        public ulong Id { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }
    }

}