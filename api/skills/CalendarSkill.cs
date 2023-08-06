using System;
using System.ComponentModel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Lotus.Sheets;

public sealed class CalendarSkill
{
  [SKFunction, Description("Get the current date")]
  public static string Today() => DateTimeOffset.UtcNow.ToString("D");

  [SKFunction, Description("Get tomorrow's date")]
  public static string Tomorrow() => DateTimeOffset.UtcNow.AddDays(1).ToString("D");

  [SKFunction, Description("Get yesterday's date")]
  public static string Yesterday() => DateTimeOffset.UtcNow.AddDays(-1).ToString("D");

  [SKFunction, Description("Get current week number")]
  public static string Week(SKContext context)
  {
    return context.Culture.Calendar.GetWeekOfYear(DateTime.UtcNow, context.Culture.DateTimeFormat.CalendarWeekRule, context.Culture.DateTimeFormat.FirstDayOfWeek).ToString();
  }

  [SKFunction, Description("Get the date offset by a provided number of days from today")]
  public static string DaysAgo([Description("The number of days to offset from today"), SKName("input")] double offset) => DateTimeOffset.Now.AddDays(-offset).ToString("D");
}