---
uid: Mapster.Configuration.Instance
title: "Configuration - Config Instance"
---

## Creating `Config` Instance with `TypeAdapterConfig`

You may wish to have different settings in different scenarios.
If you would not like to apply setting at a static level, Mapster also provides setting instance configurations.

```csharp
var config = new TypeAdapterConfig();
config.Default.Ignore("Id");
```

### `ForType` Configuration Methods

For instance configurations, you can use the same `NewConfig` and `ForType` methods that are used at the global level with
the same behavior: `NewConfig` drops any existing configuration and `ForType` creates or enhances a configuration.

```csharp
config.NewConfig<TSource, TDestination>()
        .Map(dest => dest.FullName,
            src => string.Format("{0} {1}", src.FirstName, src.LastName));

config.ForType<TSource, TDestination>()
        .Map(dest => dest.FullName,
            src => string.Format("{0} {1}", src.FirstName, src.LastName));
```

### `Adapt` Configuration Methods

You can apply a specific config instance by passing it to the `Adapt` method. (NOTE: please reuse your config instance to prevent recompilation)

```csharp
var result = src.Adapt<TDestination>(config);
```

### `Map` Configuration Methods

Or to a Mapper instance.

```csharp
var mapper = new Mapper(config);
var result = mapper.Map<TDestination>(src);
```

## Cloning `Config` Instance

If you would like to create configuration instance from existing configuration, you can use `Clone` method. For example, if you would like to clone from global setting.

```csharp
var newConfig = TypeAdapterConfig.GlobalSettings.Clone();
```

Or clone from existing configuration instance

```csharp
var newConfig = oldConfig.Clone();
```

### `Fork` Configuration

`Fork` is similar to `Clone`, but `Fork` will allow you to keep configuration and mapping in the same location. See [Config Location](xref:Mapster.Configuration.Location) for more info.

```csharp
var forked = mainConfig.Fork(config => 
    config.ForType<Poco, Dto>()
            .Map(dest => dest.code, src => src.Id));

var dto = poco.Adapt<Dto>(forked);
```
