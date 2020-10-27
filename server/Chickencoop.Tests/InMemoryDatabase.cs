using Chickencoop.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Tests
{
    public class InMemoryDatabase : IDisposable
    {
        private bool disposed = false;
        protected readonly DbContextOptions<ChickencoopContext> DbOptions;

        public InMemoryDatabase()
        {
            DbOptions = new DbContextOptionsBuilder<ChickencoopContext>()
                .UseInMemoryDatabase("SampleDb" + Guid.NewGuid().ToString())
                .Options;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                using (var sampleDbContext = new ChickencoopContext(DbOptions))
                {
                    sampleDbContext.Database.EnsureDeleted();
                }
            }
            disposed = true;
        }

        ~InMemoryDatabase()
        {
            Dispose(false);
        }
    }
}
