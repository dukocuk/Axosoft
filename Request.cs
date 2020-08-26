using System;
using dotenv.net.Utilities;
namespace Axosoft
{
    public class Request
    {
        
        private string clientID, clientSecret;
        

        public Request()
        {
            var envReader = new EnvReader();
            this.clientID = envReader.GetStringValue("clientID");
            this.clientSecret = envReader.GetStringValue("clientSecret");
        }

        public Request(string clientID, string clientSecret)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
        }
    }
}
