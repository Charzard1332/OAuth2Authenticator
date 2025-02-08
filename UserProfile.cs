using System.Text.Json.Serialization;

namespace OAuth2Authenticator
{
    public class UserProfile
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        public string GetAvatarUrl(string provider)
        {
            return provider switch
            {
                "discord" => $"https://cdn.discordapp.com/avatars/{Id}/{Avatar}.png",
                "google" => Avatar, // Google provides a direct avatar URL
                "github" => Avatar, // GitHub provides a direct avatar URL
                _ => null
            };
        }
    }
}
