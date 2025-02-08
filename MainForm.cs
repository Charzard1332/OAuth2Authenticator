using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuth2Authenticator
{
    public partial class MainForm : Form
    {
        private OAuthManager oauthManager;

        public MainForm()
        {
            InitializeComponent();
            oauthManager = new OAuthManager(this);
        }

        private async void discordBtn_Click(object sender, EventArgs e)
        {
            await oauthManager.LoginWithProvider("discord");
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void guna2GradientCircleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private async void googleBtn_Click(object sender, EventArgs e)
        {
            await oauthManager.LoginWithProvider("google");
        }
    }
}
