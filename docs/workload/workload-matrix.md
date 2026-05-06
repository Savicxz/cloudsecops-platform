# Workload Matrix

| Member | Role | Code Area | Responsibilities |
| --- | --- | --- | --- |
| Developer 1 | Foundation, Administrator, Security Analyst backend | `Data`, `Models`, `Services`, `Controllers/AdminController.cs`, `Controllers/AnalystController.cs` | EF Core, Identity roles, PostgreSQL/RDS-compatible data access, administrator placeholders, analyst backend foundations |
| Developer 2 | Reporter module, frontend polish, validation, screenshots | `Controllers/ReporterController.cs`, `Views/Reporter`, `Views/IncidentReports` | Reporter dashboard, submit report placeholder, my reports placeholder, report details placeholder, form validation, frontend polish, reporter user manual screenshots |
| Developer 3 | Manager dashboard and assignment workflow | `Controllers/ManagerController.cs`, `Views/Manager` | Manager dashboard placeholder, assign analyst placeholder, incident progress monitoring placeholder |
| Developer 4 | Auditor module and completed incident review | `Controllers/AuditController.cs`, `Controllers/AuditLogsController.cs`, `Views/Audit`, `Views/AuditLogs`, `Views/EvidenceFiles` | Audit logs placeholder, completed incidents review placeholder, evidence review placeholder |

Task #1 stays limited to ASP.NET Core MVC, EF Core, ASP.NET Identity, PostgreSQL/RDS-compatible configuration, and later Elastic Beanstalk or EC2 deployment. Task #2 services are intentionally out of scope.
