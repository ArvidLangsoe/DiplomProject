using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Storage.DTO.AddBatch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private StorageService _storageService;

        public BatchController(StorageService storageService)
        {
            _storageService = storageService;
        }


        [HttpPost("")]
        public IActionResult AddBatch(AddBatchDTO batchDTO) {
            _storageService.AddBatch(batchDTO);
            return Ok();
        }

    }
}