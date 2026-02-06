# Railway Deployment Guide

## Required Environment Variables

Set these in your Railway project settings:

### Database Configuration
```
DatabaseProvider=PostgreSQL
ConnectionStrings__PostgresConnection=${{Postgres.DATABASE_URL}}
```

### JWT Configuration
```
Jwt__Key=!@#!@34#$%$#%6y)_(()(*xdfkwerfdRTYRTYFGHVdsdfjsld
Jwt__Issuer=TodoTasks
Jwt__Audience=todotask-api
Jwt__ExpireMinutes=60
```

### ASP.NET Core Configuration
```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
```

## Steps to Deploy

1. **Add PostgreSQL Database** in Railway
   - Click "New" → "Database" → "Add PostgreSQL"
   - Railway will automatically create `DATABASE_URL` variable

2. **Set Environment Variables**
   - Go to your service → "Variables" tab
   - Add all variables listed above
   - Use `${{Postgres.DATABASE_URL}}` to reference the database

3. **Deploy**
   - Push your code to GitHub
   - Railway will automatically build and deploy
   - Migrations run automatically on startup

4. **Access Your API**
   - Swagger UI: `https://your-app.up.railway.app/swagger`
   - API Base: `https://your-app.up.railway.app/api`

## Troubleshooting

- Check logs in Railway dashboard
- Ensure PostgreSQL database is running
- Verify all environment variables are set
- Database migrations run automatically on startup
