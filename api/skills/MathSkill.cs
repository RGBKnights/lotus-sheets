using System;
using System.ComponentModel;
using Microsoft.SemanticKernel.SkillDefinition;
using NCalc;

namespace Lotus.Sheets;

public sealed class MathSkill
{
  [SKFunction, SKName("Calculator"), Description("Useful for getting the result of a non-trivial mathematical expression that are by NCalc library a calculator capable of more advanced math functions like Abs, Acos, Asin, Atan, Ceiling, Cos, Exp, Floor, Log, Log10, Max, Min, Pow, Round, Sign, Sin, Sqrt, Tan, and Truncate.")]
  public static string Calculate([Description("A valid mathematical expression that could be executed by NCalc library.")]string input)
  {
    var expr = new Expression(input, EvaluateOptions.IgnoreCase);
    expr.EvaluateParameter += delegate (string name, ParameterArgs args)
    {
      args.Result = name.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
      {
        "pi" => Math.PI,
        "e" => Math.E,
        _ => args.Result
      };
    };

    try
    {
      if (expr.HasErrors())
        return $"Error ({expr.Error}) could not evaluate {input}";

      var result = expr.Evaluate();
      return result.ToString();
    }
    catch (Exception e)
    {
      throw new InvalidOperationException($"Could not evaluate {input}", e);
    }
  }
}