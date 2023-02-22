using Congratulations.Domain.Entity;
using Congratulations.Domain.Response;
using Congratulations.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations.Service.Interfaces
{
    public interface IUserService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        Task<IBaseResponse<UserViewModel>> GetUser(int id);
        Task<IBaseResponse<User>> CreateUser(UserViewModel model, byte[] imageData);
        Task<IBaseResponse<bool>> DeleteUser(int id);
        Task<IBaseResponse<User>> EditUser(int id, UserViewModel model);
        Task<BaseResponse<Dictionary<int, string>>> GetUser(string term);
    }
}
