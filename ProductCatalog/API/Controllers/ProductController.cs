using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands.AddProducts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queries;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost()]
        public IActionResult AddProduct([FromBody] AddProductDTO productDTO,[FromServices] AddProductCommand addProductCommand) {
            addProductCommand.ProductDTO = productDTO;
            addProductCommand.Execute();

            if (!addProductCommand.IsSuccesful) {
                return BadRequest("Something went wrong, you probably sent a wrong product.");
            }
            return Ok();
        }


        [HttpGet()]
        public IActionResult QueryProducts([FromServices] QueryProducts productQuery) {

            List<ProductDTO> products = productQuery.Query();

            if (!productQuery.IsSuccesful) {
                return BadRequest("Something went wrong.");
            }

            return Ok(products);
        }
    }
}