using Microsoft.AspNetCore.Mvc;
using CallorieCounter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CallorieCounter;
namespace CalloriesCounter.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        var response = users.Select(user => new
        {
            user.Id,
            user.Name,
            user.Age,
            user.Weight,
            user.Height,
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetUser), new { id = user.Id }) },
                new { rel = "create-meal", href = Url.Action("CreateMeal", "Meals", new { userId = user.Id }) },
                new { rel = "edit", href = Url.Action(nameof(UpdateUser), new { id = user.Id }) },
                new { rel = "delete", href = Url.Action(nameof(DeleteUser), new { id = user.Id }) }
            }
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        var response = new
        {
            user.Id,
            user.Name,
            user.Age,
            user.Weight,
            user.Height,
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetUser), new { id = user.Id }) },
                new { rel = "create-meal", href = Url.Action("CreateMeal", "Meals", new { userId = user.Id }) },
                new { rel = "edit", href = Url.Action(nameof(UpdateUser), new { id = user.Id }) },
                new { rel = "delete", href = Url.Action(nameof(DeleteUser), new { id = user.Id }) }
            }
        };

        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User newUser)
    {
        var createdUser = _userService.CreateUser(newUser);

        var response = new
        {
            createdUser.Id,
            createdUser.Name,
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetUser), new { id = createdUser.Id }) },
                new { rel = "create-meal", href = Url.Action("CreateMeal", "Meals", new { userId = createdUser.Id }) }
            }
        };

        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
    {
        _userService.UpdateUser(id, updatedUser);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return NoContent();
    }
}
