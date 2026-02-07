# 🚀 CI/CD Setup Guide

## What is CI/CD?

**CI (Continuous Integration):** Automatically builds and tests your code when you push to GitHub
**CD (Continuous Deployment):** Automatically deploys your app to Railway after successful build

**In simple terms:** Push code → GitHub builds it → Railway runs it → Your app is live! ✨

---

## 📋 One-Time Setup (5 minutes)

### Step 1: Get Railway Token

1. Go to [Railway Dashboard](https://railway.app)
2. Click your profile (top right)
3. Click **"Account Settings"**
4. Click **"Tokens"** tab
5. Click **"Create Token"**
6. Give it a name: `GitHub Actions`
7. **Copy the token** (you'll need it in Step 3)

### Step 2: Get Railway Service Name

1. Go to your Railway project
2. Click on your service
3. Look at the URL or service name (e.g., `todotasks-api`)
4. **Copy the service name**

### Step 3: Add Secrets to GitHub

1. Go to your GitHub repository
2. Click **"Settings"** (top menu)
3. Click **"Secrets and variables"** → **"Actions"** (left sidebar)
4. Click **"New repository secret"**
5. Add **First Secret:**
   - Name: `RAILWAY_TOKEN`
   - Value: (Paste the token from Step 1)
   - Click **"Add secret"**
6. Add **Second Secret:**
   - Name: `RAILWAY_SERVICE_NAME`
   - Value: (Paste the service name from Step 2)
   - Click **"Add secret"**

---

## ✅ That's It! Now It Works Automatically

### How to Deploy:

```bash
# 1. Make changes to your code
# 2. Commit and push
git add .
git commit -m "Add new feature"
git push origin main

# 3. Watch the magic happen! 🎉
```

### What Happens Next:

1. ⏱️ **GitHub Actions starts** (within seconds)
2. 🔨 **Builds your .NET 10 app** (~2 minutes)
3. 📦 **Creates self-contained package**
4. 🚀 **Deploys to Railway** (~1 minute)
5. ✅ **Your app is live!**

---

## 📊 Monitor Your Deployments

### GitHub Actions:
- Go to your repo → **"Actions"** tab
- See build logs, status, and errors
- Green checkmark ✅ = Success
- Red X ❌ = Failed (click to see why)

### Railway:
- Go to Railway Dashboard
- Click **"Deployments"** tab
- See deployment status and logs

---

## 🐛 Troubleshooting

### Build Fails on GitHub Actions:
- Check the **Actions** tab for error messages
- Common issues:
  - Syntax errors in code
  - Missing NuGet packages
  - Test failures

### Deployment Fails on Railway:
- Check Railway logs
- Common issues:
  - Missing environment variables
  - Database connection issues
  - Port configuration

### Secrets Not Working:
- Make sure secret names are EXACTLY:
  - `RAILWAY_TOKEN` (not railway_token)
  - `RAILWAY_SERVICE_NAME` (not service_name)
- Secrets are case-sensitive!

---

## 💡 Tips

- **First deployment takes longer** (~3-5 minutes)
- **Subsequent deployments are faster** (~2-3 minutes)
- **You can manually trigger** workflow from Actions tab
- **Free tier limits:** 2,000 minutes/month (plenty for hobby projects!)

---

## 📚 Learn More

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Railway Documentation](https://docs.railway.app)
- [.NET Self-Contained Deployment](https://learn.microsoft.com/en-us/dotnet/core/deploying/)

---

**Questions?** Check the comments in `.github/workflows/deploy.yml` - every line is explained! 📖
