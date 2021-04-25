using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories.Contracts;
using DocumentManager.DTO.Models;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManager.DAL.Repositories
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        private readonly DocumentContext context;

        public DocumentTypeRepository(DocumentContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DocumentTypeModel> GetDocumentTypes()
        {
            var documentTypes = this
                .All()
                .Select(dt => new DocumentTypeModel
                {
                    Code = dt.Code,
                    Name = dt.Name
                })
                .ToList();

            return documentTypes;
        }
    }
}
