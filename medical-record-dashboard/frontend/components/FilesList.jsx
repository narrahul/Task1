'use client';

import FilePreviewCard from './FilePreviewCard';

export default function FilesList({ files, onFileDeleted }) {
  if (files.length === 0) {
    return (
      <div className="bg-gray-50 rounded-lg p-6">
        <div className="text-center py-8">
          <div className="bg-white rounded-lg p-4 mb-4 border border-gray-200">
            <div className="text-gray-400 text-sm transform -rotate-12 mb-2">
              Preview of Sent file
            </div>
          </div>
          <p className="text-gray-600 text-sm max-w-md mx-auto">
            By default, this space will be empty. It should only appear when 
            I add files. I can add one file at a time, but multiple times. 
            For example: first file - lab reports, second file - medical 
            prescription. These should be displayed here in a responsive manner.
          </p>
          <div className="mt-4 flex space-x-4 justify-center">
            <button disabled className="bg-yellow-400 hover:bg-yellow-500 text-black px-4 py-2 rounded-md font-medium transition-colors opacity-50 cursor-not-allowed">
              View
            </button>
            <button disabled className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md font-medium transition-colors opacity-50 cursor-not-allowed">
              Delete
            </button>
          </div>
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