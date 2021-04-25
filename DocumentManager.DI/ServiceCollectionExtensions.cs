using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories;
using DocumentManager.DAL.Repositories.Container;
using DocumentManager.DAL.Repositories.Contracts;
using DocumentManager.DTO;
using DocumentManager.Infrastructure;
using DocumentManager.Infrastructure.Contracts;
using DocumentManager.Services;
using DocumentManager.Services.Container;
using DocumentManager.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentManager.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection SetDependencyInjectionBindings(
            this IServiceCollection services,
            AppData appData)
        {
            BindRepositories(services);
            BindServices(services);
            BindInfrastructureServices(services);
            BindDbContexts(services, appData);

            return services;
        }

        public static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IFileTypeRepository, FileTypeRepository>();
            services.AddScoped<IRepositoryContainer, RepositoryContainer>();
        }

        public static void BindServices(IServiceCollection services)
        {
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IFileTypeService, FileTypeService>();
            
            services.AddScoped<IServiceContainer, ServiceContainer>();
        }

        public static void BindInfrastructureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITenantService, TenantService>();

            services.AddScoped<INotification, Notification>();
        }

        public static void BindDbContexts(IServiceCollection services, AppData appData)
        {
            services.AddDbContext<DocumentContext>(options => options.UseSqlServer(appData.DocumentManagerConnectionString));
        }
    }
}
