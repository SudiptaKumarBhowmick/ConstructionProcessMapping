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

        public string MasterName { get; set; }
        public string JobName { get; set; }
        public string JobExecutor { get; set; }
        public string OrganisationType { get; set; }
        public string ContractingOrganisationType { get; set; }
        public int StepNumber { get; set; }
        public string DisplayedText { get; set; }


        public List<string> CustomInput { get; set; } //this should be an enum?
        public string StepName { get; set; } //this should be a list or maybe an enum of string?
        public int StepCount { get; set; } //not sure if this should be a property //not currently within the csv structure
        public int CustomInputCount { get; set; } //not sure if this should be a property
        public int CustomOutputCount { get; set; } //not sure if this should be a property
        public int LevelNumber { get; set; } //not sure if this should be a property
        public int JobNumberOnLevel { get; set; } //not sure if this should be a property
        public string GenericInputType { get; set; } //this should be a list, enum or dictionary with  below?
        public string GenericInputDescription { get; set; } //this should be a list, enum or dictionary with above?
        public List<string> CustomOutput { get; set; } //this should be an enum?

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
