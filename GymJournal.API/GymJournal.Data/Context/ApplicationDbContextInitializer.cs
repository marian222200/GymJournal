﻿using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace GymJournal.Data.Context
{
	public class ApplicationDbContextInitializer
	{
		private readonly ILogger<ApplicationDbContextInitializer> _logger;
		private readonly ApplicationDbContext _context;

		public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}
		public async Task InitialiseAsync()
		{
			try
			{
				if (_context.Database.IsSqlServer())
				{
					await _context.Database.MigrateAsync();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while initializing the database.");
				throw;
			}
		}
	}
}
