using System;
using System.Collections.Generic;
using AxosoftAPI.NET;
using AxosoftAPI.NET.Models;
using dotenv.net.Utilities;
namespace Axosoft
{
    public class Request
    {
        private EnvReader envReader;
        Proxy axosoftClient;
        private Result<IEnumerable<Project>> projectsResult;

        public Request()
        {
            dotenv.net.DotEnv.Config(true, "../../.env");
            envReader = new EnvReader();

            axosoftClient = new Proxy
            {
                Url = envReader.GetStringValue("URL"),
                ClientId = envReader.GetStringValue("CLIENTID"),
                ClientSecret = envReader.GetStringValue("CLIENTSECRET"),
            };

            ObtainAccessToken();
            getAllProjects();
            
        }

        private void ObtainAccessToken()
        {
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
            projectsResult = axosoftClient.Projects.Get();

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

        public void createProject()
        {
            // Example 4: we can create a new project
            Console.WriteLine("Example 4 -> Create project:");

            var project = axosoftClient.Projects.Create(new Project
            {
                Name = string.Format("Test Project API"),
                //Description = string.Format("Created on: {0}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")),
                Description = "",
            });

            if (!project.IsSuccessful)
            {
                // Wait for input before closing the console
                Console.WriteLine("Unable to create a new project. We're done here!");
                Console.ReadKey(true);

                return;
            }

        }

        public void deleteProject()
        {

        }


    }
}