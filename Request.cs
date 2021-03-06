﻿using System;
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
        private Result<Project> projectResult;

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

        public void createProject()
        {
            // Example 4: we can create a new project
            Console.WriteLine("Create project:");

            projectResult = axosoftClient.Projects.Create(new Project
            {
                Name = string.Format("Test Project API"),
                //Description = string.Format("Created on: {0}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")),
                Description = "",
            });

            if (!projectResult.IsSuccessful)
            {
                // Wait for input before closing the console
                Console.WriteLine("Unable to create a new project. We're done here!");
                Console.ReadKey(true);

                return;
            }

            Console.WriteLine("Created: " + projectResult.Data.Id);

        }

        public void deleteProject()
        {
            
            Console.WriteLine("Delete project:");

            
            var deleteProjectResultest = axosoftClient.Projects.Delete(projectResult.Data.Id.Value, new Dictionary<string, object>
            {
                { "delete_items", true }
            });

            if (!deleteProjectResultest.IsSuccessful)
            {
                // Wait for input before closing the console
                Console.WriteLine("Unable to delete a project. We're done here!");
                

                return;
            }



            Console.WriteLine("Deleted: " +projectResult.Data.Id);

        }

        public void createUser()
        {
            Console.WriteLine("Create user:");

            var test = new List<SecurityRole>();
            
            var sec = new SecurityRole { Name="Hold 34"} ; // int[]

            test.Add(sec);
           
            var userResultest = axosoftClient.Users.Update(new User
            {
                FirstName = "TESTINGGYESYESYES",
                LastName = "TESTINGG",
                LoginId = "TESTINGG",
                Email = "duran.kse@gmail.com",
                SecurityRoles = test,
                IsActive = true,
                Id = 482,
                
                
            }
            );



            var secroleResult = axosoftClient.SecurityRoles.Get();

            foreach (var i in secroleResult.Data)
            {
                Console.WriteLine(i.Name);
            }




            








            if (!userResultest.IsSuccessful)
            {
                // Wait for input before closing the console
                Console.WriteLine("Unable to create user. We're done here!");


                return;
            }

            Console.WriteLine("Created user: " + userResultest.Data.Id);
        }


    }
}