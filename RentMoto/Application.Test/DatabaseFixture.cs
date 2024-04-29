using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Test
{
    public class DatabaseFixture
    {
        private const string _connectionString = "Host=localhost;Port=5432;Database=DatabaseTest;User ID=postgres;Password=R3ntm0t0#";

        private static object _lock = new object();
        private static bool _databaseInitialize;

        public DatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialize)
                {
                    using var context = CreateContext();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            }
        }

        public RentMotoContext CreateContext() => new RentMotoContext(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<RentMotoContext>().UseNpgsql(_connectionString).Options);
    }
}
