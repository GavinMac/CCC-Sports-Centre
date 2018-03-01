using System;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(CCCSports.Web.App_Start.MySuperPackage), "PreStart")]

namespace CCCSports.Web.App_Start {
    public static class MySuperPackage {
        public static void PreStart() {
            MVCControlsToolkit.Core.Extensions.Register();
            System.Web.Mvc.GlobalFilters.Filters.Add(new MVCControlsToolkit.ActionFilters.PlaceJavascriptAttribute());
        }
    }
}