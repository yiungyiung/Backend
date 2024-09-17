#SCDD Backend
# Project Documentation

## Overview

This project is a comprehensive web application designed for managing questionnaires, assignments, vendor hierarchies, and responses. It features various endpoints for interacting with these entities, complete with role-based access control and file management capabilities. 

## Features

- **Questionnaire Management**: Create, retrieve, and manage questionnaires and their associated questions.
- **Assignment Management**: Assign questionnaires to vendors, view assignments by ID or vendor, and retrieve statistics.
- **Response Management**: Save and retrieve responses for assignments and questionnaires.
- **Vendor Management**: Add, retrieve, and categorize vendors. Manage vendor hierarchy and access by user ID.
- **File Management**: Upload and download files.

## Architecture

The application is built using ASP.NET Core and follows a clean architecture with separation of concerns:

- **Controllers**: Handle HTTP requests and responses.
- **Services**: Contain business logic and interact with repositories.
- **DTOs**: Data transfer objects used for request and response models.
- **Repositories**: Interface with the database (not detailed here but implied in service layers).

## Endpoints

### 1. **Admin and User Management**

- **VendorController**
  - `GET /api/vendor/vendors` - Retrieve all vendors.
  - `POST /api/vendor/add` - Add a new vendor.
  - `GET /api/vendor/{id}` - Retrieve a vendor by ID.
  - `GET /api/vendor/categories` - Retrieve vendor categories.
  - `GET /api/vendor/tiers` - Retrieve vendor tiers.
  - `GET /api/vendor/vendors/byTier/{tierId}` - Retrieve vendors by tier ID.
  - `GET /api/vendor/byUserId/{userId}` - Retrieve vendor ID by user ID.
  - `GET /api/vendor/byVendorID/{vendorID}` - Retrieve vendor by vendor ID.
  - `GET /api/vendor/vendors/byCategory` - Retrieve vendors grouped by category.

### 2. **Questionnaire Management**

- **QuestionnaireController**
  - `POST /api/questionnaire` - Create a new questionnaire.
  - `GET /api/questionnaire/getallquestionnaires` - Retrieve all questionnaires with questions.
  - `GET /api/questionnaire/{questionnaireId}` - Retrieve questions by questionnaire ID.

### 3. **Assignment Management**

- **QuestionnaireAssignmentController**
  - `POST /api/questionnaireassignment` - Create a new assignment.
  - `GET /api/questionnaireassignment/{id}` - Retrieve assignment by ID.
  - `GET /api/questionnaireassignment/vendor/{vendorId}` - Retrieve assignments by vendor ID.
  - `GET /api/questionnaireassignment` - Retrieve all assignments.
  - `GET /api/questionnaireassignment/questionnaire/{questionnaireId}` - Retrieve assignments by questionnaire ID.
  - `GET /api/questionnaireassignment/statistics` - Retrieve assignment statistics.

### 4. **Response Management**

- **ResponseController**
  - `POST /api/response` - Save a single response.
  - `POST /api/response/bulk` - Save multiple responses.
  - `GET /api/response/assignment/{assignmentId}` - Retrieve responses for an assignment.
  - `GET /api/response/questionnaire/{questionnaireId}` - Retrieve all responses for a questionnaire.
  - `GET /api/response/assignment/{assignmentId}/question/{questionId}` - Retrieve response for a specific assignment and question.
  - `POST /api/response/upload` - Upload a file.
  - `GET /api/response/download` - Download a file.

### 5. **Vendor Hierarchy**

- **VendorHierarchyController**
  - `GET /api/vendorhierarchy` - Retrieve the vendor hierarchy.
### These are the few important API endpoints find more in the controllers folder
## Configuration

### `appsettings.json`

Contains configuration settings such as:
- Database connection strings
- File upload paths
- Logging settings

### `launchSettings.json`

Defines profiles for running the application:
- **`iisSettings`**: 
  - **`windowsAuthentication`**: `false`
  - **`anonymousAuthentication`**: `true`
  - **`iisExpress`**:
    - **`applicationUrl`**: `http://localhost:3267`
    - **`sslPort`**: `0`

### `Program.cs`

Initializes and configures the application:
- Sets up the web host and application services.
- Configures middleware for handling HTTP requests and responses.

## Roles and Permissions

- **Admin**: Full access to all endpoints.
- **Manager**: Access to most endpoints with management capabilities.
- **Analyst**: Read-only access to certain endpoints.
- **Vendor**: Limited access for managing responses and vendor-related tasks.

You're right; setting up the database and applying migrations are crucial steps. Here's the revised **Running the Application** section with these additions:

---

## Running the Application

1. **Clone the Repository**:
   ```bash
   git clone https://your-repo-url.git
   ```

2. **Navigate to the Project Directory**:
   ```bash
   cd your-project-directory
   ```

3. **Configure the Database**:
   - Open `appsettings.json` and update the connection string to match your database configuration:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
     }
     ```

4. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

5. **Apply Migrations**:
   - Run the following command to apply pending migrations to the database:
     ```bash
     dotnet ef database update
     ```

6. **Run the Application**:
   ```bash
   dotnet run
   ```

7. **Access the Application**:
   Open a browser and navigate to `http://localhost:3267`.

8. Login after seeding will be admin@admin.com and password will be admin
