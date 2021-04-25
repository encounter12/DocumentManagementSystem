using DocumentManager.DTO.Models;
using DocumentManager.Services.Container;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DocumentManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileTypeController : ControllerBase
    {
        private readonly ILogger<FileTypeController> _logger;

        private readonly IServiceContainer _serviceContainer;

        public FileTypeController(ILogger<FileTypeController> logger, IServiceContainer serviceContainer)
        {
            _logger = logger;
            _serviceContainer = serviceContainer;
        }

        [HttpGet("/FileTypes")]
        public IActionResult GetFileTypes()
        {
            IEnumerable<FileTypeModel> fileTypes = _serviceContainer.FileTypeService.GetFileTypes();

            return Ok(fileTypes);
        }
    }
}
