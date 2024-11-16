using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Plugins;


namespace Nop.Plugin.Customers.RestApi;
public class CustomerApiPlugin:BasePlugin
{
    public override Task InstallAsync()
    {
        // Add custom logic here if needed
        return base.InstallAsync();
    }

    public override Task UninstallAsync()
    {
        // Add custom logic here if needed
       return base.UninstallAsync();
    }
}
