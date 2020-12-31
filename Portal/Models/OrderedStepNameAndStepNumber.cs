using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class OrderedStepNameAndStepNumber : BaseModel
    {
        public string key { get; set; }
        public int value { get; set; }
        public int StructuringInformationModelId { get; set; }
        public virtual StructuringInformationModel StructuringInformationModel { get; set; }
    }
}
