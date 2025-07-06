# Medical Record Dashboard

A full-stack application for managing medical records with user authentication and file upload capabilities.

## Features

- User Registration and Login (Session-based authentication)
- User Profile Management with Profile Picture Upload
- Medical File Upload with Multiple File Types Support
- File Preview and Management
- Responsive Design

## Tech Stack

### Frontend
- Next.js 14 (App Router)
- React 18
- TailwindCSS
- React Hook Form
- Axios
- Lucide React Icons

### Backend
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- MySQL Database (Pomelo.EntityFrameworkCore.MySql)
- BCrypt for Password Hashing
- Session-based Authentication

## Prerequisites

- Node.js 18+ and npm
- .NET 8.0 SDK
- MySQL Server
- Git

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd medical-record-dashboard
```

### 2. Database Setup

1. Make sure MySQL is running on your machine
2. Create the database and tables:

```bash
mysql -u root -p < backend/database-schema.sql
```

3. Update the connection string in `backend/MedicalRecordAPI/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MedicalRecordDB;User=root;Password=yourpassword;"
}
```

### 3. Backend Setup

1. Navigate to the backend directory:

```bash
cd backend/MedicalRecordAPI
```

2. Restore NuGet packages:

```bash
dotnet restore
```

3. Run the backend API:

```bash
dotnet run
```

The API will start at `http://localhost:5000`

### 4. Frontend Setup

1. Open a new terminal and navigate to the frontend directory:

```bash
cd frontend
```

2. Install dependencies:

```bash
npm install
```

3. Run the development server:

```bash
npm run dev
```

The application will be available at `http://localhost:3000`

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout
- `GET /api/auth/me` - Get current user

### User Profile
- `PUT /api/user/profile` - Update user profile
- `POST /api/user/profile-image` - Upload profile image

### File Management
- `POST /api/file/upload` - Upload medical file
- `GET /api/file` - Get all user files
- `GET /api/file/{id}` - Get file content
- `DELETE /api/file/{id}` - Delete file

## Supported File Types

- Lab Report
- Prescription
- X-Ray
- Blood Report
- MRI Scan
- CT Scan

Supported formats: PDF, JPG, JPEG, PNG, GIF, BMP (Max 10MB)

## Project Structure

```
medical-record-dashboard/
├── frontend/
│   ├── app/
│   │   ├── dashboard/
│   │   ├── globals.css
│   │   ├── layout.jsx
│   │   └── page.jsx
│   ├── components/
│   │   ├── FilesList.jsx
│   │   ├── FileUploadForm.jsx
│   │   └── UserProfile.jsx
│   ├── lib/
│   │   └── axios.js
│   └── public/
├── backend/
│   └── MedicalRecordAPI/
│       ├── Controllers/
│       ├── Data/
│       ├── DTOs/
│       ├── Models/
│       ├── Services/
│       └── Program.cs
└── README.md
```

## Security Features

- Password hashing using BCrypt
- Session-based authentication
- File type validation
- File size limits
- CORS configuration
- Input validation

## Development Notes

- The backend API uses session-based authentication with cookies
- Uploaded files are stored in `wwwroot/uploads` directory
- Profile images are stored in `wwwroot/profile-images` directory
- All API responses include proper error handling and status codes