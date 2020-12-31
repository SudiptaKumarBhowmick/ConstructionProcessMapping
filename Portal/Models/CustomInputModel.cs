using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class CustomInputModel : BaseModel
    {
        public string InputName { get; set; }
        public int StructuringInformationModelId { get; set; }
        public virtual StructuringInformationModel StructuringInformationModel { get; set; }
    }
}
