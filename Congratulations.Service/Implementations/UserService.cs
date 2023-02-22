using Congratulations.DAL.Interfaces;
using Congratulations.Domain.Entity;
using Congratulations.Domain.Enum;
using Congratulations.Domain.Extensions;
using Congratulations.Domain.Response;
using Congratulations.Domain.ViewModels;
using Congratulations.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> CreateUser(UserViewModel model, byte[] imageData)
        {
            try
            {
                var user = new User()
                {
                    Name = model.Name,
                    DateBirthDay = DateTime.Now,
                    TypeUser = Enum.Parse<TypeUser>(model.TypeUser),
                    Avatar = imageData
                };
                await _userRepository.Create(user);

                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.OK,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _userRepository.Delete(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> EditUser(int id, UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                user.Name = user.Name;
                user.TypeUser = user.TypeUser;
                user.DateBirthDay= user.DateBirthDay;

                await _userRepository.Update(user);


                return new BaseResponse<User>()
                {
                    Data = user,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<BaseResponse<Dictionary<int, string>>> GetUser(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        DateBirthDay = x.DateBirthDay.ToLongDateString(),
                        TypeUser = x.TypeUser.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = users;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((TypeUser[])Enum.GetValues(typeof(TypeUser)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<UserViewModel>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<UserViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new UserViewModel()
                {
                    Name = user.Name,
                    DateBirthDay = user.DateBirthDay.ToShortDateString(),
                    TypeUser = user.TypeUser.GetDisplayName(),
                    Image = user.Avatar,
                };

                return new BaseResponse<UserViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserViewModel>()
                {
                    Description = $"[GetUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        DateBirthDay = x.DateBirthDay.ToShortDateString(),
                        TypeUser = x.TypeUser.GetDisplayName()
                    })
                    .ToListAsync();
       
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {              
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
