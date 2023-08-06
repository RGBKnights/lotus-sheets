using System;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Lotus.Sheets;

public sealed class DocumentSkill
{
  private readonly CosmosClient cosmosClient;
  
  public DocumentSkill(CosmosClient cosmosClient)
  {
    this.cosmosClient = cosmosClient;
  }

  [SKFunction, Description("List all document ids matching a query.")]
  public static string Search()
  {
    return ""; // list of Ids
  }
  
  [SKFunction, Description("Get a document by id")]
  public static string Get() // Id
  {
    return ""; // JSON
  }

  [SKFunction, Description("Change part of a document by Id, JSON Path, and Value")]
  public static string Patch() // Id, Path, Value
  {
    return ""; // JSON
  }
 
  [SKFunction, Description("Create a document matching the schema by id")]
  public static string Create() // Schema Id
  {
    return ""; // Document Id
  }

  [SKFunction, Description("Remove a document by id")]
  public static string Remove() // Id
  {
    return ""; // Nothing...?
  }
}