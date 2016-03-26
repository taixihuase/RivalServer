using System;
using System.Collections.Generic;
using System.Data.Entity;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Context
{
    public class TestInitializer:DropCreateDatabaseIfModelChanges<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            var users = new List<User>
            {
                new User
                {
                    Account = "123",
                    Nickname = "456",
                    Password = "789",
                    RegistTime = DateTime.Now
                },
                new User
                {
                    Account = "321",
                    Nickname = "654",
                    Password = "987",
                    RegistTime = DateTime.Now
                }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
        }
    }
}
