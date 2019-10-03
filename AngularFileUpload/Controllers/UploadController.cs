using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularFileUpload.Data.Extensions;
using AngularFileUpload.Data.Models;
using AngularFileUpload.Helpers;
using AngularFileUpload.Models;
using AngularFileUpload.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularFileUpload.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IFileHelper _fileHelper;
        private readonly IFileService _fileService;

        public UploadController(IWebHostEnvironment hostingEnvironment, IFileService fileService, IFileHelper fileHelper)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileService = fileService;
            _fileHelper = fileHelper;
        }

        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            try
            {
                var userId = User.Claims.GetUserId();

                IFormFile file = Request.Form.Files.First();
                FileInfoModel fileInfo = _fileHelper.SaveFile(file);

                _fileService.AddFile(new File()
                {
                    Id = fileInfo.UniqueName,
                    Extension = fileInfo.Extension,
                    FileName = fileInfo.UploadName,
                    UserId = userId
                });

                return new JsonResult("Upload Successful.");
            }
            catch (System.Exception ex)
            {
                return new JsonResult("Upload Failed: " + ex.Message);
            }
        }

    }
}