using Microsoft.AspNetCore.Mvc.Razor;
namespace WallOfKindness.Web.Infrastructure.ViewLocationExpanders
{
    public class CustomViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            // Add your custom paths here
            var customLocations = new List<string>
        {
            // Global shared views
            "/Views/Shared/{0}.cshtml",
            
            // Area shared views
            "/Areas/{2}/Views/Shared/{0}.cshtml",
            
            // Your custom paths
            "/Areas/Views/Shared/{0}.cshtml",  // New path you requested
            "/Areas/GeneralUser/Views/Shared/{0}.cshtml",

            "/Areas/Moderator/Views/Shared/{0}.cshtml",
            "/Areas/Moderator/Views/{1}/{0}.cshtml",
            
            // Default paths
            "/Areas/{2}/Views/{1}/{0}.cshtml",
            "/Areas/{2}/Views/Shared/{0}.cshtml",
            "/Views/{1}/{0}.cshtml",
            "/Views/Shared/{0}.cshtml",

            "Areas/{2}/Views/{1}/{0}.cshtml"
        };

            return customLocations.Union(viewLocations); // Combine with default locations
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // Not needed for this implementation
        }
    }
}
