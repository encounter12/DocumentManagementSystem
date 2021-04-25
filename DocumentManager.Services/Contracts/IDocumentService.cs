using DocumentManager.DTO;
using DocumentManager.DTO.Models;
using System.Collections.Generic;

namespace DocumentManager.Services.Contracts
{
    public interface IDocumentService
    {
        IEnumerable<DocumentModel> GetDocuments();

        long AddDocument(DocumentPostModel documentModel);

        DocumentEditModel GetDocumentForEdit(long documentId);

        DocumentEditResponseModel UpdateDocument(DocumentEditModel documentEditModel);

        bool DeleteDocument(long documentId);
    }
}
