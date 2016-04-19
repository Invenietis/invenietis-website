using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Invenietis.LocalizedRoutes.Mvc
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Find the view corresponding to the current culture. 
        /// The pattern used to match the filename is "{action}.{culture}.cshtml".
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="model">The optional model to use in the view.</param>
        /// <returns>The localized view</returns>
        public static ViewResult LocalizedView( this Controller ctrl, object model = null )
        {
            var viewName = $"{ctrl.ActionContext.ActionDescriptor.Name}.{CultureInfo.CurrentCulture.Name}";
            return ctrl.View( viewName, model );
        }
    }
}
