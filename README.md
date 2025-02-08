# 🌍 Multi-Platform OAuth2 Authenticator - .NET WinForms

🚀 A **Windows Forms Application** that allows users to **log in via multiple OAuth2 providers** including:
- 🟣 **Discord**
- 🔵 **Google**
- 🟡 **GitHub**
- 🐦 **Twitter**
- 🎮 **Microsoft**
- 🟢 **Epic Games**

---

## ✅ Features
- 🔐 **Multi-Provider OAuth2 Login** (Discord, Google, GitHub, Twitter, Microsoft, Epic Games)
- 📂 **Extensible Architecture (Easily Add More OAuth2 Providers)**

---

## ⚙️ Setup & Installation

### 🔹 **1️⃣ Clone the Repository**
```sh
git clone https://github.com/YOUR_GITHUB_USERNAME/OAuth2Authenticator.git
cd OAuth2Authenticator

### 🔹 **2️⃣ Change Client ID And Secret**
OAuthHelper.cs
```cs
private static readonly string discordClientId = "YOUR_DISCORD_CLIENT_ID";
private static readonly string discordClientSecret = "YOUR_DISCORD_CLIENT_SECRET";
private static readonly string googleClientId = "YOUR_GOOGLE_CLIENT_ID";
private static readonly string googleClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
private static readonly string githubClientId = "YOUR_GITHUB_CLIENT_ID";
private static readonly string githubClientSecret = "YOUR_GITHUB_CLIENT_SECRET";
private static readonly string twitterClientId = "YOUR_TWITTER_CLIENT_ID";
private static readonly string twitterClientSecret = "YOUR_TWITTER_CLIENT_SECRET";
private static readonly string microsoftClientId = "YOUR_MICROSOFT_CLIENT_ID";
private static readonly string microsoftClientSecret = "YOUR_MICROSOFT_CLIENT_SECRET";
private static readonly string epicClientId = "YOUR_EPIC_CLIENT_ID";
private static readonly string epicClientSecret = "YOUR_EPIC_CLIENT_SECRET";
private static readonly string redirectUri = "http://localhost:5000/callback";
```

### **3️⃣ Build Application**
```
Ctrl + Shift + b
```

## 🛠 Usage
- Open the provided **OAuth2 login URL** in your browser.
- Authorize the application using your OAuth2 account.
- The app will fetch and display your **Discord username and Nitro status**.

---

## 📝 License
This project is open-source and available under the **MIT License**.

---

## 🌟 Contributing
Feel free to submit **issues** or **pull requests** to improve the project!
