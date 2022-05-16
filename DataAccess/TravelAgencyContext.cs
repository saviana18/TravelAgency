using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TravelAgencyContext : IdentityDbContext<IdentityUser>
    {
        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options): base(options)
        {

        }
        public DbSet<CustomerEntity> CustomerEntities { get; set; }                                                                    
        public DbSet<BookingEntity> BookingEntities { get; set; }
        public DbSet<BillingEntity> BillingEntities { get; set; }
        public DbSet<OfferEntity> OfferEntities { get; set; }
        public DbSet<DestinationEntity> DestinationEntities { get; set; }
        public DbSet<ReviewEntity> ReviewEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.; Database=TravelAgency; Trusted_Connection=True;");
        }
    }
}
