using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlataformService.Models;

namespace PlataformService.Data
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
      if (!context.Plataforms.Any())
      {
        Console.WriteLine("--> Seeding Data...");

        context.Plataforms.AddRange(
          new Plataform() { Name = "DotNet",Publisher = "Ms", Cost = "Free"},
          new Plataform() { Name = "SqlServer",Publisher = "Ms", Cost = "Free"},
          new Plataform() { Name = "Kubernetes",Publisher = "CNCF", Cost = "Free"}
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