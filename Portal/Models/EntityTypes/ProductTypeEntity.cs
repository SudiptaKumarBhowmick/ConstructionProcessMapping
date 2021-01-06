using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models
{
    public class ProductTypeEntity //seems like this can go
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ProductTypeIdentifier { get; set; }
        public ProductTypeEntity(string name, int level, int productTypeIdentifier)
        {
            Name = name;
            Level = level;
            ProductTypeIdentifier = productTypeIdentifier;
        }
    }
}
