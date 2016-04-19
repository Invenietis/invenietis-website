Invenietis.LocalizedRoutes
===
Provides localized routes in different cultures from a specified configuration for ASP.NET Core.

## Introduction
The idea behind this package is to make the routes configuration more flexible, and culture-aware.
The routes are specified in an object implementing `ILocalizedRouteConfig`, that uses some conventions :
- Routes are composed of `RouteFragment`
- RouteFragments are defined invidually by cultures
- The `Id` of a route is composed in the form : `{part1}.{part2}.{partN}`
- The dots `"."` in the route `Id` indicates an inheritance hierarchy, or simply the concatenation of the route value preceding the dot
    - Given `"Home" = "home-page"` and `"Home.Page" = "a-page"`, the resulting value of `"Home.Page"` will be `"home-page/a-page"`
- A `RouteFragment` can be marked as `Abstract` : it will never be concretized as a route, but can be used to compose other routes.
- The resulting route has the form : `{culture}/{routeTemplate}`

## LocalizedRouteProvider
The `LocalizedRouteProvider` is a generic component. It has no dependency to Mvc. Its role is to generate the different
routes from the specified configuration, following cultures and inheritance hierarchy.

### Setup
 - To instantiate a `LocalizedRouteProvider`, you need to specify the configuration of the routes implementing the `ILocalizedRouteConfig` interface.
 You can store it in a JSON file, and unserialize it as a `LocalizedRouteConfig` object.
 
 Example of a JSON config file :
 ```json
 {
  "cultures": [
    {
      "culture": "fr",
      "defaultRouteId": "Home.Index",
      "routeFragments": [
        {
          "id": "Home",
          "value": "",
          "isAbstract": true
        },
        {
          "id": "Home.Index"
        },
        {
          "id": "Home.AboutUs",
          "value": "qui-sommes-nous"
        },   
        {
          "id": "Projects",
          "value": "projets",
          "isAbstract": true
        },
        {
          "id": "Projects.Index"
        },
        {
          "id": "Projects.GetProject",
          "value": "{id}-{name}"
        }
      ]
    },
    {
      "culture": "en",
      "defaultRouteId": "Home.Index",
      "routeFragments": [
        {
          "id": "Home",
          "value": "",
          "isAbstract": true
        },
        {
          "id": "Home.Index"
        },
        {
          "id": "Home.AboutUs",
          "value": "who-we-are"
        },
        {
          "id": "Projects",
          "value": "projects",
          "isAbstract": true
        },
        {
          "id": "Projects.Index"
        },
        {
          "id": "Projects.GetProject",
          "value": "{id}-{name}"
        }
      ]
    }
  ]
}
 ```
 
- Then, you need to call `SetupCultures()` on the instance with a `CultureConfig` argument. It should contain :
    - The default culture used by the website
    - The list of the supported cultures
    - A map, containing for each cultures, a list of fallback cultures.
    
- Finally, you can call `Build()` to generate the localized routes.

### Useful methods
- `string GetLocalizedLink( string routeId, string culture = null, Dictionary<string, string> routeParamsValues = null )` 
returns a localized link from the specified routeId.
-  `ILocalizedRoute GetLocalizedRoute( string routeId, string culture = null )`
returns the desired route.

> **Note:** These methods apply fallback following the `CultureConfig` fallback map if the route doesn't exist in the specified culture.

### TODO
```
/// <summary>
/// Add a new route in the specified culture. 
/// Triggers a <see cref="RouteAdded"/> event.
/// </summary>
/// <param name="culture">The culture of the route fragment</param>
/// <param name="routeFragment">The route fragment to add</param>
/// <returns>The resulting localized route, or null if the fragment is abstract</returns>
public ILocalizedRoute AddFragment( string culture, IRouteFragment routeFragment )
{
    // TODO
    throw new NotImplementedException();
}

/// <summary>
/// Remove the specified route.
/// Triggers a <see cref="RouteRemoved"/> event.
/// </summary>
/// <param name="route">The route to remove.</param>
public void RemoveRoute( ILocalizedRoute route )
{
    // TODO
    throw new NotImplementedException();
}

/// <summary>
/// Remove the specified route fragment.
/// </summary>
/// <param name="culture">The culture of the fragment</param>
/// <param name="fragmentId">The id of the fragment</param>
public void RemoveFragment( string culture, string fragmentId )
{
    // TODO
    throw new NotImplementedException();
}
```

## UrlCultureProvider
You can set the thread culture from the url content with the `UrlCultureProvider`.
For example, if the url starts with "/fr", the culture will be set to "fr".

If the culture is not supported, the fallback map will be used to choose the most appropriate culture.

### Setup
In the `Startup.cs`:

```
// Change culture if url starts with '/fr' or '/en', ...
private void ConfigureLocalization( IApplicationBuilder app, CultureConfig cultureConfig )
{
    RequestCulture defaultCulture = new RequestCulture( cultureConfig.DefaultCulture );

    RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions();
    localizationOptions.SupportedCultures = cultureConfig.SupportedCultures.Select( x => new CultureInfo( x ) ).ToList();

    localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>()
    {
            new UrlCultureProvider( cultureConfig )
    };

    app.UseRequestLocalization( localizationOptions, defaultCulture );
}
```

## LocalizedActionLinkTagHelper
A provided TagHelper `asp-culture` with `asp-route-id` and `asp-params` lets you generate localized links from the view to a specified route.

In your `_ViewImports.cshtml` :
```
@addTagHelper "*, Invenietis.LocalizedRoutes"
```

Sample:
```html
<a asp-route-id="Home.Index" asp-culture="en">Go to Index EN</a>
<a asp-route-id="Home.Index" asp-culture="fr">Go to Index FR</a>

<!-- If no culture is specified, the current culture is used -->
<a asp-route-id="Home.Index" asp-culture>Go to Index</a>

<!-- These routes requires arguments -->
<a asp-route-id="Home.GetArticle" asp-params="id: 120" asp-culture="fr">Go to Index FR</a>
<a asp-route-id="Home.GetArticles" asp-params="category: 20, token: aValidUriStringWithoutQuotes" asp-culture="fr">Go to Index FR</a>
```

> **Note:** A fallback is applied if the route doesn't exist in the specified culture.