using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MyWebApi.Models;

namespace MyWebApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private static readonly List<UserModel> _users;
    // get user จากไฟล์ json ที่ข้อมูลมาจาก https://jsonplaceholder.typicode.com/users
    static UsersController()
    {
        try
        {
            if (System.IO.File.Exists("users.json"))
            {
                var jsonText = System.IO.File.ReadAllText("users.json");
                _users = JsonSerializer.Deserialize<List<UserModel>>(jsonText) ?? new List<UserModel>();
            }
            else
            {
                _users = new List<UserModel>();
            }
        }
        catch
        {
            _users = new List<UserModel>();
        }
    }
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_users);
    }
    [HttpGet("{userId}")]
    public IActionResult GetUserById(long userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null) 
        {
            return NotFound(new { message = "User not found" });
        }
        return Ok(user);
    }
    [HttpPost]
    public IActionResult CreateUser([FromBody] UserModel newUserInput)
    {
        long newId = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        
        var userToSave = newUserInput with { Id = newId };
        
        _users.Add(userToSave);
        return CreatedAtAction(nameof(GetUserById), new { userId = userToSave.Id }, userToSave);
    }
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(long userId, [FromBody] UserModel updatedUserInput)
    {
        var index = _users.FindIndex(u => u.Id == userId);
        if (index == -1)
        {
            return NotFound(new { message = "User not found" });
        }
        var updatedUser = updatedUserInput with { Id = userId };
        _users[index] = updatedUser;

        return Ok(updatedUser);
    }
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(long userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        _users.Remove(user);
        return Ok(new { message = $"User with ID {userId} has been deleted." });
    }
}
