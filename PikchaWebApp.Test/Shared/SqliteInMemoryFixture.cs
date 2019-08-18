using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using PikchaWebApp.Data;
using Microsoft.EntityFrameworkCore;
using PikchaWebApp.Models;

namespace PikchaWebApp.Test.Shared
{
    public class SqliteInMemoryFixture : IDisposable
    {
        private IServiceScope _serviceScope;
        private SqliteConnection _connection;

        public virtual void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _serviceScope?.Dispose();
            _serviceScope = null;
        }

        public virtual IServiceCollection ConfigureServices(IServiceCollection services)
        { 
            services
                .AddMemoryCache()
                .AddLogging()
                .AddDbContext<PikchaDbContext>(b => b.UseSqlite(_connection));
            
            services.AddDefaultIdentity<PikchaUser>()
                .AddEntityFrameworkStores<PikchaDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<PikchaUser, PikchaDbContext>();

            return services;
        }
        
        public virtual IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceScope == null)
                {
                    _serviceScope = ConfigureServices(new ServiceCollection()).BuildServiceProvider().CreateScope();

                    
                }

                return _serviceScope.ServiceProvider;
            }
        }

        public virtual PikchaDbContext Context
            => ServiceProvider.GetRequiredService<PikchaDbContext>();

        public virtual void CreateDatabase()
        {
            Dispose();
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            Context.Database.EnsureCreated();
        }
    }

}
