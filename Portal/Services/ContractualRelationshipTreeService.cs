using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class ContractualRelationshipTreeService : IContractualRelationshipTreeService
    {
        public List<OrganisationTypeEntity> DefineContractualRelationshipHierarchy(List<Job> organisations)
        {   //still need a way to define here a 2nd component of a placement code (something like an a, b, c... component for each level)
            List<string> organisationType = GetListOfOrganisationTypes(organisations);
            List<OrganisationTypeEntity> contractTree = new List<OrganisationTypeEntity>();
            contractTree.Add(new OrganisationTypeEntity ("Owner", 1, 1));
            foreach (var job in organisations)
            {
                bool hasBeenFound = false;
                for (int i = 0; i < contractTree.Count; i++)
                {
                    if (job.OrganisationType.ToLowerInvariant() == contractTree[i].Name.ToLowerInvariant()) //what is the purpose of tolowerinvariant? feels like we need an i-1 or i+1
                    {
                        contractTree.Add(new OrganisationTypeEntity (job.ContractingOrganisationType, contractTree[i].Level + 1, GetCompanyIdentifierID(job.ContractingOrganisationType, organisationType)));
                        contractTree.Add(new OrganisationTypeEntity (job.OrganisationType, contractTree[i].Level + 1, GetCompanyIdentifierID(job.OrganisationType, organisationType)));
                        hasBeenFound = true;
                        break;
                    }
                }
                if (!hasBeenFound && job.ContractingOrganisationType != "")
                {
                    contractTree.Add(new OrganisationTypeEntity (job.ContractingOrganisationType, contractTree[contractTree.Count - 1].Level + 1, GetCompanyIdentifierID(job.ContractingOrganisationType, organisationType)));
                    contractTree.Add(new OrganisationTypeEntity (job.OrganisationType, contractTree[contractTree.Count - 1].Level + 1, GetCompanyIdentifierID(job.OrganisationType, organisationType)));
                }
            }
            return contractTree;
        }

        private int GetCompanyIdentifierID(string contractingOrganisationType, List<string> organisationType)
        {
            for (int i = 0; i < organisationType.Count; i++) //should the above be a string or a list of string???
            {
                if (organisationType[i].ToLowerInvariant() == contractingOrganisationType) //what is the purpose of tolowerinvariant here? feels like we need an i-1 or i+1
                {
                    return i + 2;
                }
            };
            return 0;
        }

        private List<string> GetListOfOrganisationTypes(List<Job> jobs) //This method gives us an unsorted list of distinct organisation types
        {
            List<string> distinctOrganisationTypes = new List<string>();
            distinctOrganisationTypes.AddRange(jobs.Select(x => x.OrganisationType).Distinct().ToList());
            distinctOrganisationTypes.AddRange(jobs.Select(x => x.ContractingOrganisationType).Distinct().ToList());
            return distinctOrganisationTypes.Distinct().ToList();
        }
    }
}
