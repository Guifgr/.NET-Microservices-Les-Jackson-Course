using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlataformService.Data.Interface;
using PlataformService.Dtos;
using PlataformService.Models;

namespace PlataformService.Controllers
{
  [Route("api/[controller]/")]
  [ApiController]
  public class PlataformController : ControllerBase
  {
    private IPlataformRepository _reposiory;
    private IMapper _mapper;

    public PlataformController(IPlataformRepository reposiory, IMapper mapper)
    {
      _reposiory = reposiory;
      _mapper = mapper;
    }

    [HttpGet("[action]")]
    public ActionResult<IEnumerable<PlataformReadDto>> GetPlataforms()
    {
      Console.WriteLine("--> Getting Plataforms...");
      var result = _reposiory.GetAllPlataforms();
      if (!result.Any())
      {
        return NotFound();
      }
      return Ok(_mapper.Map<IEnumerable<PlataformReadDto>>(result));
    }

    [HttpGet("[action]/{id:int}", Name = "GetPlataformById")]
    public ActionResult<PlataformReadDto> GetPlataformById([FromRoute] int id)
    {
      Console.WriteLine("--> Getting Plataform...");
      var result = _reposiory.GetPlataformById(id);
      if (result == default)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<PlataformReadDto>(result));
    }

    [HttpPost("[action]")]
    public ActionResult<PlataformReadDto> CreatePlataform([FromBody] PlataformCreateDto dto)
    {
      Console.WriteLine("--> Creating Plataform...");
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var plataform = _reposiory.CreatePlataform(_mapper.Map<Plataform>(dto));
      return CreatedAtAction(nameof(GetPlataformById), new { Id = plataform.Id }, plataform);
    }

  }
}