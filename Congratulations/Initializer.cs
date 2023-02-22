using Congratulations.DAL.Interfaces;
using Congratulations.DAL.Repositories;
using Congratulations.Domain.Entity;
using Congratulations.Service.Implementations;
using Congratulations.Service.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Congratulations
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
