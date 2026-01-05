---
uid: Mapster.Packages.Immutable
title: "Packages - Immutable Support"
---

This Package enables Immutable collection support in Mapster.

### Installation

```nuget
    PM> Install-Package Mapster.Immutable
```

### Usage

Call `EnableImmutableMapping` from your `TypeAdapterConfig` to enable Immutable collection.

```csharp
TypeAdapterConfig.GlobalSettings.EnableImmutableMapping();
```

or:

```csharp
config.EnableImmutableMapping();
```

This will allow mapping to:

- `IImmutableDictionary<,>`
- `IImmutableList<>`
- `IImmutableQueue<>`
- `IImmutableSet<>`
- `IImmutableStack<>`
- `ImmutableArray<>`
- `ImmutableDictionary<,>`
- `ImmutableHashSet<>`
- `ImmutableList<>`
- `ImmutableQueue<>`
- `ImmutableSortedDictionary<,>`
- `ImmutableSortedSet<>`
- `ImmutableStack<>`
