'use client';

import { User } from 'lucide-react';
import { useRouter } from 'next/navigation';
import toast from 'react-hot-toast';
import api from '@/lib/axios';

export default function Header({ user }) {
  const router = useRouter();

  const handleLogout = async () => {
    try {
      await api.post('/auth/logout');
      toast.success('Logged out successfully');
      router.push('/');
    } catch (error) {
      toast.error('Failed to logout');
    }
  };

  return (
    <div className="bg-blue-600 text-white p-4">
      <div className="flex justify-between items-center">
        <div className="flex items-center space-x-2">
          <div className="bg-white text-blue-600 px-3 py-1 rounded">
            <span className="font-bold">hfiles</span>
          </div>
          <span className="text-sm font-medium">HEALTH FILES</span>
        </div>
        <div className="flex items-center space-x-4">
          <div className="w-10 h-10 rounded-full bg-gray-300 flex items-center justify-center overflow-hidden">
            {user?.profileImagePath ? (
              <img
                src={`${process.env.NEXT_PUBLIC_API_URL?.replace('/api', '') || 'http://localhost:5000'}${user.profileImagePath}`}
                alt="Profile"
                className="w-full h-full object-cover"
              />
            ) : (
              <User size={24} className="text-gray-600" />
            )}
          </div>
          <button
            onClick={handleLogout}
            className="text-sm hover:text-gray-200 transition-colors"
          >
            Logout
          </button>
        </div>
      </div>
    </div>
  );
}