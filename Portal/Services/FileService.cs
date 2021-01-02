using Microsoft.AspNetCore.Http;
using Portal.Data;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class FileService : IFileService
    {
        private ApplicationDbContext _dbContext;
        public FileService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public byte[] AddFile(IFormFile addFile)
        {
            FileModel filemodel = new FileModel();
            byte[] result = null;

            if (addFile != null)
            {
                string csvname = Guid.NewGuid().ToString() + Path.GetExtension(addFile.FileName);

                using (var ms = new MemoryStream())
                {
                    addFile.CopyTo(ms);

                    var bytearray = ms.ToArray();

                    filemodel.Extension = addFile.FileName.Split(".")[1];
                    filemodel.Data = bytearray;
                    filemodel.FileName = addFile.FileName;
                    filemodel.Length = addFile.Length;

                    _dbContext.Add<FileModel>(filemodel);
                    result = bytearray;
                }
            }
            return result;
        }
    }
}
