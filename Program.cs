using System;
using System.IO;
using dotenv.net.Utilities;

namespace Axosoft
{
    class MainClass
    {
        public static void Main(string[] args)
        {


            Console.WriteLine("Axosoft bruger oprettelse\r");
            Console.WriteLine("------------------------\n");

            var requestTest = new Request();

            requestTest.createProject();
            requestTest.deleteProject();

            
            //Console.ReadKey();


        }
    }
}
