using AngularFileUpload.Data;
using AngularFileUpload.Data.Models;
using AngularFileUpload.Data.Repositories;
using AngularFileUpload.Service.Implementations;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngularFileUpload.Test
{
    public class FileServiceTests
    {
        [Test]
        public void AllFiles()
        {
            var mockRepo = new Mock<IFileRepository>();
            var settingsMock = new Mock<IOptionsSnapshot<AppSettings>>();

            mockRepo.Setup(repo => repo.GetAllIncludingProperties(nameof(File.User)))
                .Returns(GetFakeData());

            var fileService = new FileService(mockRepo.Object);

            Assert.AreEqual(fileService.GetAllFiles().Count(), GetFakeData().Count());
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

            files.Add(new File()
            {
                FileName = "test3.jpg",
                Id = Guid.NewGuid(),
                Extension = "jpg",
                UserId = Guid.NewGuid().ToString()
            });

            files.Add(new File()
            {
                FileName = "test1.jpg",
                Id = Guid.NewGuid(),
                Extension = "jpg",
                UserId = Guid.NewGuid().ToString()
            });

            files.Add(new File()
            {
                FileName = "test2.jpg",
                Id = Guid.NewGuid(),
                Extension = "jpg",
                UserId = Guid.NewGuid().ToString()
            });


            return files.AsQueryable<File>();
        }
    }
}
