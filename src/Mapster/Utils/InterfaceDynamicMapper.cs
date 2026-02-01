using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mapster.Utils;

public class InterfaceDynamicMapper
{
    private readonly TypeAdapterConfig _config;
    private readonly List<Type> _types;

    public InterfaceDynamicMapper(TypeAdapterConfig config, List<Type> types)
    {
        _config = config;
        _types = types;
    }

    internal void ApplyMappingFromAssembly()
    {
        foreach (var type in _types)
        {
            var method = GetMethod(type);
            if (method != null)
            {
                var instance = Activator.CreateInstance(type);
                method.Invoke(instance, new object[] { _config });
                continue;
            }

            var sourceType = type.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                .GetGenericArguments()[0];
            _config.NewConfig(sourceType, type);
        }
    }

    private static MethodInfo? GetMethod(Type type)
    {
        const string methodName = "ConfigureMapping";
        var method = type.GetMethod(methodName);
        if (method == null)
            return null;
        var parameters = method.GetParameters();
        var condition = parameters.Length == 1 && parameters[0].ParameterType == typeof(TypeAdapterConfig);
        if (!condition)
        {
            throw new Exception($"{methodName} is not implemented right or it's ambiguous!");
        }

        return method;
    }
}
