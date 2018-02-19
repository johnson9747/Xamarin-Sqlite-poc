﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlLitePOC
{
    public class DatabaseContext:DbContext
    {
		public DbSet<Product> Products { get; set; }
		private readonly string _databasePath;
		public DatabaseContext(string databasePath)
		{
			_databasePath = databasePath;
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Filename={_databasePath}");
		}
	}
}
