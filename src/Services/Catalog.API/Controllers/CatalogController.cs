using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IServiceProvider provider)
        {
            _productRepository = provider.GetRequiredService<IProductRepository>();
        }

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productRepository.GetProducts();
            return Ok(response);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            var response = await _productRepository.GetProductByCategory(category);
            return Ok(response);
        }

        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetProduct(string Id)
        {
            var response = await _productRepository.GetProductById(Id);
            return Ok(response);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProductById", new { id = product.Id });
        }

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            var response = await _productRepository.UpdateProduct(product);
            return Ok(response);
        }

        [HttpDelete(Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string Id)
        {
            var response = await _productRepository.DeleteProduct(Id);
            return Ok(response);
        }
    }
}
