### Implicit inheritance

Type mappings will automatically inherit for source types. Ie. if you set up following config.

    TypeAdapterConfig<SimplePoco, SimpleDto>.NewConfig()
        .Map(dest => dest.Name, src => src.Name + "_Suffix");

A derived type of `SimplePoco` will automatically apply the base mapping config.

    var dest = TypeAdapter.Adapt<DerivedPoco, SimpleDto>(src); //dest.Name = src.Name + "_Suffix"

If you don't wish a derived type to use the base mapping, you can turn off by `AllowImplicitSourceInheritance`

    TypeAdapterConfig.GlobalSettings.AllowImplicitSourceInheritance = true;

And by default, Mapster will not inherit destination type mappings. You can turn on by `AllowImplicitDestinationInheritance`.

    TypeAdapterConfig.GlobalSettings.AllowImplicitDestinationInheritance = true;

### Explicit inheritance

You can copy setting from based type explicitly.

    TypeAdapterConfig<DerivedPoco, DerivedDto>.NewConfig()
        .Inherits<SimplePoco, SimpleDto>();

### Include derived types

You can also include derived type to the based type declaration. For example:

    TypeAdapterConfig<Vehicle, VehicleDto>.NewConfig()
        .Include<Car, CarDto>();

    Vehicle vehicle = new Car { Id = 1, Name = "Car", Make = "Toyota" };
    var dto = vehicle.Adapt<Vehicle, VehicleDto>();

    dto.ShouldBeOfType<CarDto>();
    ((CarDto)dto).Make.ShouldBe("Toyota"); //Make property doesn't exist in Vehicle
