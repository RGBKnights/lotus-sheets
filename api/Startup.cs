using System.IO;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Lotus.Sheets.Startup))]

namespace Lotus.Sheets;
public class Startup : FunctionsStartup
{
  public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
  {
    var context = builder.GetContext();

    builder.ConfigurationBuilder
      .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true, reloadOnChange: false)
      .AddUserSecrets("def4c678-2c7c-440f-9747-6c8e6b009d88")
      .AddEnvironmentVariables();
  }

  override public void Configure(IFunctionsHostBuilder builder)
  {
    var context = builder.GetContext();
    var configuration = context.Configuration;

    builder.Services.AddHttpClient();
    builder.Services.AddSingleton(() => 
    {
      var url = configuration["CosmosEndpoint"];
      var token = configuration["CosmosToken"];
      return new CosmosClient(url, token);
    });
  }
}