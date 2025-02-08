using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OAuth2Authenticator
{
    public static class SecureStorage
    {
        private static readonly string key = "MY_SECRET_KEY"; // Change this!

        public static void SaveEncryptedToken(string provider, string token)
        {
            byte[] encryptedBytes = Protect(token);
            File.WriteAllBytes($"{provider}_token.dat", encryptedBytes);
        }

        public static string LoadEncryptedToken(string provider)
        {
            if (!File.Exists($"{provider}_token.dat")) return null;

            byte[] encryptedBytes = File.ReadAllBytes($"{provider}_token.dat");
            return Unprotect(encryptedBytes);
        }

        private static byte[] Protect(string data)
        {
            return ProtectedData.Protect(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(key), DataProtectionScope.CurrentUser);
        }

        private static string Unprotect(byte[] data)
        {
            return Encoding.UTF8.GetString(ProtectedData.Unprotect(data, Encoding.UTF8.GetBytes(key), DataProtectionScope.CurrentUser));
        }
    }
}
