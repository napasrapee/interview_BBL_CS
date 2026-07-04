using interview.Models;

namespace interview.Interfaces;

public interface IUserRepository
{
    IEnumerable<UserModel> GetAll();
    UserModel? GetById(long id);
    void Add(UserModel user);
    int FindIndex(long id);
    void Update(int index, UserModel user);
    void Remove(UserModel user);
    long GetNextId();
}
