using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFileUpload.ViewModels
{
    public class FileViewModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }
        public string Extension { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}
