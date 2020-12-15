using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class FileModel: BaseModel
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public Int64 Length { get; set; }
        public Byte[] Data { get; set; }
    }
}
