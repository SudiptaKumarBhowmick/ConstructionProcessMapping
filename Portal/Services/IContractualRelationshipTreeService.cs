using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public interface IContractualRelationshipTreeService
    {
        List<OrganisationTypeEntity> DefineContractualRelationshipHierarchy(List<Job> jobs);
    }
}
