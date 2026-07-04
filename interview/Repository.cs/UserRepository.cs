using System.Text.Json;
using interview.Interfaces;
using interview.Models;
namespace interview.Repository; 
public class UserRepository : IUserRepository
{
    private static readonly List<UserModel> _users;
    static UserRepository()
    {
        try
        {
            if (System.IO.File.Exists("users.json"))
            {
                var jsonText = System.IO.File.ReadAllText("users.json");
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                _users = JsonSerializer.Deserialize<List<UserModel>>(jsonText, options) ?? new List<UserModel>();
            }
            else { _users = new List<UserModel>(); }
        }
        catch { _users = new List<UserModel>(); }
    }
    public IEnumerable<UserModel> GetAll() => _users;
    public UserModel? GetById(long id) => _users.FirstOrDefault(u => u.Id == id);
    public void Add(UserModel user) => _users.Add(user);
    public int FindIndex(long id) => _users.FindIndex(u => u.Id == id);
    public void Update(int index, UserModel user) => _users[index] = user;
    public void Remove(UserModel user) => _users.Remove(user);
    public long GetNextId() => _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
}
