using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductCatalogWatcher
{
    public static class ConfigLoader
    {

        public static IConfiguration LoadConfig(string configPath) {
            IConfiguration config;

            //Config help: https://garywoodfine.com/configuration-api-net-core-console-application/
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, false, true);
            config = builder
                .Build();

            return config;
        }

    }
}
