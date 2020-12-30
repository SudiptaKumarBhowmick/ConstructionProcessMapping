using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class StructuringInformationModel : BaseModel
    {
        public Guid Record { get; set; }
        public List<string> MasterName { get; set; }
        public string JobName { get; set; }
        public string JobExecutor { get; set; }
        public string OrganisationType { get; set; }
        public string ContractingOrganisationType { get; set; }
        public List<int> StepNumber { get; set; }
        public List<string> StepName { get; set; }
        public List<string> CustomInputName { get; set; }
        public List<string> CustomOutputName { get; set; }
        public List<string> GenericInputName { get; set; }

        public StructuringInformationModel()
        {
            Record = new Guid();
        }
    }
}
