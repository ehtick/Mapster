### EnableNonPublicMember

This will allow Mapster to set to all non-public members. You can turn on per type pair.

    TypeAdapterConfig<Poco, Dto>.NewConfig().EnableNonPublicMember(true);

Or global!

    TypeAdapterConfig.GlobalSettings.Default.EnableNonPublicMember(true);

### AdaptMember attribute

You can also map non-public members with `AdaptMember` attribute.

```
public class Product 
{
    [AdaptMember]
    private string HiddenId { get; set; }
    public string Name { get; set; }
}
```

### Map

`Map` command can map to private member by specify name of the members.

    TypeAdapterConfig<TSource, TDestination>
        .NewConfig()
        .Map("PrivateDestName", "PrivateSrcName");

### IncludeMember

With `IncludeMember`, you can select which access modifier to allow.

    TypeAdapterConfig.GlobalSettings.Default
        .IncludeMember((member, side) => member.AccessModifier == AccessModifier.Internal 
                                         || member.AccessModifier == AccessModifier.ProtectedInternal);

### Note for non-public member mapping

If type doesn't contain public properties, Mapster will treat type as primitive, you must also declare type pair to ensure Mapster will apply non-public member mapping.

    TypeAdapterConfig.GlobalSettings.Default.EnableNonPublicMember(true);
    TypeAdapterConfig<PrivatePoco, PrivateDto>.NewConfig();

