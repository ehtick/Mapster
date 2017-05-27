In EntityFramework, when we got data from dto, and we would like to map data to entities. We need to get objects from store first and then mapping data to it. Problem is, when the object contains complex graph, it is very difficult to map data correctly. Because some child property might reference to another object in store, and some children collection may or may not exist in store. This plugin will help you to copy data to object in store correctly. If data doesn't found, new object will be created.

    PM> Install-Package Mapster.EF6

Sample

```
var poco = dto.BuildAdapter()
              .CreateEntityFromContext(db)
              .AdaptToType<DomainPoco>();
```

NOTE: If your data contains complex graph, it would be better to dump related records in single command to store first. This is to avoid plugin from downloading records one-by-one, which should be slower.

```
//just make the query, objects will be cached in store
db.DomainPoco.Include("Children").Where(item => item.Id == dto.Id).FirstOrDefault();

//when mapping, Mapster will get object from cache rather than lookup in db
var poco = dto.BuildAdapter()
              .CreateEntityFromContext(db)
              .AdaptToType<DomainPoco>();
```
