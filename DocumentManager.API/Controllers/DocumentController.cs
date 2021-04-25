using DocumentManager.DTO;
using DocumentManager.DTO.Models;
using DocumentManager.Services.Container;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DocumentManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;

        private readonly IServiceContainer _serviceContainer;

        public DocumentController(ILogger<DocumentController> logger, IServiceContainer serviceContainer)
        {
            _logger = logger;
            _serviceContainer = serviceContainer;
        }

        [HttpGet("/Documents")]
        public IActionResult GetDocuments()
        {
            IEnumerable<DocumentModel> documents = _serviceContainer.DocumentService.GetDocuments();

            return Ok(documents);
        }

        [HttpPost("/Documents")]
        public IActionResult PostDocument(DocumentPostModel documentPostModel)
        {
            long documentId = _serviceContainer.DocumentService.AddDocument(documentPostModel);

            return Ok(documentId);
        }

        [HttpGet("/Document/{id}")]
        public IActionResult GetDocumentForUpdate(long id)
        {
            DocumentEditModel documentEditModel = _serviceContainer.DocumentService.GetDocumentForEdit(id);

            return Ok(documentEditModel);
        }

        [HttpPut("/Document")]
        public IActionResult UpdateDocument([FromBody]DocumentEditModel docEditModel)
        {
            DocumentEditResponseModel docEditResponseModel = _serviceContainer
                .DocumentService
                .UpdateDocument(docEditModel);

            return Ok(docEditResponseModel);
        }

        [HttpDelete("/Document/{id}")]
        public IActionResult DeleteDocument(long id)
        {
            bool deleted = _serviceContainer.DocumentService.DeleteDocument(id);

            return Ok(deleted);
        }
    }
}
