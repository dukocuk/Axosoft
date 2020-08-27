using System;
using AxosoftAPI.NET;
using dotenv.net.Utilities;
namespace Axosoft
{
    public class Request
    {
        
        Proxy axosoftClient;

        public Request()
        {
            dotenv.net.DotEnv.Config(true, "../../.env");
            var envReader = new EnvReader();

            axosoftClient = new Proxy
            {
                Url = envReader.GetStringValue("URL"),
                ClientId = envReader.GetStringValue("CLIENTID"),
                ClientSecret = envReader.GetStringValue("CLIENTSECRET"),
            };

            axosoftClient.ObtainAccessTokenFromUsernamePassword(envReader.GetStringValue("USERNAME"), envReader.GetStringValue("PASSWORD"), ScopeEnum.ReadWrite);

            if (string.IsNullOrWhiteSpace(axosoftClient.AccessToken))
            {
                Console.WriteLine("Unable to authenticate against Axosoft.");

                // Wait for input before closing the console
                Console.WriteLine("Press any key to close the console.");
                Console.ReadKey(true);

                return;
            }
        }


        public void getAllProjects()
        {
            // Example 1: we can get all projects
            var projectsResult = axosoftClient.Projects.Get();

            if (!projectsResult.IsSuccessful)
            {
                // Wait for input before closing the console
                Console.WriteLine("Unable to get projects. We're done here!");
                Console.ReadKey(true);

                return;
            }

            Console.WriteLine("Example 1 -> Projects:");

            foreach (var project in projectsResult.Data)
            {
                Console.WriteLine(string.Format("Project Id: {0} - Name: {1}", project.Id, project.Name));
            }

            Console.WriteLine();
        }


    }
}