using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.PlaceOrder;
using Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [Authorize("edit:orders")]
        public IActionResult PlaceOrder([FromBody] PlaceOrderDTO order, [FromServices] PlaceOrderCommand placeOrderCommand) {

            placeOrderCommand.Order = order;
            placeOrderCommand.Execute();

            return NoContent();
        }


        [HttpGet]
        [Authorize("read:orders")]
        public IActionResult GetOrders( [FromServices] OrderQuery orderQuery) {
            orderQuery.Query();
            return Ok(orderQuery.Result);
        }

    }
}