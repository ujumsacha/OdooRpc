using Microsoft.AspNetCore.Mvc;
using PortaCapena.OdooJsonRpcClient.Models;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Converters;
using APIOdooWeb.Models;
using PortaCapena.OdooJsonRpcClient.Result;
using PortaCapena.OdooJsonRpcClient.Consts;

namespace APIOdooWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OdooController : ControllerBase
    {
        private readonly OdooConfig _config;
        private readonly OdooClient _client;
        public OdooController(OdooClient client)
        {
            _config = new OdooConfig(apiUrl: "https://odoo-api-url.com", dbName: "odoo-db-name", userName: "admin", password: "admin");
            _client = new OdooClient(_config);
        }

        [HttpGet(Name = "GetVersion")]
        public async Task<string> GetVersion()
        {
            var versionResult = await _client.GetVersionAsync();
            return versionResult.Jsonrpc;
        }


        [HttpGet(Name = "LoginAsync")]
        public async Task<string> LoginAsync()
        {
            var loginResult = await _client.LoginAsync();
            return loginResult.Jsonrpc;
        }

        
        [HttpGet(Name = "GetproductTable")]
        public async Task<string> GetproductTable()
        {
            var tableName = "product.product";
            var modelResult = await _client.GetModelAsync(tableName);

            var model = OdooModelMapper.GetDotNetModel(tableName, modelResult.Value);
            return model;
        }

        [HttpGet(Name = "Getproduct")]
        public async Task<OdooResult<OdooProductProduct[]>> Getproduct()
        {
            var repository = new OdooRepository<OdooProductProduct>(_config);
            var products = await repository.Query().ToListAsync();
            return products;
        }


        [HttpGet(Name = "GetOneproduct")]
        public async Task<OdooResult<OdooProductProduct[]>> GetOneproduct()
        {
            var repository = new OdooRepository<OdooProductProduct>(_config);
            var products = await repository.Query()
                .Where(x => x.DisplayName, OdooOperator.EqualsTo, "test product name")
                //.Where(x => x.WriteDate, OdooOperator.GreaterThanOrEqualTo, new DateTime(2020, 12, 2))
                .Select(x => new
                {
                    x.DisplayName,
                    x.Price,
                    x.Id
                })
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToListAsync();

            return products;
        }



    }
}