namespace Lib.Mapping
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;

    public abstract class MappingProfileBase : Profile
    {
        public MappingProfileBase()
        {
            ApplyMappingsFromAssembly(GetType().Assembly);
            // ApplyMappingsFromAssembly(GetType().Assembly);
            // ApplyMappingsFromAssembly(typeof(MappingProfileBase).Assembly);
    
            Console.WriteLine(Assembly.GetEntryAssembly().FullName);
            Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
            Console.WriteLine(Assembly.GetCallingAssembly().FullName);
            Console.WriteLine(GetType().Assembly.FullName);
            Console.WriteLine(typeof(MappingProfileBase).Assembly.FullName);
        }
    
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(
                    t => t.GetInterfaces()
                        .Any(
                            i =>
                                i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>)
                                                    || i.GetGenericTypeDefinition() == typeof(IMapTo<>))))
                .ToList();
    
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
    
                var methodInfo = type.GetMethod("Mapping", BindingFlags.Instance | BindingFlags.NonPublic)
                                 ?? (type.GetInterfaces()
                                     .Any(
                                         i => i.IsGenericType &&
                                              i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                                     ? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping")
                                     : type.GetInterface("IMapTo`1")?.GetMethod("Mapping"));
    
                methodInfo?.Invoke(instance, new object?[] {this});
            }
        }
    }
}