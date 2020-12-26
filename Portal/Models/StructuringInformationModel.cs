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
        public string CustomInput { get; set; }
        public int StepNumber { get; set; }
        public string StepName { get; set; }
        public string GenericInputType { get; set; }
        public string GenericInputDescription { get; set; }
        public string CustomOutput { get; set; }

        public StructuringInformationModel()
        {
            Record = new Guid();
        }
    }
}
