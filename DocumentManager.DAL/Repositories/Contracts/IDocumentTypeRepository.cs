using DocumentManager.DAL.Model;
using DocumentManager.DTO.Models;
using System.Collections.Generic;

namespace DocumentManager.DAL.Repositories.Contracts
{
    public interface IDocumentTypeRepository : IGenericRepository<DocumentType>
    {
        IEnumerable<DocumentTypeModel> GetDocumentTypes();
    }
}
