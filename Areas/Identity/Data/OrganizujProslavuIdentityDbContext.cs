using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizujProslavu.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OrganizujProslavu.Areas.Identity.Data
{
    public class OrganizujProslavuIdentityDbContext : IdentityDbContext<WebApp1User>
    {
        public OrganizujProslavuIdentityDbContext(DbContextOptions<OrganizujProslavuIdentityDbContext> options)
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
        public DbSet<WebApp1User> Korisnici {get; set;}
        public DbSet<Oglas> Oglasi {get; set;}
        public DbSet<Rezervacija> Rezervacije {get; set;}
        public DbSet<Termin> Termini {get; set;}
        public DbSet<Karakteristike> Karakteristike {get; set;}
        public DbSet<ClanBenda> ClanoviBenda {get; set;}
        public DbSet<SlikaOglasa> SlikeOglasa {get;set;}


    }
}
