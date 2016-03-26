using System;
using System.Linq;
using DatabaseServer.Entity.Models;
using EntityFramework.Extensions;

namespace DatabaseServer.Entity.Context
{
    class Test
    {
        public static void Main()
        {
            using (TestContext ctx = new TestContext())
            {
                ctx.Database.Initialize(true);
                Console.WriteLine("初始化");
                Console.ReadLine();
                ctx.Users.Add(new User {Nickname = "0", Account = "1",Password = "050000", RegistTime = DateTime.Now});
                ctx.SaveChanges();

            }
            Console.ReadLine();
        }
    }
}
