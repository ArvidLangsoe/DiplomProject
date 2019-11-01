using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands;
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
        public IActionResult AddProduct([FromBody] AddProductDTO productDTO,[FromServices] AddProductCommand addProductCommand) {
            AddTrace(addProductCommand);
            addProductCommand.ProductDTO = productDTO;
            addProductCommand.Execute();

            if (!addProductCommand.IsSuccesful) {
                Logger.Warn("Product addition failed, productDTO: {@productDTO} ", productDTO);
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
            Logger.Debug("Specific Product Query: {@Ids} ", ids);

            CatalogPage<Product> products = productQuery.Query(ids);
            if (!productQuery.IsSuccesful)
            {
                Logger.Warn("Specific product query failed, ids searched: {@ids} ", ids);
                return BadRequest(productQuery.Errors);
            }
            return Ok(products);
        }

        [Authorize("edit:product")]
        [HttpPatch("")]
        public IActionResult PatchProduct([FromRoute] Guid productId, [FromBody] UpdateProductDTO productChanges, [FromServices] UpdateProductCommand updateProductCommand) {
            AddTrace(updateProductCommand);
            updateProductCommand.ProductUpdate = productChanges;
            updateProductCommand.Execute();

            if (!updateProductCommand.IsSuccesful)
            {
                Logger.Warn("Product patch failed, attempted changes: {@productChanges} ", productChanges);
                return BadRequest(updateProductCommand.Errors);
            }
            return NoContent();
        }

        [Authorize("edit:product")]
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct([FromRoute] Guid productId, [FromServices] DeleteProductCommand deleteProductCommand) {
            AddTrace(deleteProductCommand);
            deleteProductCommand.ProductId = productId;
            deleteProductCommand.Execute();
            if (!deleteProductCommand.IsSuccesful)
            {
                Logger.Warn("Product deletion failed, productid: {@productId} ", productId);
                return BadRequest(deleteProductCommand.Errors);
            }

            return NoContent();

        }

        //TODO: Consider making this an extension method to command, with reverse method input.
        private void AddTrace(Command command) {
            command.TraceId = Request.Headers["TraceId"];
        }
    }
}