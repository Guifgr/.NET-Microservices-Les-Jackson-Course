using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
      }
    }
    private static void SeedData(AppDbContext context)
    {
      if (!context.Platforms.Any())
      {
        Console.WriteLine("--> Seeding Data...");

        context.Platforms.AddRange(
          new Platform() { Name = "DotNet",Publisher = "Ms", Cost = "Free"},
          new Platform() { Name = "SqlServer",Publisher = "Ms", Cost = "Free"},
          new Platform() { Name = "Kubernetes",Publisher = "CNCF", Cost = "Free"}
        );
        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("--> We already have data");
      }
    }


  }
}