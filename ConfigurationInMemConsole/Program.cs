using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace ConfigurationInMemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
        }

        private static void Example1()
        {
            var builder = new ConfigurationBuilder();

            // add static values
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Environment", "production" },
            });

            // add values from a json file
            builder.AddJsonFile("appsettings.json");

            // create the IConfigurationRoot instance
            IConfigurationRoot configuration = builder.Build();

            // see -> builder.AddInMemoryCollection above
            string value = configuration["Environment"];

            IConfigurationSection section = configuration.GetSection("database");
                                                                                  
            var dataSections = configuration.GetSection("database").GetChildren().ToList();

            var connectionString = $"Data Source={dataSections[1].Value};" +
                                   $"Initial Catalog={dataSections[0].Value};" +
                                   $"Integrated Security={dataSections[2].Value}";

            var appSections = configuration.GetSection("AppSettings").GetChildren().ToList();

            Debug.WriteLine($"        User name: {appSections[2].Value}");
            Debug.WriteLine($"           In mem: {value}");
            Debug.WriteLine($"Connection string: {connectionString}");
            Debug.WriteLine("");
        }
    }
}
