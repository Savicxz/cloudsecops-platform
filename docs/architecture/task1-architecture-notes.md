# Task 1 Architecture Notes

Task 1 uses a server-based architecture:

Browser -> ASP.NET Core MVC on local machine -> PostgreSQL local Docker container

The same server-based pattern can later move toward:

Browser -> ASP.NET Core MVC on AWS Elastic Beanstalk -> Amazon RDS PostgreSQL

Task 1 focuses on the MVC application structure, Entity Framework Core data foundation, ASP.NET Core Identity foundation, local PostgreSQL, and role-based module separation. The app is intentionally a repository skeleton with placeholder services and pages so the team can divide work cleanly.

Task 2 will extend the platform with cloud and serverless architecture later.
