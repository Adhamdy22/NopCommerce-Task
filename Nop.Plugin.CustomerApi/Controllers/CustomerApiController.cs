using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Customers.RestApi.Controllers;


[Route("api/customers")]
[ApiController]
public class CustomerApiController : BasePluginController
{
    private readonly ICustomerService _customerService;
    public CustomerApiController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        var data = customers.Select(c => new
        {
            c.Id,
            c.Email,
            c.Username,
            c.Active,
            c.CreatedOnUtc
        }).ToList();
        return new JsonResult(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        return new JsonResult(new
        {
            customer.Id,
            customer.Email,
            customer.Username,
            customer.Active,
            customer.CreatedOnUtc
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] Customer model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _customerService.InsertCustomerAsync(model);
        return CreatedAtAction(nameof(GetCustomerById), new { id = model.Id }, model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        await _customerService.DeleteCustomerAsync(customer);
        return NoContent();
    }


    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer model)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        customer.Email = model.Email;
        customer.Username = model.Username;
        customer.Active = model.Active;
        await _customerService.UpdateCustomerAsync(customer);

        return NoContent();
    }
}

