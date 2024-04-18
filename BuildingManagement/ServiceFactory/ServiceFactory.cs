using BusinessLogic.Logics;
using DataAccess;
using IDataAccess;
using LogicInterface.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceFactory
{
    public static class ServiceFactory
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAdminLogic, AdminLogic>();
            serviceCollection.AddScoped<IAdminRepository, AdminRepository>();
        }
    }
}
