using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class CustomOutputModel : BaseModel
    {
        public string OutputName { get; set; }
        public int StructuringInformationModelId { get; set; }
        public virtual StructuringInformationModel StructuringInformationModel { get; set; }
    }
}
