# CloudSecOps

CloudSecOps: Cloud-Based Security Operations and Incident Response Platform.

CloudSecOps is a commercial-style defensive cybersecurity operations platform for managing security incidents, vulnerabilities, assets, analyst assignments, audit logs, evidence files, and role-based dashboards.

## Scope

This repository is for defensive security operations and incident response coursework only. It must not include exploit tools, password cracking, malware, phishing kits, credential collection, port scanning, attack automation, bypass tools, or payload generation.

## Confirmed Stack

- ASP.NET Core MVC
- Razor Views / CSHTML
- Bootstrap 5
- JavaScript
- PostgreSQL
- Entity Framework Core
- ASP.NET Core Identity
- Docker Compose
- GitHub

## Task 1 Focus

Task 1 establishes a clean production-style skeleton: MVC folders, EF Core and Identity foundations, role-based controller boundaries, placeholder entities, services, view models, Razor views, Docker Compose, and documentation structure.

Business logic is intentionally minimal so team members can work in parallel without blocking each other.

## Task 2 Future Services

Task 2 is deferred and may extend the system with Amazon RDS PostgreSQL, AWS Elastic Beanstalk, Amazon S3, API Gateway, AWS Lambda, CloudWatch, and X-Ray.

## Local Setup

1. Install the .NET SDK.
2. Install Docker Desktop.
3. Start PostgreSQL and pgAdmin:

```powershell
docker compose up -d
```

4. Restore and build the web app:

```powershell
dotnet restore CloudSecOps.Web/CloudSecOps.Web.csproj
dotnet build CloudSecOps.Web/CloudSecOps.Web.csproj
```

5. Run the MVC app:

```powershell
cd CloudSecOps.Web
dotnet run
```

## Local URLs

- Web app HTTP: http://localhost:5045
- Web app HTTPS: https://localhost:7045
- pgAdmin: http://localhost:5051
- pgAdmin email: admin@cloudsecops.local
- pgAdmin password: admin123

## Git Workflow

- Branch from `develop`.
- Use feature branches such as `feature/repo-skeleton`.
- Open pull requests into `develop`.
- Do not push directly to `main`.
- Keep Task 2 AWS/serverless implementation out of Task 1 branches unless explicitly approved.

## Team Role Division

- Member 1: Backend and Database Lead. ASP.NET models, EF Core, PostgreSQL, Identity foundation, RDS later.
- Member 2: Frontend and Cloud Integration Lead. Razor Views, Bootstrap UI, navigation/layout, Elastic Beanstalk later, S3/Lambda/API Gateway later.
- Member 3: Documentation and Architecture Lead. Diagrams, slide deck, report structure, screenshot explanation, references.
- Member 4: QA, Testing, Monitoring Lead. Test cases, user manual, screenshots, CloudWatch/X-Ray later, demo validation.

## Safety Note

CloudSecOps is a defensive platform only. The repository must not include offensive security tooling or attack automation.
