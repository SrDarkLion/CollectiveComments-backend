using System.ComponentModel;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FeedbackType
{
  [Description("Simple comment about some topic")]
  Comment,
  Suggestion,
  Report
}