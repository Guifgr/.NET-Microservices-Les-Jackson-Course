using System;
using System.Collections.Generic;
using System.Linq;
using PlataformService.Data.Interface;
using PlataformService.Models;

namespace PlataformService.Data.Repository
{
  public class PlataformRepository : IPlataformRepository
  {
    private AppDbContext _context;

    public PlataformRepository(AppDbContext context)
    {
      _context = context;
    }
    public Plataform CreatePlataform(Plataform plataform)
    {
      if (plataform == null)
      {
        throw new ArgumentNullException(nameof(plataform));
      }

      _context.Plataforms.Add(plataform);
      SaveChanges();
      return plataform;
    }

    public IEnumerable<Plataform> GetAllPlataforms()
    {
      return _context.Plataforms.ToList();
    }

    public Plataform GetPlataformById(int id)
    {
      return _context.Plataforms.FirstOrDefault(p => p.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }
  }
}