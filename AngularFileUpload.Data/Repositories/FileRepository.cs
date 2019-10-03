using AngularFileUpload.Data.Models;
using AngularFileUpload.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularFileUpload.Data.Repositories
{
    public class FileRepository : EFRespository<File>, IFileRepository
    {
        public FileRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

    public interface IFileRepository : IRepository<File>
    {

    }
}
