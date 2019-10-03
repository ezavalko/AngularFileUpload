using AngularFileUpload.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFileUpload.Models
{
    public class FileGroupModel
    {
        public string Extension { get; set; }
        public List<FileViewModel> Files { get; set; }
    }
}
