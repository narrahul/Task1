'use client';

import { useState } from 'react';
import { User } from 'lucide-react';
import toast from 'react-hot-toast';
import api from '@/lib/axios';

export default function UserProfile({ user, setUser }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editData, setEditData] = useState({
    email: user?.email || '',
    phoneNumber: user?.phoneNumber || '',
    gender: user?.gender || ''
  });

  const handleProfileUpdate = async () => {
    try {
      const response = await api.put('/user/profile', editData);
      setUser(response.data);
      toast.success('Profile updated successfully');
      setIsEditing(false);
    } catch (error) {
      toast.error(error.response?.data?.message || 'Failed to update profile');
    }
  };

  const handleProfileImageUpload = async (e) => {
    const file = e.target.files[0];
    if (!file) return;

    const formData = new FormData();
    formData.append('image', file);

    try {
      const response = await api.post('/user/profile-image', formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
      });
      setUser({ ...user, profileImagePath: response.data.profileImagePath });
      toast.success('Profile image updated successfully');
    } catch (error) {
      toast.error(error.response?.data?.message || 'Failed to upload image');
    }
  };

  return (
    <div className="bg-blue-100 rounded-lg p-6 relative">
      <div className="absolute top-4 right-4 bg-yellow-200 px-3 py-1 rounded text-sm font-medium">
        {user?.id || 'FH54376V768'}
      </div>
      
      <div className="flex items-start space-x-4 mb-6">
        <div className="relative">
          <div className="w-20 h-20 rounded-full bg-gray-300 flex items-center justify-center overflow-hidden">
            {user?.profileImagePath ? (
              <img
                src={`${process.env.NEXT_PUBLIC_API_URL?.replace('/api', '') || 'http://localhost:5000'}${user.profileImagePath}`}
                alt="Profile"
                className="w-full h-full object-cover"
              />
            ) : (
              <User size={32} className="text-gray-600" />
            )}
          </div>
          <input
            type="file"
            accept="image/*"
            onChange={handleProfileImageUpload}
            className="hidden"
            id="profile-image-upload"
          />
        </div>
        <div className="flex-1">
          <h2 className="text-xl font-semibold text-gray-800 mb-1">
            {user?.fullName || 'User Name'}
          </h2>
          <label
            htmlFor="profile-image-upload"
            className="text-sm text-blue-600 hover:text-blue-700 cursor-pointer"
          >
            Change Profile Picture
          </label>
        </div>
      </div>

      <div className="space-y-4">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Email :
          </label>
          <input
            type="email"
            value={editData.email}
            onChange={(e) => setEditData({...editData, email: e.target.value})}
            disabled={!isEditing}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:bg-gray-100"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-1">
            Phone :
          </label>
          <input
            type="tel"
            value={editData.phoneNumber}
            onChange={(e) => setEditData({...editData, phoneNumber: e.target.value})}
            disabled={!isEditing}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:bg-gray-100"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">
            Gender :
          </label>
          <div className="flex space-x-4">
            <label className="flex items-center">
              <input
                type="radio"
                value="male"
                checked={editData.gender === 'male'}
                onChange={(e) => setEditData({...editData, gender: e.target.value})}
                disabled={!isEditing}
                className="mr-2"
              />
              <span className="text-sm">Male</span>
            </label>
            <label className="flex items-center">
              <input
                type="radio"
                value="female"
                checked={editData.gender === 'female'}
                onChange={(e) => setEditData({...editData, gender: e.target.value})}
                disabled={!isEditing}
                className="mr-2"
              />
              <span className="text-sm">Female</span>
            </label>
          </div>
        </div>
      </div>

      <div className="mt-6 flex justify-center space-x-4">
        {!isEditing ? (
          <button
            onClick={() => setIsEditing(true)}
            className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-md font-medium transition-colors"
          >
            Edit Profile
          </button>
        ) : (
          <>
            <button
              onClick={handleProfileUpdate}
              className="bg-yellow-400 hover:bg-yellow-500 text-black px-6 py-2 rounded-md font-medium transition-colors"
            >
              Save
            </button>
            <button
              onClick={() => {
                setIsEditing(false);
                setEditData({
                  email: user?.email || '',
                  phoneNumber: user?.phoneNumber || '',
                  gender: user?.gender || ''
                });
              }}
              className="bg-gray-400 hover:bg-gray-500 text-white px-6 py-2 rounded-md font-medium transition-colors"
            >
              Cancel
            </button>
          </>
        )}
      </div>
    </div>
  );
}