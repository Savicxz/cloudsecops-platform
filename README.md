# CloudSecOps

CloudSecOps is a cloud-based cybersecurity operations platform for managing security incidents, vulnerabilities, assets, audit logs, and role-based dashboards.

## Task 1 Focus

- ASP.NET Core MVC application foundation
- PostgreSQL local database using Docker Compose
- Entity Framework Core packages
- Identity package foundation
- GitHub branch workflow

## Confirmed Stack

- ASP.NET Core MVC
- Razor Views / Bootstrap 5
- PostgreSQL
- Entity Framework Core
- ASP.NET Core Identity
- Docker Compose
- GitHub

## Local Ports

- ASP.NET HTTP: http://localhost:5045
- ASP.NET HTTPS: https://localhost:7045
- PostgreSQL: localhost:5433
- pgAdmin: http://localhost:5051

## Database Credentials

- Database: cloudsecops_db
- Username: cloudsecops_admin
- Password: cloudsecops_password

## Run Locally

```powershell
docker compose up -d
cd CloudSecOps.Web
dotnet restore
dotnet run
```

## Git Workflow

- main = stable branch
- develop = integration branch
- branch-task1 = Task 1 setup branch
- feature branches should merge into develop by pull request
- develop should merge into main only after testing

## Task 2 Note

Do not implement Task 2 yet. Later Task 2 will include S3, API Gateway, Lambda, CloudWatch, and X-Ray.
