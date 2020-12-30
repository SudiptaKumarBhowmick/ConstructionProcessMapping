using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    //objects: map, job, process
    public class Job
    {
        public Guid Record { get; set; }

        public string MasterName { get; set; } // don't need this here
        public string JobName { get; set; }
        public string JobExecutor { get; set; }
        public string OrganisationType { get; set; }
        public string ContractingOrganisationType { get; set; }
        public List<int> StepNumbers { get; set; }
        public List<string> StepNames { get; set; }
        public List<string> CustomInputs { get; set; }
        public List<string> CustomOutputs { get; set; }
        public List<string> GenericInputDescriptions { get; set; }
        public List<string> GenericInputTypes { get; set; }
        public string DisplayedText { get; set; } // don't need this here


        public int StepCount { get; set; } //should not need this property, it's just lisofsteps.count
        public int LevelNumber { get; set; }
        public int JobNumberOnLevel { get; set; }
        public int CustomInputCount { get; set; } //should not need this property, it's just lisofinputs.count
        public int CustomOutputCount { get; set; } //should not need this property, it's just lisofoutputs.count
        public int GenericInputCount { get; set; } //should not need this property, it's just lisofsteps.count

        public Job(string organisationType, string contractingOrganisationType, int levelNumber, string jobName, int jobNumberOnLevel, string jobExecutor, int stepCount, int customInputCount, int customOutputCount)
        {
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            LevelNumber = levelNumber;
            JobName = jobName;
            JobNumberOnLevel = jobNumberOnLevel;
            JobExecutor = jobExecutor;
            StepCount = stepCount; //should not need this property, it's just lisofsteps.count
            CustomInputCount = customInputCount; //should not need this property, it's just lisofinputs.count
            CustomOutputCount = customOutputCount; //should not need this property, it's just lisofoutputs.count
        }

        public Job(string organisationType, string contractingOrganisationType, string jobName, string jobExecutor)
        {
            OrganisationType = organisationType;
            ContractingOrganisationType = contractingOrganisationType;
            JobName = jobName;
            JobExecutor = jobExecutor;
        }
        public Job(string organisationType)
        {
            OrganisationType = organisationType;
        }

        public Job(){}
    }
}
