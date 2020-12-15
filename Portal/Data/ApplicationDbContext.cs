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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }      
    }
}
