---
uid: Mapster.Packages.EntityFramework
title: "Packages - EF 6 and EF Core Support"
---

## [EntityFramework 6 support](#tab/ef6)

To install the package for EF 6:

```nuget
    PM> Install-Package Mapster.EF6
```

## [EntityFramework Core support](#tab/efcore)

To install the package for EF Core:

```nuget
    PM> Install-Package Mapster.EFCore
```

In EF, objects are tracked, when you copy data from dto to entity containing navigation properties, this package will help finding entity object in navigation properties automatically.

---

## Compatibility

- use Mapster.EFCore version 5.x for EFCore 5.x
- use Mapster.EFCore version 3.x for EFCore 3.x
- use Mapster.EFCore version 1.x for EFCore 2.x

## Usage

Use `EntityFromContext` method to define data context.

```csharp
var poco = db.DomainPoco.Include("Children")
    .Where(item => item.Id == dto.Id).FirstOrDefault();

dto.BuildAdapter()
    .EntityFromContext(db)
    .AdaptTo(poco);
```

Or like this, if you use mapper instance

```csharp
_mapper.From(dto)
    .EntityFromContext(db)
    .AdaptTo(poco);
```

### EF Core `ProjectToType`

`Mapster.EFCore` also allows you to perform projection from `IQueryable` source via mapper instance and `ProjectToType` directly to your DTO type.

```csharp
var query = db.Customers.Where(...);
_mapper.From(query)
    .ProjectToType<Dto>();
```
