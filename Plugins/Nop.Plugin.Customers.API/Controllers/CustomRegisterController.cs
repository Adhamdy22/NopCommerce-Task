using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Security;
using Nop.Plugin.Customers.API.Services;
using Nop.Services.Customers;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Controllers;

namespace Nop.Plugin.Customers.API.Controllers;
public class CustomRegisterController : Controller
{
    private readonly DynamicsCrmService _crmService;
    private readonly ICustomerService _customerService;

    public CustomRegisterController(
        ICustomerService customerService,
        DynamicsCrmService crmService)
    {
        _customerService = customerService;
        _crmService = crmService;
    }
    

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Register(CustomerModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Custom registration logic
        var existingContact = await _crmService.GetContactByEmailAsync(model.Email);
        if (existingContact != null)
        {
            ModelState.AddModelError("", "A contact with this email already exists.");
            return View(model);
        }

        // Registration logic here
        await _crmService.CreateContactAsync(model.FirstName, model.LastName, model.Email);

        return RedirectToAction("Success");
    }


}
