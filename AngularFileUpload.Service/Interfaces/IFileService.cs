using AngularFileUpload.Data.Models;
using AngularFileUpload.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularFileUpload.Service.Interfaces
{
    public interface IFileService
    {
        void AddFile(File newFile);
        List<File> GetAllFiles();
    }
}
