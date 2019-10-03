using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularFileUpload.Data.Models;
using AngularFileUpload.Helpers;
using AngularFileUpload.Models;
using AngularFileUpload.Service.Interfaces;
using AngularFileUpload.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MimeMapping;

namespace AngularFileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly AppSettings _settings;
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;

        public FileController(IFileService fileService, IOptionsSnapshot<AppSettings> settings, IMapper mapper, IFileHelper fileHelper)
        {
            _fileService = fileService;
            _settings = settings.Value;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        [Route("get-files")]
        public List<FileGroupModel> GetFiles()
        {
            var files = _fileService.GetAllFiles();
            var mappedFiles = _mapper.Map<List<FileViewModel>>(files).GroupBy(x => x.Extension.ToLowerInvariant());
            return mappedFiles.Select(x => 
                new FileGroupModel() 
                { 
                    Extension = x.Key, 
                    Files = x.ToList() 
                }).ToList();
        }

        [Route("get-file/{fileName}")]
        public FileResult GetFile([FromRoute]string fileName)
        {
            var fileBytes = _fileHelper.GetFileBytes(fileName);
            
            return new FileContentResult(fileBytes, MimeUtility.GetMimeMapping(fileName));
        }
    }
}