'use client';

import toast from 'react-hot-toast';
import api from '@/lib/axios';

export default function FilePreviewCard({ file, onDelete }) {
  const handleViewFile = () => {
    const fileUrl = `http://localhost:5000/api/file/${file.id}`;
    window.open(fileUrl, '_blank');
  };

  const handleDeleteFile = async () => {
    try {
      await api.delete(`/file/${file.id}`);
      onDelete(file.id);
      toast.success('File deleted successfully');
    } catch (error) {
      toast.error('Failed to delete file');
    }
  };

  return (
    <div className="bg-white rounded-lg p-4 border border-gray-200">
      <div className="flex items-center justify-between mb-2">
        <div>
          <h3 className="font-medium text-gray-800">{file.fileName}</h3>
          <p className="text-sm text-gray-600">{file.fileType}</p>
        </div>
        <div className="text-xs text-gray-500">
          {new Date(file.uploadedAt).toLocaleDateString()}
        </div>
      </div>
      <div className="flex space-x-2">
        <button
          onClick={handleViewFile}
          className="bg-yellow-400 hover:bg-yellow-500 text-black px-4 py-2 rounded-md font-medium transition-colors text-sm"
        >
          View
        </button>
        <button
          onClick={handleDeleteFile}
          className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md font-medium transition-colors text-sm"
        >
          Delete
        </button>
      </div>
    </div>
  );
}