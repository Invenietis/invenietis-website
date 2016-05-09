var CultureResolver = function (item, filter) {
    if (!item.Cultures) return;

    var defaultCulture = Invenietis.Cultures.DefaultCulture;
    var supportedCultures = Invenietis.Cultures.SupportedCultures;
    var fallbackMap = Invenietis.Cultures.FallbackMap;

    var orderedCultures = [defaultCulture].concat(fallbackMap[defaultCulture]);

    var chosen = _.find(orderedCultures, function (i) {
        return item.Cultures[i.Id] != null && filter(item.Cultures[i.Id]);
    });

    if (chosen != null) return item.Cultures[chosen.Id];
}


String.IsNullOrEmpty = function (str) {
    return str == null || str == '';
}

String.IsNotNullOrEmpty = function (str) {
    return !String.IsNullOrEmpty(str);
}