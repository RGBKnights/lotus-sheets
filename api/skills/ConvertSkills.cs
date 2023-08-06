using System;
using System.ComponentModel;
using Microsoft.SemanticKernel.SkillDefinition;

namespace Lotus.Sheets;

public sealed class ConvertSkills
{
    [SKFunction]
    [Description("Converts a date to a unix timestamp (seconds)")]
    public static string DateToNumber([Description("The date to be converted"), SKName("date")] string date) => DateTimeOffset.Parse(date).ToUnixTimeSeconds().ToString();

    [SKFunction]
    [Description("Converts unix timestamp (milliseconds) to a date")]
    public static string NumberToDate([Description("The number to be converted"), SKName("number")] long number) => DateTimeOffset.FromUnixTimeSeconds(number).ToString("D");
}