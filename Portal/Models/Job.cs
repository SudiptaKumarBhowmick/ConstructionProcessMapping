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
        public List<string> OrderedCustomInputName1 { get; set; } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public List<string> OrderedCustomOutputName1 { get; set; } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public List<OrderedGenericInputNameAndStepNumber> OrderedGenericInputNameAndStepNumber { get; set; }

        //Not sure if the two below can be dealt within the file controller, they might have to be dealt with when the files are compiled into the database
        public int OrganisationLevelNumber { get; set; }
        public int JobNumberOnLevel { get; set; } //I don't think we need this - just order level number list accordingly

        //Don't think that any of the below should be required, they can be called on within the code as OrderedCustomInputName.Count for instance. Might be more efficient to have them as properties, not sure
        public int StepCount { get; set; }
        public int CustomInputCount { get; set; }
        public int CustomOutputCount { get; set; }
        public int GenericInputCount { get; set; }
        public Job(string organisationType, string contractingOrganisationType, int organisationLevelNumber, string jobName, int jobNumberOnLevel, string jobExecutor, int stepCount, int customInputCount, int customOutputCount)
        {
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            OrganisationLevelNumber = organisationLevelNumber;
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
            //OrganisationLevelNumber = ;
            //JobNumberOnLevel = ; //I don't think we need this - just order level number list accordingly
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
        public Job(string jobName, string jobExecutor, string organisationType, string contractingOrganisationType, List<string> orderedCustomInputName1, List<string> orderedCustomOutputName1)
        {
            JobName = jobName;
            JobExecutor = jobExecutor;
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            OrderedCustomInputName1 = orderedCustomInputName1;
            OrderedCustomOutputName1 = orderedCustomOutputName1;
        }
        public Job(string jobName, string jobExecutor, string organisationType, string contractingOrganisationType, List<string> orderedCustomInputName1, List<string> orderedCustomOutputName1, int organisationLevelNumber)
        {
            JobName = jobName;
            JobExecutor = jobExecutor;
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            OrderedCustomInputName1 = orderedCustomInputName1;
            OrderedCustomOutputName1 = orderedCustomOutputName1;
            OrganisationLevelNumber = organisationLevelNumber;
        }
        public Job(string jobName, string jobExecutor, string organisationType, string contractingOrganisationType, List<string> orderedCustomInputName1, List<string> orderedCustomOutputName1, int organisationLevelNumber, int jobNumberonLevel)
        {
            JobName = jobName;
            JobExecutor = jobExecutor;
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            OrderedCustomInputName1 = orderedCustomInputName1;
            OrderedCustomOutputName1 = orderedCustomOutputName1;
            OrganisationLevelNumber = organisationLevelNumber;
            JobNumberOnLevel = jobNumberonLevel;
        }
    }
}
