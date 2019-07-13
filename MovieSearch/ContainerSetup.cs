using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieSearch.Data.DAL;
using MovieSearch.Queries;
using MovieSearch.Services;

namespace MovieSearch
{
    public static class ContainerSetup
    {
        public static void Setup(IServiceCollection services)
        {
            AddUnitOfWork(services);
            AddQueryProcessors(services);
        }

        private static void AddUnitOfWork(IServiceCollection services)
        {
            // We are using in memory database for this demo project.
            services.AddDbContext<MovieSearchDbContext>(builder => builder.UseInMemoryDatabase("InMemoryDb"));

            services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<MovieSearchDbContext>()));

            services.AddScoped<IUserAuthService, UserAuthService>();
        }

        private static void AddQueryProcessors(IServiceCollection services)
        {
            Type exampleProcessorType = typeof(UserQueryProcessor);

            Type[] types = (from t in exampleProcessorType.GetTypeInfo().Assembly.GetTypes()
                where t.Namespace == exampleProcessorType.Namespace
                      && t.GetTypeInfo().IsClass
                      && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
                select t).ToArray();

            foreach (Type type in types)
            {
                Type interfaceQ = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(interfaceQ, type);
            }
        }
    }
}
