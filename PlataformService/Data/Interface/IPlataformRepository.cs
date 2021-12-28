using System.Collections.Generic;
using PlataformService.Models;

namespace PlataformService.Data.Interface
{
    public interface IPlataformRepository
    {
         bool SaveChanges();
         IEnumerable<Plataform> GetAllPlataforms();
         Plataform GetPlataformById(int id);
         Plataform CreatePlataform(Plataform plataform);
    }
}