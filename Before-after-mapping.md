### Before mapping action

You can perform actions before mapping started by using `BeforeMapping` method.

```csharp
TypeAdapterConfig<Foo, Bar>.ForType()
    .BeforeMapping((src, dest) => dest.Initialize());
```

### After mapping action

You can perform actions after each mapping by using `AfterMapping` method. For instance, you might would like to validate object after each mapping.

```csharp
TypeAdapterConfig<Foo, Bar>.ForType()
    .AfterMapping((src, dest) => dest.Validate());
```

Or you can set for all mappings to types which implemented a specific interface by using `ForDestinationType` method.

```csharp
TypeAdapterConfig.GlobalSettings.ForDestinationType<IValidatable>()
    .AfterMapping(dest => dest.Validate());
```
### Before & after mapping in code generation

`BeforeMapping` and `AfterMapping` accept action which allowed you to pass multiple statements. In code generation, you might need to pass expression instead of action using `BeforeMappingInline` and `AfterMappingInline`, expression can be translated into code, but action cannot.

#### Single line statement

For single line statement, you can directly change from `BeforeMapping` and `AfterMapping` to `BeforeMappingInline` and `AfterMappingInline`.

```csharp
TypeAdapterConfig.GlobalSettings.ForDestinationType<IValidatable>()
    .AfterMappingInline(dest => dest.Validate());
```

#### Multiple statements

For multiple statements, you need to declare a method for actions.

```csharp
public static void Validate(Dto dto) {
    action1(dto);
    action2(dto);
    ...
}
```

Then you can reference the method to `BeforeMappingInline` and `AfterMappingInline`.

```csharp
TypeAdapterConfig.GlobalSettings.ForDestinationType<IValidatable>()
    .AfterMappingInline(dest => PocoToDtoMapper.Validate(dest));
```
