using System;
using System.Collections.Generic;

namespace AngularFileUpload.Data.Models
{
    public partial class File
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }
        public string Extension { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
