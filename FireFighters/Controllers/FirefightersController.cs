using FireFighters.Models.DTOs;
using FireFighters.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace FireFighters.Controllers;

[Route("api/firefighters/action")]
[ApiController]
public class FirefightersController : ControllerBase
{
    private readonly IFirefighterRepository _firefighterRepository;

    public FirefightersController(IFirefighterRepository firefighterRepository)
    {
        _firefighterRepository = firefighterRepository;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetAction(int id)
    {
        try
        {
            ReturnedFireAction result = await _firefighterRepository.GetActionById(id);


            if (result == null)
            {
                return NotFound("Action not found!");
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = e.Message });
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAction(int id)
    {
        try
        {
            ReturnedFireAction result = await _firefighterRepository.GetActionById(id);
            if (result == null)
            {
                return NotFound("Action not found!");
            }

            bool succesfull = await _firefighterRepository.DeleteActionWithId(id);

            if (succesfull)
            {
                return Ok(result);
            }

            {
                return BadRequest("You can't delete action that ended");
            }


        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = e.Message });
        }
    }
}