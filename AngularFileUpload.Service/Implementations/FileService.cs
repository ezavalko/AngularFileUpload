using AngularFileUpload.Data.Models;
using AngularFileUpload.Data.Repositories;
using AngularFileUpload.Service.Interfaces;
using AngularFileUpload.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngularFileUpload.Service.Implementations
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void AddFile(File newFile)
        {
            _fileRepository.Add(newFile);
            _fileRepository.CommitAsync();
        }

        public List<File> GetAllFiles()
        {
            return _fileRepository.GetAllIncludingProperties(nameof(File.User)).ToList();
        }
    }
}
