using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interface;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
  [Route("api/[controller]/")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    private IPlatformRepository _reposiory;
    private IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;

    public PlatformsController(IPlatformRepository reposiory, IMapper mapper, ICommandDataClient commandDataClient)
    {
      _reposiory = reposiory;
      _mapper = mapper;
      _commandDataClient = commandDataClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      Console.WriteLine("--> Getting Platforms...");
      var result = _reposiory.GetAllPlatforms();
      if (!result.Any())
      {
        return NotFound();
      }

      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(result));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById([FromRoute] int id)
    {
      Console.WriteLine("--> Getting Platform...");
      var result = _reposiory.GetPlatformById(id);
      if (result == default)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<PlatformReadDto>(result));
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto dto)
    {
      Console.WriteLine("--> Creating Platform...");
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var platform = _mapper.Map<PlatformReadDto>(_reposiory.CreatePlatform(_mapper.Map<Platform>(dto)));
      try
      {
        await _commandDataClient.SendPlatformToCommand(platform);
      }
      catch (Exception e)
      {
        Console.WriteLine($"--> could not sent data synchronously: {e.Message}");
      }
      return CreatedAtAction(nameof(GetPlatformById), new { Id = platform.Id }, platform);
    }
  }
}