using Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class OrganisationTypeEntity //seems like this can go - along with contractual relationship service
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CompanyTypeIdentifier { get; set; }
        public OrganisationTypeEntity(string name, int level, int companyTypeIdentifier)
        {
            Name = name;
            Level = level;
            CompanyTypeIdentifier = companyTypeIdentifier;
        }
    }
}
