using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OAuth2Authenticator
{
    public class OAuthHelper
    {
        private static readonly string discordClientId = "YOUR_DISCORD_CLIENT_ID";
        private static readonly string discordClientSecret = "YOUR_DISCORD_CLIENT_SECRET";
        private static readonly string googleClientId = "YOUR_GOOGLE_CLIENT_ID";
        private static readonly string googleClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
        private static readonly string githubClientId = "YOUR_GITHUB_CLIENT_ID"; // if dev
        private static readonly string githubClientSecret = "YOUR_GITHUB_CLIENT_SECRET"; // if dev
        private static readonly string twitterClientId = "YOUR_TWITTER_CLIENT_ID";
        private static readonly string twitterClientSecret = "YOUR_TWITTER_CLIENT_SECRET";
        private static readonly string microsoftClientId = "YOUR_MICROSOFT_CLIENT_ID";
        private static readonly string microsoftClientSecret = "YOUR_MICROSOFT_CLIENT_SECRET";
        private static readonly string epicgamesClientId = "YOUR_EPICGAMES_CLIENT_ID";
        private static readonly string redirectUri = "http://localhost:5000/callback";

        public async Task<string> ExchangeCodeForToken(string provider, string code)
        {
            string tokenUrl = provider switch
            {
                "discord" => "https://discord.com/api/oauth2/token",
                "google" => "https://oauth2.googleapis.com/token",
                "github" => "https://github.com/login/oauth/access_token",
                "twitter" => "https://api.twitter.com/2/oauth2/token",
                "microsoft" => "https://login.microsoftonline.com/common/oauth2/v2.0/token",
                "epic" => "https://api.epicgames.dev/epic/oauth/v1/token",
                _ => throw new Exception("Unsupported provider")
            };

            var clientId = provider switch
            {
                "discord" => discordClientId,
                "google" => googleClientId,
                "github" => githubClientId,
                "twitter" => twitterClientId,
                "microsoft" => microsoftClientId,
                "epic" => epicgamesClientId,
                _ => throw new Exception("Unsupported provider")
            };

            var clientSecret = provider switch
            {
                "discord" => discordClientSecret,
                "google" => googleClientSecret,
                "github" => githubClientSecret,
                "twitter" => twitterClientSecret,
                "microsoft" => microsoftClientSecret,
                _ => throw new Exception("Unsupported provider")
            };

            using var client = new HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri)
            });

            var response = await client.PostAsync(tokenUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📩 OAuth2 Token Response: {responseString}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Error: Failed to retrieve access token. Status: {response.StatusCode}");
                return null;
            }

            var tokenData = JsonSerializer.Deserialize<AccessTokenData>(responseString);
            return tokenData?.AccessToken;
        }

        public async Task<string> WaitForCallback()
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add(redirectUri + "/");
            httpListener.Start();

            Console.WriteLine("🔄 Waiting for OAuth callback...");

            var context = await httpListener.GetContextAsync();
            var response = context.Response;

            string responseString = "<html><body><h2>You can close this tab now.</h2></body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();

            string code = context.Request.QueryString["code"];
            httpListener.Stop();

            Console.WriteLine($"✅ Received OAuth Code: {code}");
            return code;
        }

        public string GetAuthUrl(string provider)
        {
            return provider switch
            {
                "discord" => $"https://discord.com/api/oauth2/authorize?client_id={discordClientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope=identify",
                "google" => $"https://accounts.google.com/o/oauth2/auth?client_id={googleClientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope=email%20profile",
                "github" => $"https://github.com/login/oauth/authorize?client_id={githubClientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope=read:user",
                "twitter" => $"https://twitter.com/i/oauth2/authorize?response_type=code&client_id={twitterClientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope=users.read%20tweet.read%20offline.access",
                "microsoft" => $"https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={microsoftClientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope=user.read%20openid%20email%20offline_access",
                "epic" => $"https://www.epicgames.com/id/authorize?client_id={epicgamesClientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope=basic_profile%20friends_list%20presence",
                _ => throw new Exception("Unsupported provider")
            };
        }

        public async Task<UserProfile> FetchUserProfile(string provider, string accessToken)
        {
            string apiUrl = provider switch
            {
                "discord" => "https://discord.com/api/users/@me",
                "google" => "https://www.googleapis.com/oauth2/v2/userinfo",
                "github" => "https://api.github.com/user",
                _ => throw new Exception("Unsupported provider")
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetStringAsync(apiUrl);
                return JsonSerializer.Deserialize<UserProfile>(response);
            }
        }
    }
}
