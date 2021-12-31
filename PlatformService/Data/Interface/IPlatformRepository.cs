using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data.Interface
{
    public interface IPlatformRepository
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        Platform CreatePlatform(Platform platform);
    }
}