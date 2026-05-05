# Local Setup

## Prerequisites

- Install .NET SDK.
- Install Docker Desktop.
- Clone the repository and checkout the required branch.

## Run PostgreSQL and pgAdmin

```powershell
docker compose up -d
docker ps
```

PostgreSQL runs on local port `5433`.

pgAdmin runs at http://localhost:5051.

## Restore and Build

```powershell
dotnet restore CloudSecOps.Web/CloudSecOps.Web.csproj
dotnet build CloudSecOps.Web/CloudSecOps.Web.csproj
```

## Run the Web App

```powershell
cd CloudSecOps.Web
dotnet run
```

Open:

- http://localhost:5045
- https://localhost:7045

If the HTTPS development certificate is not trusted, use the HTTP URL for Task 1 screenshots or run `dotnet dev-certs https --trust`.
