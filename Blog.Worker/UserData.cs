using System.Text.Json.Serialization;

namespace Blog.Worker
{
    public class UserData
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("auth_type")]
        public string AuthType { get; set; } = string.Empty;

    }
}
