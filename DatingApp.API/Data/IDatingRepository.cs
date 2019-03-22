using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T: class;  //T:class means t type of class
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<User>> GetUsers();
        Task<User>GetUser(int id);

    }
}