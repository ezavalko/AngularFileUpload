using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFileUpload.Models
{
    public class FileInfoModel
    {
        public Guid UniqueName { get; set; }
        public string Extension { get; set; }
        public string UploadName { get; set; }

        public string FullName => $"{UniqueName.ToString()}{Extension}";
    }
}
