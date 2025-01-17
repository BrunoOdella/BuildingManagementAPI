﻿using BusinessLogic.Logics;
using DataAccess;
using IDataAccess;
using LogicInterface.Interfaces;
using LogicInterface.Interfaces.IManagerLogic;
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
            serviceCollection.AddScoped<IBuildingLogic, BuildingLogic>();
            serviceCollection.AddScoped<IMaintenanceStaffLogic, MaintenanceStaffLogic>();
            serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddScoped<IReportLogicByBuilding, ConcreteReportFactory_RequestByBuilding>();
            serviceCollection.AddScoped<IConstructionCompanyLogic, ConstructionCompanyLogic>();
            serviceCollection.AddScoped<IManagerLogic, ManagerLogic>();
            serviceCollection
                .AddScoped<IReportLogicByMaintenanceStaff, ConcreteReportFactory_RequestByMaintenanceStaff>();
            serviceCollection.AddScoped<IBuildingImportLogic, BuildingImportLogic>();
        }
        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BuildingManagementDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IMaintenanceStaffRepository, MaintenanceStaffRepository>();
            services.AddScoped<IConstructionCompanyAdminRepository, ConstructionCompanyAdminRepository>();
            services.AddScoped<IConstructionCompanyRepository, ConstructionCompanyRepository>();

        }
    }
}
