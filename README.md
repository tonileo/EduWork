# EduWork

**EduWork** is a web application developed to track employee working hours across multiple projects, as well as manage annual leave days and sick days. The system provides insightful analysis to managers, aiming to enhance productivity and streamline project management.

[Access the live app here](https://eduwork2024.azurewebsites.net/) (Please wait ~20 seconds for the initial load).

## Technologies

- **Back-end**: ASP.NET Core Web API (.Net 8)
- **Front-end**: Blazor WebAssembly
- **Database**: Microsoft SQL Server
- **Authentication**: Azure Entra and JWT
- **Unit testing**: xUnit
- **End-to-End testing**: bUnit
- **CI/CD Pipeline**: GitHub Actions

## Features

### 1. **Login & Authentication**
a) **Microsoft Account Integration**: Employees can register and log in using their Microsoft accounts, authenticated via Azure Entra.

b) **JWT Authentication**: Employees can register and log in through the app.
  
### 2. **Main Page**
- **Work Time Management**: Employees can input, update, and delete their working hours with a focus on an optimized employee experience (UX).
- **Daily Overview**: Employees can view all entered working hours for a selected date in an intuitive display.

### 3. **Input History**
- **Monthly Overview**: Displays all work entries for the current month, with color-coded representation of completed and pending work hours for each day.
- **Date Navigation**: Clicking on any work date redirects employees to the time input page for that specific date.
- **Date Preview**: Employees can preview detailed information about their work hours on any date by clicking on the respective row.
- **Filtering Options**: Employees can filter their work entries by:
  - Last month
  - Current month
  - Current quarter

### 4. **Project Statistics**
- **Data Visualization**: Employees can filter their work time entries and view them through interop chart generated with Chart.js.

### 5. **Profile Page**
- **Employee Role & Project Info**: Displays the employee’s current role and projects they are involved in, with short statistics.
- **Leave Management**: Employees can view and add their annual leave and sick leave days.

### 6. **Admin Features**
- **Employee Management**: Admins can view all employees, including their roles and current project.
- **Employee Profiles**: Admins can access and view all employee profiles.
- **Admin Input History**: Admins can see and filter history of all employees' work hour entries.
- **Admin Statistics**: Provides in-depth, color-coded statistics on project work categorized by project type. Admins can also filter results by employee for more granular data.

### 7. **CI/CD Pipeline**

EduWork uses GitHub Actions to automate the build, test, and deploy the application on Azure. The CI/CD pipeline is triggered on push and pull requests to ensure code integrity and facilitate continuous integration.

## Setup and Installation

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server

### Installation Steps
1. **Clone the repository**:
   ```bash
   git clone https://github.com/tonileo/EduWork.git
   
2. **Navigate to the project directory**:
   ```bash
   cd EduWork

3. **Restore the required packages**:
   ```bash
   dotnet restore

4. **Set up the database and run the backend side**:
   - Update the connection string in appsettings.json (inside EduWork.WebApi) to point to your SQL Server instance.
   - Apply migrations to set up the database schema:

   ~~~ bash
   cd EduWork.WebApi
   ~~~
   ~~~ bash
   dotnet ef database update
   ~~~
   ~~~ bash
   dotnet run
   ~~~ 

5. **Run the frontend side in new terminal**:

   ~~~ bash
   cd EduWork.UI
   ~~~
   ~~~ bash
   dotnet run
   ~~~ 

7. **Access the app**:
   - Open your browser and navigate to https://localhost:7041.
   - Also you can navigate to https://localhost:7104/swagger for Swagger.
