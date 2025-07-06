'use client';

import { useState } from 'react';
import toast from 'react-hot-toast';
import api from '@/lib/axios';

const FILE_TYPES = [
  'Lab Report',
  'Prescription',
  'X-Ray',
  'Blood Report',
  'MRI Scan',
  'CT Scan',
];

export default function FileUploadForm({ onFileUploaded }) {
  const [selectedFile, setSelectedFile] = useState(null);
  const [fileType, setFileType] = useState('');
  const [fileName, setFileName] = useState('');
  const [uploading, setUploading] = useState(false);

  const handleFileUpload = async (e) => {
    e.preventDefault();
    
    if (!selectedFile || !fileType || !fileName) {
      toast.error('Please fill all fields');
      return;
    }

    const formData = new FormData();
    formData.append('fileType', fileType);
    formData.append('fileName', fileName);
    formData.append('file', selectedFile);

    setUploading(true);
    try {
      const response = await api.post('/file/upload', formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      });
      
      onFileUploaded(response.data);
      toast.success('File uploaded successfully');
      
      // Reset form
      setSelectedFile(null);
      setFileType('');
      setFileName('');
      document.getElementById('file-input').value = '';
    } catch (error) {
      toast.error(error.response?.data?.message || 'Failed to upload file');
    } finally {
      setUploading(false);
    }
  };

  return (
    <div>
      <h2 className="text-xl font-semibold text-blue-600 mb-4 border-b-2 border-blue-600 pb-2">
        Please Add Your Medical Records
      </h2>
      
      <form onSubmit={handleFileUpload} className="space-y-4">
        <div>
          <select
            value={fileType}
            onChange={(e) => setFileType(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          >
            <option value="">Select file type</option>
            {FILE_TYPES.map((type) => (
              <option key={type} value={type}>
                {type}
              </option>
            ))}
          </select>
        </div>

        <div>
          <input
            type="text"
            value={fileName}
            onChange={(e) => setFileName(e.target.value)}
            placeholder="Enter Name of File..."
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div className="flex space-x-4">
          <input
            type="file"
            onChange={(e) => setSelectedFile(e.target.files[0])}
            accept=".pdf,.jpg,.jpeg,.png,.gif,.bmp"
            className="hidden"
            id="file-input"
          />
          <label
            htmlFor="file-input"
            className="flex-1 px-4 py-2 border border-gray-300 rounded-md cursor-pointer hover:bg-gray-50 text-center"
          >
            {selectedFile ? selectedFile.name : 'Select file'}
          </label>
          <button
            type="submit"
            disabled={uploading}
            className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-md font-medium transition-colors disabled:opacity-50"
          >
            {uploading ? 'Uploading...' : 'Submit'}
          </button>
        </div>
      </form>
    </div>
  );
}