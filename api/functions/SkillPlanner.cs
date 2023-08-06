using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.AI.TextCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Skills.Core;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Stepwise;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace Lotus.Sheets;
public class SkillPlanner
{
  private readonly IKernel kernel;
  private readonly StepwisePlanner planner;
  private readonly CosmosClient cosmosClient;

  public SkillPlanner(CosmosClient cosmosClient, IConfiguration configuration, ILogger logger) //IOptions<OpenAIApiOptions> aiOptions
  {
    this.cosmosClient = cosmosClient;

    var apiKey = configuration["OpenAiKey"];
    var orgId = configuration["OpenAiOrg"];

    this.kernel = Kernel.Builder
        .WithLogger(logger)
        .WithAIService<ITextEmbeddingGeneration>("TextEmbedding", new OpenAITextEmbeddingGeneration("text-embedding-ada-002", apiKey, orgId))
        .WithAIService<ITextCompletion>("TextCompletion", new OpenAIChatCompletion("gpt-4", apiKey, orgId))
        .Build();

    kernel.ImportSkill(new FormSkill(this.cosmosClient), nameof(FormSkill));
    //kernel.ImportSkill(new DocumentSkill(this.cosmosClient), nameof(DocumentSkill));
    //kernel.ImportSkill(new CalendarSkill(), nameof(CalendarSkill));
    //kernel.ImportSkill(new TimeSkill(), nameof(TimeSkill));
    //kernel.ImportSkill(new MathSkill(), nameof(MathSkill));
    //kernel.ImportSkill(new ConvertSkills(), nameof(ConvertSkills));
    
    var config = new StepwisePlannerConfig
    {
        MinIterationTimeMs = 1000,
        MaxIterations = 32,
        MaxTokens = 4000,
        MaxRelevantFunctions = 3,
        RelevancyThreshold = 0.1,
    };
    this.planner = new(kernel, config);
  }

  [FunctionName("Planner")]
  public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "plan")] HttpRequest req)
  {
    req.Query.TryGetValue("goals", out var goals);
    var goal = string.Join(Environment.NewLine, goals); // var goal = "load part abc123";
    var plan = this.planner.CreatePlan(goal);
    var cxt = this.kernel.CreateNewContext();
    var output = await plan.InvokeAsync(cxt);
    return new OkObjectResult(output.Result);
  }
}