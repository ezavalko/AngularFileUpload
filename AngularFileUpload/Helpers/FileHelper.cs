using AngularFileUpload.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AngularFileUpload.Helpers
{
    public class FileHelper : IFileHelper
    {
        private static string _fileSavePath { get; set; }
        private static string _saveFolder { get; set; }

        private readonly IWebHostEnvironment _environment;

        public FileHelper(IWebHostEnvironment environment)
        {
            _environment = environment;

            _fileSavePath = $"{_environment.WebRootPath}/{_saveFolder}";
        }

        public static void Initialize(string path)
        {
            _saveFolder = path;
        }

        public FileInfoModel SaveFile(IFormFile file)
        {
            if (!Directory.Exists(_fileSavePath))
            {
                Directory.CreateDirectory(_fileSavePath);
            }

            FileInfoModel model = new FileInfoModel();

            model.UniqueName = Guid.NewGuid();
            model.Extension = Path.GetExtension(file.FileName);
            model.UploadName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            var path = Path.Combine(_fileSavePath, model.FullName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return model;
        }

        public Stream GetFileStream(string fileName)
        {
            var path = Path.Combine(_fileSavePath, fileName);
            return new FileStream(path, FileMode.Open);
        }

        public byte[] GetFileBytes(string fileName)
        {
            var path = Path.Combine(_fileSavePath, fileName);
            return File.ReadAllBytes(path);
        }

        public void DeleteFile(string fileName)
        {
            try
            {
                var path = Path.Combine(_fileSavePath, fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch { }
        }

        public bool CheckFile(string fileName)
        {
            return File.Exists(Path.Combine(_fileSavePath, fileName));
        }

        public void DeleteFiles(params string[] files)
        {
            foreach (var file in files)
            {
                DeleteFile(file);
            }
        }
    }

    public interface IFileHelper
    {
        FileInfoModel SaveFile(IFormFile file);
        Stream GetFileStream(string fileName);
        void DeleteFile(string fileName);
        void DeleteFiles(params string[] files);
        bool CheckFile(string fileName);
        byte[] GetFileBytes(string fileName);
    }
}
