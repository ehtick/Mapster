namespace Mapster;

public interface IMapFrom<TSource>
{
#if !NETSTANDARD2_0
    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig(typeof(TSource), GetType());
    }
#endif
}
