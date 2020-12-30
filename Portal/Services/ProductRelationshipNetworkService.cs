using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace Portal.Services
//{
//    public class ProductRelationshipNetworkService : IProductRelationshipNetworkService
//    {
//        public List<ProductTypeEntity> DefineProductRelationshipNetwork(List<Job> products)
//        {   //still need a way to define here a 2nd component of a placement code (something like an a, b, c... component for each level)
//            List<string> productType = GetListOfProductTypes(products);
//            List<ProductTypeEntity> productNetwork = new List<ProductTypeEntity>();
//            productNetwork.Add(new ProductTypeEntity ("ProjectRequirementBrief", 1, 1));
//            foreach (var job in products)
//            {
//                bool hasBeenFound = false;
//                for (int i = 0; i < productNetwork.Count; i++)
//                {
//                    if (job.OrganisationType.ToLowerInvariant() == productNetwork[i].Name.ToLowerInvariant())
//                    {
//                        productNetwork.Add(new ProductTypeEntity (job.CustomOutput, productNetwork[i].Level + 1, GetProductIdentifierID(job.CustomOutput, productType)));
//                        productNetwork.Add(new ProductTypeEntity (job.CustomInput, productNetwork[i].Level + 1, GetProductIdentifierID(job.CustomInput, productType)));
//                        hasBeenFound = true;
//                        break;
//                    }
//                }
//                if (!hasBeenFound && job.ContractingOrganisationType != "")
//                {
//                    productNetwork.Add(new ProductTypeEntity (job.CustomOutput, productNetwork[productNetwork.Count - 1].Level + 1, GetProductIdentifierID(job.CustomOutput, productType)));
//                    productNetwork.Add(new ProductTypeEntity (job.CustomInput, productNetwork[productNetwork.Count - 1].Level + 1, GetProductIdentifierID(job.CustomInput, productType)));
//                }
//            }
//            return productNetwork;
//        }

//        private int GetProductIdentifierID(string customOutput, List<string> customInput) //
//        {
//            for (int i = 0; i < customInput.Count; i++) //should the above be a string or a list of string?
//            {
//                if (customInput[i].ToLowerInvariant() == customOutput) //
//                {
//                    return i + 2;
//                }
//            };
//            return 0;
//        }

//        private List<string> GetListOfProductTypes(List<Job> jobs) //This method gives us an unsorted list of distinct product types
//        {
//            List<string> distinctProductTypes = new List<string>();
//            distinctProductTypes.AddRange(jobs.Select(x => x.CustomInput).Distinct().ToList());
//            distinctProductTypes.AddRange(jobs.Select(x => x.CustomOutput).Distinct().ToList());
//            return distinctProductTypes.Distinct().ToList();
//        }
//    }
//}
