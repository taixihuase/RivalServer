using System.Data.Entity;
using DatabaseServer.Entity.Models;
using DatabaseServer.Entity.Models.Maps;

namespace DatabaseServer.Entity.Context
{
    public class TestContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TestContext() : base("Test") { }

        public TestContext(string nameOrConnectionString): base(nameOrConnectionString){ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
