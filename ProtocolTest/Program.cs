using System;
using System.Collections.Generic;
using System.Linq;
using C2SProtocol.Data;
using Protocol;

namespace ProtocolTest
{
    class Program
    {
        static void Main(string[] args)
        {
           
            try
            {
                Console.WriteLine(IpTool.GetPublicIpAddress());
            }
            catch (Exception)
            {               
                Console.WriteLine("Failed");
            }
            Console.ReadLine();
            Console.ReadLine();

        }
    }
}
