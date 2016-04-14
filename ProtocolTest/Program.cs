using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using C2SProtocol.Data;
using DatabaseServer.Entity.Context;
using DatabaseServer.Entity.Models;
using Protocol;
using S2SProtocol.Common;

namespace ProtocolTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RivalContext db = new RivalContext())
            {
                Console.WriteLine("OK1");

                var time = ServerTime.Instance.Time;
                Thread.Sleep(1000);
                List<User> users = new List<User>
                {
                    new User
                    {
                        Account = "User1",
                        Nickname = "AA",
                        Password = "123456",
                        RegistTime = ServerTime.Instance.Time
                    },
                    new User
                    {
                        Account = "User2",
                        Nickname = "AB",
                        Password = "123456",
                        RegistTime = ServerTime.Instance.Time
                    }
                };
            Console.WriteLine(users[0].RegistTime);

                db.Users.Add(users[0]);
                db.SaveChanges();
            }
            Console.WriteLine("OK4");
        }
    }
}
