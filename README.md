# Teamsync - Online Organization Management

Teamsync is a web application built with ASP.NET Core MVC that allows users to manage tasks, teams, meetings, customer support, public holidays, user accounts, project archives, and private notes.

<img src="https://user-images.githubusercontent.com/76687631/235875245-3342f80c-cfeb-449b-97f1-e6c3a9b93498.svg" alt="Team Sync" style="width:100%;"/>

## Module Outline
### User Management
User Creation, Role Allocation, Note taking etc.

### Team Management
Involves Team creation and management, Log maintenance, Attendance record checking, task reviews, project archival etc.

### Calendar Management
Meeting scheduling, Public holiday marking and an abstracted view based upon authenticated user.

### Communication
Updates for all types of users with status checking.

## Detailed Description of features
### 1. User Authentication and Authorization
The application uses ASP.NET Identity for user authentication and authorization.

### 2. Task Management
Users can create, update, and delete tasks. Tasks can be assigned to team members and can be filtered by team. Users can also request task review from managers.

### 3. Team Management
Users can create and delete teams.

### 4. Expense Management
Teams can manage expenses of the project and download CSV of the expense sheet.

### 5. Meeting Management
Users can schedule and manage meetings.

### 6. Customer Support
Users can seek customer support from admin.

### 7. Public Holidays
Users can manage public holidays for the organization.

### 8. Calendar Events View
Abstracted view of public holidays and calendars based on the team.

### 9. User Account Management
Administrators can create user accounts.

### 10. Project Archives
Managers can add completed projects to archives.

### 11. Private Note Taking
Users can take private notes based on priority.

### 12. Mail Enabling
Mail will be sent in the event of team creation, project archival, and meeting scheduling.

### 13. Attendance
Attendance Log for users to mark their presence status and input the work done.

### 14. Dashboard
Displays tasks and meetings nearing the deadline.

### 15. Spotify API
Users can listen to music from time to time for relaxation.

## Technology Used
- HTML, CSS, JS
- Bootstrap, AJAX/jQuery
- FullCalendar.io, Google Fonts
- Spotify API
- ASP.NET MVC
- Microsoft Server SQL (2017)
- Amazon Relational Database Service (RDS)

## Conclusion
Teamsync provides a comprehensive set of tools for managing an organization's daily activities. From task and team management to customer support and project archival, it provides everything needed to manage a modern organization. With user authentication and authorization, and user account management, it provides a secure and efficient way to manage the organization's resources. Its integration with Amazon RDS provides scalable, reliable storage for the organization's data. 
