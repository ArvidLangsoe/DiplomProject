using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ProductStorageAPI.Controllers
{
    [Authorize("read:storage")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private StorageService _storageService;

        public ProductController(StorageService storageService) {
            _storageService = storageService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetProductAvailability([FromRoute] Guid id) {
            Logger.Debug("ProductAvailability searched for: {@Id} ", id);
            return Ok(_storageService.ProductCount(id));
        }

    }
}