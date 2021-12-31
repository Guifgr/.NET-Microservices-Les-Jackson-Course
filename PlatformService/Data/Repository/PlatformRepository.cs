using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Data.Interface;
using PlatformService.Models;

namespace PlatformService.Data.Repository
{
  public class PlatformRepository : IPlatformRepository
  {
    private AppDbContext _context;

    public PlatformRepository(AppDbContext context)
    {
      _context = context;
    }
    public Platform CreatePlatform(Platform platform)
    {
      if (platform == null)
      {
        throw new ArgumentNullException(nameof(platform));
      }

      _context.Platforms.Add(platform);
      SaveChanges();
      return platform;
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
      return _context.Platforms.ToList();
    }

    public Platform GetPlatformById(int id)
    {
      return _context.Platforms.FirstOrDefault(p => p.Id == id);
    }

    public bool SaveChanges()
    {
      return (_context.SaveChanges() >= 0);
    }
  }
}