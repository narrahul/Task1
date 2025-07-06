'use client';

import { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import api from '@/lib/axios';
import toast from 'react-hot-toast';

// Import all components
import Header from '@/components/Header';
import UserProfile from '@/components/UserProfile';
import FileUploadForm from '@/components/FileUploadForm';
import FilesList from '@/components/FilesList';

export default function Dashboard() {
  const router = useRouter();
  const [user, setUser] = useState(null);
  const [files, setFiles] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchUserData();
    fetchFiles();
  }, []);

  const fetchUserData = async () => {
    try {
      const response = await api.get('/auth/me');
      setUser(response.data);
    } catch (error) {
      toast.error('Failed to fetch user data');
      router.push('/');
    } finally {
      setLoading(false);
    }
  };

  const fetchFiles = async () => {
    try {
      const response = await api.get('/file');
      setFiles(response.data);
    } catch (error) {
      toast.error('Failed to fetch files');
    }
  };

  const handleFileUploaded = (newFile) => {
    setFiles([newFile, ...files]);
  };

  const handleFileDeleted = (fileId) => {
    setFiles(files.filter(file => file.id !== fileId));
  };

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <div className="text-lg">Loading...</div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header Component */}
      <Header user={user} />

      <div className="container mx-auto px-4 py-8">
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          
          {/* Left Column */}
          <div className="space-y-6">
            {/* User Profile Component */}
            <UserProfile user={user} setUser={setUser} />
            
            {/* Files List Component - Below Profile */}
            <FilesList files={files} onFileDeleted={handleFileDeleted} />
          </div>

          {/* Right Column - File Upload Form */}
          <div>
            <FileUploadForm onFileUploaded={handleFileUploaded} />
          </div>
        </div>
      </div>
    </div>
  );
}