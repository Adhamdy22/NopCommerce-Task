using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Customers.API.Infrastructure;
public class RouteProvider: IRouteProvider
{
    public void RegisterRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapControllerRoute(
            name: "CustomerApi",
            pattern: "api/customers/{action=Index}/{id?}",
            defaults: new { controller = "CustomerApi" });
    }

    public int Priority => 0; // Ensures this route is registered with default priority
}
