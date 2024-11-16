using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace Nop.Plugin.Customers.API.Services;
public class DynamicsCrmService
{
    private readonly string _url;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public DynamicsCrmService(string url, string clientId, string clientSecret)
    {
        _url = url;
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    private ServiceClient GetCrmServiceClient()
    {
        var connectionString = $"AuthType=ClientSecret;Url={_url};ClientId={_clientId};ClientSecret={_clientSecret};";
        return new ServiceClient(connectionString);
    }

    public async Task<Entity?> GetContactByEmailAsync(string email)
    {
        using var client = GetCrmServiceClient();
        var query = new QueryExpression("contact")
        {
            ColumnSet = new ColumnSet("contactid", "firstname", "lastname", "emailaddress1"),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression("emailaddress1", ConditionOperator.Equal, email) }
            }
        };

        var result = await Task.Run(() => client.RetrieveMultiple(query));
        return result.Entities.FirstOrDefault();
    }

    public async Task CreateContactAsync(string firstName, string lastName, string email)
    {
        using var client = GetCrmServiceClient();
        var contact = new Entity("contact")
        {
            ["firstname"] = firstName,
            ["lastname"] = lastName,
            ["emailaddress1"] = email
        };

        await Task.Run(() => client.Create(contact));
    }
}
