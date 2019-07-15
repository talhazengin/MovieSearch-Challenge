using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieSearch.Core.Services;
using MovieSearch.Data.DAL;
using MovieSearch.Data.QueryProcessors;
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
            services.AddDbContext<MovieSearchDbContext>(builder => builder.UseInMemoryDatabase("InMemoryDb"), ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserAuthService, UserAuthService>();
            services.AddTransient<IMovieSearchService, MovieSearchService>();
            services.AddSingleton<IMovieDbUpdateService, MovieDbUpdateService>();
        }

        private static void AddQueryProcessors(IServiceCollection services)
        {
            Type exampleProcessorType = typeof(UserQueryProcessor);

            Type[] processorTypes = (from t in exampleProcessorType.GetTypeInfo().Assembly.GetTypes()
                                     where t.Namespace == exampleProcessorType.Namespace && t.IsClass && !t.IsAbstract
                                           && t.GetCustomAttribute<CompilerGeneratedAttribute>() == null
                                     select t).ToArray();

            foreach (Type processorType in processorTypes)
            {
                Type interfaceQ = processorType.GetInterfaces().First();
                services.AddTransient(interfaceQ, processorType);
            }
        }
    }
}
