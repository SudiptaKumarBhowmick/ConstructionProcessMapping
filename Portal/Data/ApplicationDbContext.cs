using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<StructuringInformationModel> Jobdata { get; set; }
        public DbSet<CustomInputModel> CustomInputs { get; set; }
        public DbSet<CustomOutputModel> CustomOutputs { get; set; }
        public DbSet<OrderedStepNameAndStepNumber> JobSteps { get; set; }
        public DbSet<OrderedGenericInputNameAndStepNumber> GenericInputs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }      
    }
}
