### Step-into debugging

    PM> Install-Package Mapster.Diagnostics

This plugin allow you to perform step-into debugging!

In order to step-into debugging, make sure you turn off just my code feature.
![turn off just my code](https://cloud.githubusercontent.com/assets/5763993/23740682/47608676-04d7-11e7-842d-77c18a459515.png)

##### Usage

Just declare `EnableDebugging`, this command is working only in debug mode. Therefore, you can keep this flag turn on. But do not forget to build your application in release mode for production, otherwise, there will be huge performance impact. This command should be called before you start mapping.

    TypeAdapterConfig.GlobalSettings.EnableDebugging();

And whenever you would like to step into mapping function, you need to mark `MapsterDebugger.BreakOnEnterAdaptMethod` to `true`. You can turn on or off any time by coding or Immediate window.

    MapsterDebugger.BreakOnEnterAdaptMethod = true;
    var dto = poco.Adapt<SimplePoco, SimpleDto>(); <--- After this line, you will step-into this function!!

![image](https://cloud.githubusercontent.com/assets/5763993/26521773/180427b6-431b-11e7-9188-10c01fa5ba5c.png)

##### Net Core support

Currently, step-into debugging is working only in .NET 4.x. .NET Core doesn't yet support.

##### Using internal classes or members

`private` and `protected` don't allow in debug mode. You can access to `internal`, but you have to add following attribute to your assembly.

    [assembly: InternalsVisibleTo("Mapster.Dynamic, PublicKey=0024000004800000940000000602000000240000525341310004000001000100ED6C7EE4B2E0AC62548FAA596F267469CD877990306F7C97ACBD889455E5D5846FFC5DCD5D87A72263DC7EB276629261C115DA3456485AE051D0C4EDBED3DD1A0092280F6408407D88FF3A76F2E6EECA4DE369E31A2A631052C99CAC2105B9A273969A66B076D5FC2B9B57F8E45D358906F58A0D959EA9BA2D725131A0E372AB")]

### Get mapping script

We can also see how Mapster generate mapping logic with `ToScript` method.

```
var script = poco.BuildAdapter()
                .CreateMapExpression<SimpleDto>()
                .ToScript();
```
