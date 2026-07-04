using Microsoft.AspNetCore.Mvc;
using interview.Controllers; 
using interview.Models;      
using Xunit;

namespace interview.Tests;
public class UsersControllerTests
{
    private readonly UsersController _controller;
    public UsersControllerTests()
    {
        _controller = new UsersController();
    }
    [Fact]
    public void GetAllUsers_ReturnsOkResult_WithListOfUsers()
    {
        var result = _controller.GetAllUsers();
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<IEnumerable<UserModel>>(okResult.Value);
    }
    [Fact]
    public void GetUserById_ReturnsOkResult_WhenUserExists()
    {
        var result = _controller.GetUserById(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var user = Assert.IsType<UserModel>(okResult.Value);
        Assert.Equal(1, user.Id);
    }
    [Fact]
    public void GetUserById_ReturnsNotFoundResult_WhenUserDoesNotExist()
    {
        var result = _controller.GetUserById(999);
        Assert.IsType<NotFoundObjectResult>(result);
    }
    [Fact]
    public void CreateUser_ReturnsCreatedAtActionResult_WhenModelIsValid()
    {
        var testUser = new UserModel(0, "Test Name", "testuser", "test@email.com", "123", "test.com");
        var result = _controller.CreateUser(testUser);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedUser = Assert.IsType<UserModel>(createdResult.Value);
        
        Assert.Equal("Test Name", returnedUser.Name);
        Assert.True(returnedUser.Id > 0);
    }
    [Fact]
    public void UpdateUser_ReturnsOkResult_WithUpdatedData()
    {
        var updatedData = new UserModel(1, "Updated Name", "updatedusername", "update@email.com", "999", "new.com");
        var result = _controller.UpdateUser(1, updatedData);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<UserModel>(okResult.Value);
        Assert.Equal("Updated Name", returnedUser.Name);
    }
    [Fact]
    public void DeleteUser_ReturnsNoContentResult_WhenDeleteIsSuccessful()
    {
        var result = _controller.DeleteUser(2);
        Assert.IsType<NoContentResult>(result);
    }
}
