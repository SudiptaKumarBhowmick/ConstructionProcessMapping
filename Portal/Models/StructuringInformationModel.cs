using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class StructuringInformationModel : BaseModel
    {
        public Guid Record { get; set; }
        public string JobName { get; set; }
        public string JobExecutor { get; set; }
        public string OrganisationType { get; set; }
        public string ContractingOrganisationType { get; set; }
        public virtual List<CustomInputModel> CustomInputModels { get; set; }
        public virtual List<CustomOutputModel> CustomOutputModels { get; set; }
        public virtual List<OrderedStepNameAndStepNumber> OrderedStepNameAndStepNumber { get; set; }
        public virtual List<OrderedGenericInputNameAndStepNumber> OrderedGenericInputNameAndStepNumber { get; set; }
        public StructuringInformationModel()
        {
            Record = new Guid();
            CustomInputModels = new List<CustomInputModel>();
            CustomOutputModels = new List<CustomOutputModel>();
            OrderedStepNameAndStepNumber = new List<OrderedStepNameAndStepNumber>();
            OrderedGenericInputNameAndStepNumber = new List<OrderedGenericInputNameAndStepNumber>();
        }
    }
}
