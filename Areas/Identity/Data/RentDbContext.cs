using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentApp.Areas.Identity.Data;
using RentApp.Models;

namespace RentApp.Data
{
    public class RentDbContext : IdentityDbContext<ApplicationUser>
    {
        public RentDbContext(DbContextOptions<RentDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<RentItems> RentItems { get; set; }
        public DbSet<TypeOfEquipment> TypeOfEquipments { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<ImageModal> ImageModals { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
