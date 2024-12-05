#nullable disable

using System.Text.Json.Serialization;

namespace RCL.SSL.SDK
{
    public class ValidationToken
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string tokenName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string tokenValue { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string challengeType { get; set; }
    }
}
