using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IFileService
    {
        byte[] AddFile(IFormFile addFile);
    }
}
