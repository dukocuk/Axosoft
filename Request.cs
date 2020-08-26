using System;
using dotenv.net.Utilities;
namespace Axosoft
{
    public class Request
    {
        
        private string clientID, clientSecret;
        

        public Request()
        {
            dotenv.net.DotEnv.Config(true, "../../.env");
            var envReader = new EnvReader();
            this.clientID = envReader.GetStringValue("CLIENTID");
            this.clientSecret = envReader.GetStringValue("CLIENTSECRET");
            Console.WriteLine(this.clientID);
        }

        public Request(string clientID, string clientSecret)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
        }
    }
}
