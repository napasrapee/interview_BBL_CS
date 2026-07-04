using Microsoft.AspNetCore.Mvc;
using interview.Controllers; 
using interview.Models;      
using interview.Interfaces; 
using Moq; 
using Xunit;

namespace interview.Tests;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _mockRepo;

    public UsersControllerTests()
    {
        _mockRepo = new Mock<IUserRepository>();
    }
    [Fact]
    public void GetAllUsers_ReturnsOkResult_WithListOfUsers()
    {
        var fakeUsers = new List<UserModel>
        {
            new UserModel(1, "Mock Graham", "Bret", "test@test.com", "123", "test.org")
        };
        _mockRepo.Setup(repo => repo.GetAll()).Returns(fakeUsers);
        var controller = new UsersController(_mockRepo.Object);
        var result = controller.GetAllUsers();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<UserModel>>(okResult.Value);
        Assert.Single(returnedUsers);
    }
    [Fact]
    public void GetUserById_ReturnsOkResult_WhenUserExists()
    {
        var fakeUser = new UserModel(1, "Mock Graham", "Bret", "test@test.com", "123", "test.org");
        _mockRepo.Setup(repo => repo.GetById(1)).Returns(fakeUser);
        var controller = new UsersController(_mockRepo.Object);
        var result = controller.GetUserById(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var user = Assert.IsType<UserModel>(okResult.Value);
        Assert.Equal(1, user.Id);
    }
    [Fact]
    public void GetUserById_ReturnsNotFoundResult_WhenUserDoesNotExist()
    {
        _mockRepo.Setup(repo => repo.GetById(999)).Returns((UserModel?)null);
        var controller = new UsersController(_mockRepo.Object);
        var result = controller.GetUserById(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    [Fact]
    public void CreateUser_ReturnsCreatedAtActionResult_WhenModelIsValid()
    {
        _mockRepo.Setup(repo => repo.GetNextId()).Returns(5);
        var controller = new UsersController(_mockRepo.Object);
        var testUser = new UserModel(0, "New User", "new", "new@email.com", "123", "new.com");
        var result = controller.CreateUser(testUser);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedUser = Assert.IsType<UserModel>(createdResult.Value);
        
        Assert.Equal(5, returnedUser.Id);
        _mockRepo.Verify(repo => repo.Add(It.IsAny<UserModel>()), Times.Once); 
    }
    [Fact]
    public void UpdateUser_ReturnsOkResult_WithUpdatedData()
    {
        _mockRepo.Setup(repo => repo.FindIndex(1)).Returns(0);
        var controller = new UsersController(_mockRepo.Object);
        var updatedData = new UserModel(1, "Updated Name", "updatedusername", "update@email.com", "999", "new.com");
        var result = _controller.UpdateUser(1, updatedData);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedUser = Assert.IsType<UserModel>(okResult.Value);
        Assert.Equal("Updated Name", returnedUser.Name);
        _mockRepo.Verify(repo => repo.Update(0, It.IsAny<UserModel>()), Times.Once);
    }
    [Fact]
    public void DeleteUser_ReturnsNoContentResult_WhenDeleteIsSuccessful()
    {
        var fakeUser = new UserModel(2, "Ervin Howell", "Antonette", "shanna@email.com", "123", "site.net");
        _mockRepo.Setup(repo => repo.GetById(2)).Returns(fakeUser); 
        var controller = new UsersController(_mockRepo.Object);
        var result = controller.DeleteUser(2);
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(repo => repo.Remove(fakeUser), Times.Once); 
    }
}
