using System;
using OrganizujProslavu.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(OrganizujProslavu.Areas.Identity.IdentityHostingStartup))]
namespace OrganizujProslavu.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OrganizujProslavuIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OrganizujProslavuIdentityDbContextConnection")));

                services.AddDefaultIdentity<WebApp1User>(options => options.SignIn.RequireConfirmedEmail = false).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<OrganizujProslavuIdentityDbContext>();
            });
        }
    }
}