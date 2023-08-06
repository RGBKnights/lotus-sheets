using System;
using System.ComponentModel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Lotus.Sheets;

public sealed class ClockSkill
{
    [SKFunction, Description("Get the current clock time")]
    public static string Time() => DateTimeOffset.UtcNow.ToString("T");

    [SKFunction, Description("Get the time for the start of day.")]
    public static string StartOfDay() 
    {
        var now = DateTimeOffset.UtcNow;
        return new DateTimeOffset(now.Year, now.Month, now.Day, 9, 0, 0, TimeSpan.Zero).ToString("T");
    }

    [SKFunction, Description("Get the time for the end of day.")]
    public static string EndOfDay()
    {
        var now = DateTimeOffset.UtcNow;
        return new DateTimeOffset(now.Year, now.Month, now.Day, 5, 0, 0, TimeSpan.Zero).ToString("T");
    }
}