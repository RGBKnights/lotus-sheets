using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Lotus.Sheets;

[Description("Create a form with a given name")]
public sealed class FormSkill
{
  private readonly CosmosClient cosmosClient;
  
  public FormSkill(CosmosClient cosmosClient)
  {
    this.cosmosClient = cosmosClient;
  }

  [SKFunction, Description("get valid element types")]
  public static string Types() // JSON
  {
    return ""; // Id
  }

  [SKFunction, Description("create or replaces form")]
  public static string Put() // JSON
  {
    return ""; // Id
  }

  [SKFunction, Description("Get form by id")]
  public static string Get() // JSON
  {
    return ""; // Id
  }

  [SKFunction, Description("Remove form by id")]
  public static string Remove() // JSON
  {
    return ""; // Id
  }

  [SKFunction, Description("List all forms")]
  public static string List()
  {
    return ""; // list of Identifiers
  }
}