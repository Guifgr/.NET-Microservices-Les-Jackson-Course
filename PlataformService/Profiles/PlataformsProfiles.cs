using AutoMapper;
using PlataformService.Dtos;
using PlataformService.Models;

namespace PlataformService.Profiles
{
  public class PlataformsProfiles : Profile
  {
    public PlataformsProfiles()
    {
      CreateMap<Plataform, PlataformReadDto>();
      CreateMap<PlataformCreateDto, Plataform>();
    }
  }
}