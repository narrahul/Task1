'use client';

import FilePreviewCard from './FilePreviewCard';

export default function FilesList({ files, onFileDeleted }) {
  if (files.length === 0) {
    return (
      <div className="bg-gray-50 rounded-lg p-6">
        <h3 className="text-lg font-semibold text-gray-800 mb-4">Uploaded Files</h3>
        <div className="text-center py-12">
          <p className="text-gray-500">No files uploaded yet</p>
        </div>
      </div>
    );
  }

  return (
    <div className="bg-gray-50 rounded-lg p-6">
      <h3 className="text-lg font-semibold text-gray-800 mb-4">Uploaded Files</h3>
      <div className="space-y-4">
        {files.map((file) => (
          <FilePreviewCard
            key={file.id}
            file={file}
            onDelete={onFileDeleted}
          />
        ))}
      </div>
    </div>
  );
}