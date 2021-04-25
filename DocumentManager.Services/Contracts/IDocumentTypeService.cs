using DocumentManager.DTO.Models;
using System.Collections.Generic;

namespace DocumentManager.Services.Contracts
{
    public interface IDocumentTypeService
    {
        IEnumerable<DocumentTypeModel> GetDocumentTypes();
    }
}
