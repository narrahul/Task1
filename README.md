# Medical Record Dashboard

Built this as a health file management system where users can store and organize their medical documents.

**Check it out:** [Live Demo](https://task1-1-z38e.onrender.com)

## What I Built

A simple dashboard for managing medical records. Users can sign up, upload their medical files (lab reports, prescriptions, X-rays, etc.), and access them anytime. Added a profile section where they can update their info and upload a profile pic.

## Stack Used

**Frontend:**
- Next.js 14 with App Router
- Tailwind for styling  
- React Hook Form for forms
- Axios for API calls

**Backend:**
- .NET 8 with ASP.NET Core
- MySQL database
- Entity Framework Core
- BCrypt for password hashing

## Running Locally

### Backend Setup

1. Clone the repo and navigate to backend:
```bash
cd medical-record-dashboard/backend/MedicalRecordAPI
```

2. Create a `.env` file with your MySQL details:
```
DB_SERVER=localhost
DB_PORT=3306
DB_DATABASE=MedicalRecordDB
DB_USER=root
DB_PASSWORD=yourpassword
```

3. Install dependencies and run:
```bash
dotnet restore
dotnet run
```

Backend runs on http://localhost:5000

### Frontend Setup

1. In a new terminal, go to frontend:
```bash
cd medical-record-dashboard/frontend
```

2. Install and run:
```bash
npm install
npm run dev
```

Frontend runs on http://localhost:3000

### Database

Create your database using the schema file:
```bash
mysql -u root -p < database-schema.sql
```

## Main Features

- **User Auth**: Sign up/login with session-based auth
- **File Upload**: Upload medical files (PDFs, images) up to 10MB
- **File Types**: Lab reports, prescriptions, X-rays, blood reports, MRI/CT scans
- **Profile Management**: Update email, phone, gender, and profile picture
- **File Preview**: View uploaded files in browser
- **Responsive**: Works on mobile and desktop

## API Routes

Auth stuff:
- POST `/api/auth/register`
- POST `/api/auth/login`
- POST `/api/auth/logout`

Profile:
- PUT `/api/user/profile`
- POST `/api/user/profile-image`

Files:
- POST `/api/file/upload`
- GET `/api/file`
- GET `/api/file/{id}`
- DELETE `/api/file/{id}`

## Notes

- Files are stored in `wwwroot/uploads` on the server
- Using session cookies for auth (not JWT)
- Added proper CORS for the deployed version
- Free tier on Render so first load might be slow

## Deployment

Deployed on Render:
- Frontend: https://task1-1-z38e.onrender.com
- Backend API: https://task1-6vl2.onrender.com

For deployment setup, check the docs folder.