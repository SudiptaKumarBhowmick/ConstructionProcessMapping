using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class Job
    {
        public Guid Record { get; set; }

        public string JobName { get; set; }
        public string JobExecutor { get; set; }
        public string OrganisationType { get; set; }
        public string ContractingOrganisationType { get; set; }
        public List<OrderedStepNameAndStepNumber> OrderedStepNameAndStepNumber { get; set; }
        public List<CustomInputModel> OrderedCustomInputName { get; set; }
        public List<CustomOutputModel> OrderedCustomOutputName { get; set; }
        public List<OrderedGenericInputNameAndStepNumber> OrderedGenericInputNameAndStepNumber { get; set; }

        //Not sure if the two below can be dealt within the file controller, they might have to be dealt with when the files are compiled into the database
        public int LevelNumber { get; set; }
        public int JobNumberOnLevel { get; set; }

        //Don't think that any of the below should be required, they can be called on within the code as OrderedCustomInputName.Count for instance. Might be more efficient to have them as properties, not sure
        public int StepCount { get; set; }
        public int CustomInputCount { get; set; }
        public int CustomOutputCount { get; set; }
        public int GenericInputCount { get; set; }
        public Job(string organisationType, string contractingOrganisationType, int levelNumber, string jobName, int jobNumberOnLevel, string jobExecutor, int stepCount, int customInputCount, int customOutputCount)
        {
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            LevelNumber = levelNumber;
            JobName = jobName;
            JobNumberOnLevel = jobNumberOnLevel;
            JobExecutor = jobExecutor;
            StepCount = stepCount;
            CustomInputCount = customInputCount;
            CustomOutputCount = customOutputCount;
        }
        public Job(string jobName, string jobExecutor, string organisationType, string contractingOrganisationType, List<OrderedStepNameAndStepNumber> orderedStepNameAndStepNumber, List<CustomInputModel> orderedCustomInputName, List<CustomOutputModel> orderedCustomOutputName, List<OrderedGenericInputNameAndStepNumber> orderedGenericInputNameAndStepNumber)
        {
            JobName = jobName;
            JobExecutor = jobExecutor;
            OrganisationType = organisationType;
            //LevelNumber = ;
            //JobNumberOnLevel = ;
            ContractingOrganisationType = contractingOrganisationType;
            OrderedStepNameAndStepNumber = orderedStepNameAndStepNumber;
            OrderedCustomInputName = orderedCustomInputName;
            OrderedCustomOutputName = orderedCustomOutputName;
            OrderedGenericInputNameAndStepNumber = orderedGenericInputNameAndStepNumber;
            StepCount = orderedStepNameAndStepNumber.Count(); //is this right?
            CustomInputCount = orderedCustomInputName.Count(); //is this right?
            CustomOutputCount = orderedCustomOutputName.Count(); //is this right?
            GenericInputCount = orderedGenericInputNameAndStepNumber.Count(); //is this right?
        }
    }
}
