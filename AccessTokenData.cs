﻿using System;
using System.Text.Json.Serialization;

namespace OAuth2Authenticator
{
    public class AccessTokenData
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        public DateTime ExpiresAt => DateTime.UtcNow.AddSeconds(ExpiresIn);
    }
}
