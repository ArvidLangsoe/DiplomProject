using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands.AddProducts;
using Commands.DeleteProduct;
using Commands.UpdateProducts;
using Core.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ProductCatalog;
using Queries;
using Queries.Products;

namespace API.Controllers
{
    [Authorize("read:product")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        [Authorize("edit:product")]
        [HttpPost()]
        public IActionResult AddProduct([FromBody] AddProductDTO productDTO,[FromServices] AddProductCommand addProductCommand, [FromHeader] string TraceId) {

            addProductCommand.ProductDTO = productDTO;
            addProductCommand.Execute();

            if (!addProductCommand.IsSuccesful) {
                return BadRequest(addProductCommand.Errors);
            }


            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet()]
        public IActionResult QueryProducts([FromServices] QueryProducts productQuery, [FromQuery] string searchString) {

            Logger.Debug("Product Query: {Search} ",searchString);
            var searchParameters = new SearchParameters()
            {
                SearchString = searchString
            };

            CatalogPage<Product> products = productQuery.Query(searchParameters);

            if (!productQuery.IsSuccesful) {
                Logger.Warn("Product query failed, search string: {@searchstring} ", searchString);
                return BadRequest("Something went wrong.");
            }

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpPost("Specific")]
        public IActionResult QuerySpecificProducts([FromServices] QueryProducts productQuery, [FromBody] List<Guid> ids) {
            CatalogPage<Product> products = productQuery.Query(ids);

            if (!productQuery.IsSuccesful)
            {
                Logger.Warn("Specific product query failed, ids searched: {@ids} ", ids);
                return BadRequest(productQuery.Errors);
            }
            return Ok(products);
        }

        [Authorize("edit:product")]
        [HttpPatch("{productId}")]
        public IActionResult PatchProduct([FromRoute] Guid productId, [FromBody] UpdateProductDTO productChanges, [FromServices] UpdateProductCommand updateProductCommand) {
            if (productChanges.Id == null)
            {
                productChanges.Id = productId;
            }
            updateProductCommand.ProductUpdate = productChanges;
            updateProductCommand.Execute();

            if (!updateProductCommand.IsSuccesful)
            {
                return BadRequest(updateProductCommand.Errors);
            }
            return NoContent();
        }

        [Authorize("edit:product")]
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct([FromRoute] Guid productId, [FromServices] DeleteProductCommand deleteProductCommand) {
            deleteProductCommand.ProductId = productId;
            deleteProductCommand.Execute();
            if (!deleteProductCommand.IsSuccesful)
            {
                return BadRequest(deleteProductCommand.Errors);
            }

            return NoContent();

        }
    }
}