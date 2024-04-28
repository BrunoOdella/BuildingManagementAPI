using BusinessLogic.Logics;
using DataAccess;
using IDataAccess;
using LogicInterface.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceFactory
{
    public static class ServiceFactory
    {
        public static void AddBusinessLogicServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAdminLogic, AdminLogic>();
            serviceCollection.AddScoped<ICategoriesRequestsLogic, CategoriesRequestsLogic>();
            serviceCollection.AddScoped<IInvitationLogic, InvitationLogic>();
            serviceCollection.AddScoped<IRequestLogic, RequestLogic>();
        }
        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BuildingManagementDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();

        }
    }
}
