using Microsoft.AspNetCore.Mvc;
using interview.Models;
using interview.Interfaces;
namespace interview.Controllers;

[ApiController]
[Route("users")]
[Produces("application/json")] 
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;
    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_repository.GetAll());
    }
    [HttpGet("{userId}")]
    public IActionResult GetUserById(long userId)
    {
        var user = _repository.GetById(userId);
        if (user == null)
        {
            return NotFound(new { error = "User not found" });
        }
        return Ok(user);
    }
    [HttpPost]
    public IActionResult CreateUser([FromBody] UserModel newUserInput)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // 🌟 เรียกใช้ฟังก์ชันผ่าน Repository แทนการจัดการ List ตรงๆ
        long newId = _repository.GetNextId();
        var userToSave = newUserInput with { Id = newId };
        _repository.Add(userToSave);

        return CreatedAtAction(nameof(GetUserById), new { userId = userToSave.Id }, userToSave);
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(long userId, [FromBody] UserModel updatedUserInput)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var index = _repository.FindIndex(userId);
        if (index == -1)
        {
            return NotFound(new { error = "User not found" });
        }

        var updatedUser = updatedUserInput with { Id = userId };
        _repository.Update(index, updatedUser);
        return Ok(updatedUser);
    }
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(long userId)
    {
        var user = _repository.GetById(userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }
        _repository.Remove(user);
        return NoContent(); 
    }
}
