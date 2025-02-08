using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuth2Authenticator
{
    internal class OAuthManager
    {
        private readonly MainForm mainForm;
        private readonly OAuthHelper oauthHelper;

        public OAuthManager(MainForm form)
        {
            this.mainForm = form;
            oauthHelper = new OAuthHelper();
        }

        public async Task LoginWithProvider(string provider)
        {
            string authUrl = oauthHelper.GetAuthUrl(provider);
            System.Diagnostics.Process.Start(authUrl);

            string code = await oauthHelper.WaitForCallback(); // 🔥 Fix Here
            if (!string.IsNullOrEmpty(code))
            {
                string accessToken = await oauthHelper.ExchangeCodeForToken(provider, code);
                if (!string.IsNullOrEmpty(accessToken))
                {
                    UserProfile userProfile = await oauthHelper.FetchUserProfile(provider, accessToken);
                    mainForm.Invoke((MethodInvoker)(() =>
                    {
                        MessageBox.Show("Logged in successfully!");
                    }));

                    SecureStorage.SaveEncryptedToken(provider, accessToken);
                }
            }
        }
    }
}
