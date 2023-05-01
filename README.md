# Online Organization Management System

This is a web application built with ASP.NET Core MVC, which allows users to manage tasks, teams, meetings, customer support, public holidays, user accounts, project archives, and private notes.

## Features
The application currently supports the following features:

### 1. User authentication and authorization: 
- The application uses ASP.NET Identity for user authentication and authorization.

### 2. Task management: 
- Users can create, update, and delete tasks. Tasks can be assigned to team members and can be filtered by team. Users can also request task review from managers.

### 3. Team management: 
- Users can create and delete teams.

### 4. Expense management: 
- Teams can manage expenses of the Team.

### 5. Meeting management: 
- Users can schedule and manage meetings.

### 6. Customer support: 
- Users can seek customer support to admin.

### 7. Public holidays: 
- Users can manage public holidays for the organization.

### 8. Calendar events view: 
- Abstracted view of Public holidays and calendars based on the team.

### 9. User account management: 
- Administrators can create and manage user accounts.

### 10. Project archives: 
- Managers can manage project archives.

### 11. Private note taking: 
- Users can take private notes based on priority.

### 12. Mail Enabling:
- Mail will be sent in the event Team Creation, Project Archival and Meeting scheduling.

## Requirements
To run this application, you will need:

- Visual Studio or Visual Studio Code
- .NET 6.0 SDK or later
- SQL Server or SQL Server Express installed on your local machine

## Installation
To run this application, follow these steps:

1. Clone the repository to your local machine.
2. Open the project in Visual Studio or Visual Studio Code.
3. Open the `appsettings.json` file and change the `DefaultConnection` string to point to your SQL Server instance.
4. In the Package Manager Console, run the following command to create the database: `Update-Database`.
5. Run the application in Visual Studio or Visual Studio Code.
