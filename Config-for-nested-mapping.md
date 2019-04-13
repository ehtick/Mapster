Configuration is per type pair, not per type hierarchy. For example if you have parent and child classes.

```csharp
class ParentPoco
{
    public string Id { get; set; }
    public List<ChildPoco> Children { get; set; }
}
class ChildPoco
{
    public string Id { get; set; }
    public List<GrandChildPoco> GrandChildren { get; set; }
}
class GrandChildPoco
{
    public string Id { get; set; }
}
```

And if you have setting on parent type.

```csharp
TypeAdapterConfig<ParentPoco, ParentDto>.NewConfig()
    .PreserveReference(true);
```

When mapping, child type will not get effect from `PreserveReference`. 

To do so, you must specify all type pairs inside `ParentPoco`.

```csharp
TypeAdapterConfig<ParentPoco, ParentDto>.NewConfig()
    .PreserveReference(true);
TypeAdapterConfig<ChildPoco, ChildDto>.NewConfig()
    .PreserveReference(true);
TypeAdapterConfig<GrandChildPoco, GrandChildDto>.NewConfig()
    .PreserveReference(true);
```

Or you can set `PreserveReference` in global setting.

```csharp
TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
```

If you don't want to set config in global setting, you can also use `Fork`.

```csharp
var forked = TypeAdapterConfig.GlobalSettings.Fork(config => 
    config.Default.PreserveReference(true));
var parentDto = parentPoco.Adapt<ParentDto>(forked);
```
