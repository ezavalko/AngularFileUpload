using AngularFileUpload.Controllers;
using AngularFileUpload.Data.Models;
using AngularFileUpload.Data.Repositories;
using AngularFileUpload.Helpers;
using AngularFileUpload.Service.Implementations;
using AngularFileUpload.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularFileUpload.Test
{
    public class FileControllerTests
    {
        [Test]
        public void GetAllTest()
        {
            var mockRepo = new Mock<IFileRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockFileHelper = new Mock<IFileHelper>();
            var settingsMock = new Mock<IOptionsSnapshot<AppSettings>>();

            mockRepo.Setup(repo => repo.GetAllIncludingProperties(nameof(File.User)))
                .Returns(GetFakeData());

            var fileService = new FileService(mockRepo.Object);

            var files = fileService.GetAllFiles();

            mockMapper.Setup(repo => repo.Map<List<FileViewModel>>(files))
                .Returns(GetMapperData());

            var controller = new FileController(fileService, settingsMock.Object, mockMapper.Object, mockFileHelper.Object);

            var groups = controller.GetFiles();

            Assert.AreEqual(3, groups.Count);
        }

        private IQueryable<File> GetFakeData()
        {
            var files = new List<File>();

            files.Add(new File()
            {
                FileName = "test.jpg",
                Id = Guid.NewGuid(),
                Extension = "jpg",
                UserId = Guid.NewGuid().ToString()
            });

            return files.AsQueryable<File>();
        }

        private List<FileViewModel> GetMapperData()
        {
            var files = new List<FileViewModel>();

            files.Add(new FileViewModel()
            {
                FileName = "test.jpg",
                Id = Guid.NewGuid(),
                Extension = ".jpg",
                UserId = Guid.NewGuid().ToString(),
                User = new UserViewModel() 
                { 
                    Email = "test@mail.com" 
                }
            });

            files.Add(new FileViewModel()
            {
                FileName = "test.txt",
                Id = Guid.NewGuid(),
                Extension = ".txt",
                UserId = Guid.NewGuid().ToString(),
                User = new UserViewModel()
                {
                    Email = "test@mail.com"
                }
            });

            files.Add(new FileViewModel()
            {
                FileName = "test.pdf",
                Id = Guid.NewGuid(),
                Extension = ".pdf",
                UserId = Guid.NewGuid().ToString(),
                User = new UserViewModel()
                {
                    Email = "test@mail.com"
                }
            });

            files.Add(new FileViewModel()
            {
                FileName = "test.pdf",
                Id = Guid.NewGuid(),
                Extension = ".pdf",
                UserId = Guid.NewGuid().ToString(),
                User = new UserViewModel()
                {
                    Email = "test@mail.com"
                }
            });

            return files;
        }
    }
}