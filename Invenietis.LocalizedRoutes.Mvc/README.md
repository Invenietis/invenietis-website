Invenietis.LocalizedRoutes.Mvc
===
This package adds features to map the generated routes by the `LocalizedRoutesProvider` to an MVC environment.
It adds some limitations :
- A concrete route `Id` must be in the form `{controllerName}.{actionName}` 

## LocalizedRoutesMvcAdapter
For MVC projects, you must create an instance of `LocalizedRoutesMvcAdapter`.
It will map the generated routes by a `LocalizedRoutesProvider` to the corresponding controllers and actions.

Its constructor takes 2 parameters :
- An instance of a `LocalizedRoutesProvider`
- The `Assembly` containing the target controllers

> **Note:** On *dnxcore*, the assembly can be obtained like this : `typeof(Startup).GetTypeInfo().Assembly`

Then, you can call `MapLocalizedRoutes()`. It takes an `IRouteBuilder` as parameter and will add the localized routes to it.

For example, in the `Startup.cs`: :

```
public void Configure( IApplicationBuilder app, LocalizedRouteProvider routeProvider, LocalizedRoutesMvcAdapter routeMvcAdapter )
{
    var cultureConfig = Configuration.Get<CultureConfig>();

    // Build localized routes
    routeProvider.SetupCultures( cultureConfig );
    routeProvider.Build();

    app.UseMvc( routes =>
    {
        // Map the localized routes for MVC controllers/actions
        routeMvcAdapter.MapLocalizedRoutes( routes );
    } );
}
```

> **Note:** When you call `MapLocalizedRoutes()`, the instance of `LocalizedRoutesProvider` must be setup and built.

### LocalizationController
If you define a default route in the configuration, you can route the user to its corresponding culture.

In the `Startup.cs`:
```
public void Configure( IApplicationBuilder app, LocalizedRouteProvider routeProvider, LocalizedRoutesMvcAdapter routeMvcAdapter )
{
    ...
    
    app.UseMvc( routes =>
    {
        // Map the localized routes for MVC controllers/actions
        routeMvcAdapter.MapLocalizedRoutes( routes );

        // Default route to choose the right localization
        routes.MapRoute( "Default", "", new { controller = "Localization", action = "Index" } );
    } );
}
```

Entry point controller:

```
public class LocalizationController : Controller
{
    LocalizedRouteProvider _routeProvider;

    public LocalizationController( LocalizedRouteProvider routeProvider )
    {
        _routeProvider = routeProvider;
    }

    public IActionResult Index()
    {
        var culture = CultureInfo.CurrentCulture.Name;

        var routeName = _routeProvider.GetLocalizedRoute(x => x.IsDefault, culture).Name;

        return RedirectToRoute( routeName );
    }
}
```

### TODO
- `LocalizedRoutesProvider` emits events when a route is added or removed. 
`LocalizedRoutesMvcAdapter` should handle them by mapping/unmapping the route.