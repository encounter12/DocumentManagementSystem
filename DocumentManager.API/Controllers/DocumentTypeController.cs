using DocumentManager.DTO.Models;
using DocumentManager.Services.Container;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DocumentManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly ILogger<DocumentTypeController> _logger;

        private readonly IServiceContainer _serviceContainer;

        public DocumentTypeController(ILogger<DocumentTypeController> logger, IServiceContainer serviceContainer)
        {
            _logger = logger;
            _serviceContainer = serviceContainer;
        }

        [HttpGet("/DocumentTypes")]
        public IActionResult GetDocumentTypes()
        {
            IEnumerable<DocumentTypeModel> documentTypes = _serviceContainer.DocumentTypeService.GetDocumentTypes();

            return Ok(documentTypes);
        }
    }
}
