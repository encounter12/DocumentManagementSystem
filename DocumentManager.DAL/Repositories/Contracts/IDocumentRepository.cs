using DocumentManager.DAL.Model;
using DocumentManager.DTO.Models;
using System.Collections.Generic;

namespace DocumentManager.DAL.Repositories.Contracts
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        IEnumerable<DocumentModel> GetDocuments();

        DocumentEditModel GetDocumentForEdit(long documentId);

        void UpdateDocument(DocumentEditModel documentEditModel);

        void UpdateDocumentByAttach(DocumentEditModel documentEditModel);
    }
}
