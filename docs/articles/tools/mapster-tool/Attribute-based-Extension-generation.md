---
uid: Mapster.Tools.MapsterTool.AttributeBasedExtensionGeneration
title: Mapster Tool
---

## Generate extension methods via attributes

### Generate using `[GenerateMapper]` attribute

For any POCOs annotate with `[AdaptFrom]`, `[AdaptTo]`, or `[AdaptTwoWays]`, you can add `[GenerateMapper]` in order to generate extension methods.

Example:

```csharp
[AdaptTo("[name]Dto"), GenerateMapper]
public class Student {
    ...
}
```

Then Mapster will generate:

```csharp
public class StudentDto {
    ...
}
public static class StudentMapper {
    public static StudentDto AdaptToDto(this Student poco) { ... }
    public static StudentDto AdaptTo(this Student poco, StudentDto dto) { ... }
    public static Expression<Func<Student, StudentDto>> ProjectToDto => ...
}
```
