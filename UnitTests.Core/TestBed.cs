﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Core
{
	public abstract class TestBed
	{
		protected virtual DbContextOptions<DbContext> GenerateContextOptions()
		{
			var serviceProvider = new ServiceCollection()
				.AddEntityFrameworkInMemoryDatabase()
				.AddEntityFrameworkProxies()
				.BuildServiceProvider();

			return new DbContextOptionsBuilder<DbContext>()
				.UseInMemoryDatabase("MockDb")
				.UseLazyLoadingProxies()
				//Without this line, data will persist between tests.
				// May be desirable, under certain conditions.
				.UseInternalServiceProvider(serviceProvider)
				.EnableSensitiveDataLogging()
				.Options;
		}
	}
}
