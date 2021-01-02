using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IVisualService
    {
        Task<List<EntityNodeConfiguration>> GetNodePlottingGeometry(Guid recordID);
        Task<StraightConnectionLineViewModel> GetLinePlottingGeometry(Guid recordID);
    }
}
