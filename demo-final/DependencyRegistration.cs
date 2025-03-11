
using Azure.Core;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SDDev.Net.GenericRepository.CosmosDB;
using SDDev.Net.GenericRepository.CosmosDB.Utilities;
using System.Configuration;

namespace Demo.CosmosDB_Final;

public static class DependencyRegistration
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        // Read the config from appsettings (this is just the keyvault name)
        var tmpConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        // modify this to use the azureclicredential when running locally but the defaultazurecredential when running in container
        TokenCredential credential = new DefaultAzureCredential();
#if DEBUG
        // I did this because I have multiple azure credentials on my machine and it was choosing the wrong one
        credential = new AzureCliCredential();
#endif

        // Load the cosmosdb configuration from the key vault and add it to the configuration
        var keyVaultName = tmpConfig.GetValue<string>("KeyVaultName");
        var config = new ConfigurationBuilder()
            .AddConfiguration(tmpConfig)
            .AddAzureKeyVault(new Uri($"https://{keyVaultName}.vault.azure.net/"), credential, new AzureKeyVaultConfigurationOptions()
            {
                ReloadInterval = TimeSpan.FromMinutes(5)
            }) // Add Key Vault
            .Build();


        services.AddSingleton<IConfiguration>(config);
        // setup some basic logging
        var logFactory = LoggerFactory.Create(x =>
        {
            x.ClearProviders();
            x.AddApplicationInsights();
            x.AddConsole();
            x.AddConfiguration(config);
        });

        services.Configure<CosmosDbConfiguration>(config.GetSection("CosmosDb"));

        services.AddSingleton(ctx =>
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.None,
                NullValueHandling = NullValueHandling.Include,
                Converters = new List<JsonConverter>() { new StringEnumConverter() }
            };

            //Serialize enums as string using default naming strategy (unchanged)
            var serializer = new CosmosJsonDotNetSerializer(serializerSettings);

            var config = ctx.GetService<IOptionsMonitor<CosmosDbConfiguration>>();

            if (string.IsNullOrEmpty(config.CurrentValue.ConnectionString))
                throw new InvalidOperationException("Missing required DocumentDB Configuration");

            return new CosmosClient(config.CurrentValue.ConnectionString, new CosmosClientOptions()
            {
                Serializer = serializer,
                AllowBulkExecution = config.CurrentValue.EnableBulkQuerying
            });

        });
    }
}
