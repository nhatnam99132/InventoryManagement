using System;
using InventoryManagement.Data;
//using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(InventoryManagement.Areas.Identity.IdentityHostingStartup))]
namespace InventoryManagement.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<InventoryManagementContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("InventoryManagementContext")));

                services.AddDefaultIdentity<Employee>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<InventoryManagementContext>();
            });
        }
    }
}