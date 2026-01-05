---
uid: Mapster.Tools.MapsterTool.ConfigurationBasedCodeGeneration
title: Mapster Tool
---

## Configuration-based code generation

If you have configuration, it must be in `IRegister`

```csharp
public class MyRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TSource, TDestination>();
    }
}
```

### Advanced configuration

You can also generate extension methods and add extra settings from configuration.

```csharp
public class MyRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TSource, TDestination>()
            .GenerateMapper(MapType.Map | MapType.MapToTarget);
    }
}
```
