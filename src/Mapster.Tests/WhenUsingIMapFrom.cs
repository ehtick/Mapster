using System;
using System.Collections.Generic;
using System.Reflection;
using Mapster.Utils;
using MapsterMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Mapster.Tests;

[TestClass]
public class WhenUsingIMapFrom
{
    private readonly TypeAdapterConfig _config;
    private readonly Mapper _mapper;
    private readonly List<Type> _types;

    public WhenUsingIMapFrom()
    {
        _config = new TypeAdapterConfig
        {
            RequireExplicitMapping = true
        };
        _mapper = new Mapper(_config);
        _types = new List<Type>
        {
            typeof(SourceModel),
            typeof(InheritedDestinationModel),
            typeof(DestinationModel)
        };
    }

    [TestMethod]
    public void TestIMapFrom_WhenMethodIsNotImplemented()
    {
        var source = new SourceModel(DesireValues.Text);
        Should.Throw<InvalidOperationException>(() => _mapper.Map<DestinationModel>(source));
        _config.ScanInheritedTypes(_types);
        var destination = _mapper.Map<DestinationModel>(source);
        destination.Type.ShouldBe(DesireValues.Text);
    }

    [TestMethod]
    public void TestIMapFrom_WhenMethodImplemented()
    {
        var source = new SourceModel(DesireValues.Text);
        Should.Throw<InvalidOperationException>(() => _mapper.Map<InheritedDestinationModel>(source));
        _config.ScanInheritedTypes(_types);
        var destination = _mapper.Map<InheritedDestinationModel>(source);
        destination.Type.ShouldBe(DesireValues.Text);
        destination.Value.ShouldBe(9);
    }
}

internal static class DesireValues
{
    internal const string Text = "Test";
    internal const int Number = 9;
}

public class SourceModel
{
    public SourceModel(string type)
    {
        Type = type;
    }

    public string Type { get; set; }
}

public class InheritedDestinationModel : IMapFrom<SourceModel>
{
    public string Type { get; set; }
    public int Value { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<SourceModel, InheritedDestinationModel>()
            .Map(dest => dest.Value, _ => DesireValues.Number);
    }
}

public class DestinationModel : IMapFrom<SourceModel>
{
    public string Type { get; set; }
}
