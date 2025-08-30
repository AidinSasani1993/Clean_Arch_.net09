using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Common.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static void AddRegisterRepository(this IServiceCollection services,Assembly assembly)
        {
            services.RegisterType(assembly, "Repository", ServiceLifetime.Scoped, "Singleton");
        }

        public static void AddRegisterService(this IServiceCollection services, Assembly assembly)
        {
            services.RegisterType(assembly, "Service", ServiceLifetime.Scoped, "Singleton");
        }

        public static void AddSingletons(this IServiceCollection services, Assembly assembly)
        {
            services.RegisterType(assembly, "SingletonService", ServiceLifetime.Singleton);
        }

        private static void RegisterType(this IServiceCollection services, Assembly assembly, string suffix, ServiceLifetime lifetime, string? exclude = null)
        {
            var types = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && type.GetInterfaces().Any(x => x.Name.EndsWith(suffix)))
                .Where(t => t.Name.EndsWith(suffix));

            if (!string.IsNullOrEmpty(exclude))
                types = types.Where(t => !t.Name.Contains(exclude));

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.Name.EndsWith(suffix))
                    .ToList();

                if (interfaces.Count != 1)
                {
                    throw new InvalidOperationException($"{suffix} '{type.Name}' must implement exactly one interface ending with '{suffix}'.");
                }

                var serviceDescriptor = new ServiceDescriptor(interfaces[0], type, lifetime);
                services.Add(serviceDescriptor);
            }
        }

    }
}
