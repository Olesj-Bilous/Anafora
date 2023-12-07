
using System.Globalization;

namespace AnaforaWeb.Routes
{
    public class RouteConstraint
    {
        protected string? GetRouteValue(string routeKey, RouteValueDictionary values)
        {
            if (values.TryGetValue(routeKey, out var routeValue))
            {
                return Convert.ToString(routeValue, CultureInfo.InvariantCulture);
            }
            return null;
        }
    }

    public class PageRouteConstraint : RouteConstraint, IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var value = GetRouteValue(routeKey, values);
            return !value?.StartsWith("api") ?? true;
        }
    }
}
