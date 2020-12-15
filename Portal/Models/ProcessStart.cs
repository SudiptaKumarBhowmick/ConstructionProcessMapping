using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class ProcessStart
    {
        public Guid Record { get; set; }
        public string JobName { get; set; }
        public int StepNumber { get; set; }
        public string JobExecutor { get; set; }
        public string StepName { get; set; }
        public string Input { get; set; }
        public string PrimaryPrecedingJob { get; set; }
        public string SecondaryPrecedingJob1 { get; set; }
        public string SecondaryPrecedingJob2 { get; set; }
    }
}
