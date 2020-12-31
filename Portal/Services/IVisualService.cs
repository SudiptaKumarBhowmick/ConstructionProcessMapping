using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IVisualService
    {
        //List<EntityNodeConfiguration> GetNodePlottingGeometry(List<Job> customInputs); //dont think we need custom inputs here
        List<StraightConnectorLineConfiguration> GetNodePlottingGeometry(List<Job> customInputs);
    }
}
