# Online Organization Management System
This is a web application built with ASP.NET Core MVC, which allows users to manage tasks and teams within an organization.

## Features
The application currently supports the following features:
- User authentication and authorization using ASP.NET Identity
- Creation, updating, and deletion of tasks
- Assignment of tasks to team members
- Requesting task review from managers
- Creation and deletion of teams
- Filtering tasks by team

## Requirements
To run this application, you will need:
- Visual Studio or Visual Studio Code
- .NET 5.0 SDK or later
- SQL Server or SQL Server Express installed on your local machine

## Installation
To run this application, follow these steps:
1. Clone the repository to your local machine.
2. Open the project in Visual Studio or Visual Studio Code.
3. Open the `appsettings.json` file and change the `DefaultConnection` string to point to your SQL Server instance.
4. In the Package Manager Console, run the following command to create the database: `Update-Database`.
5. Run the application in Visual Studio or Visual Studio Code.
