using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories.Contracts;
using DocumentManager.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManager.DAL.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly DocumentContext context;

        public DocumentRepository(DocumentContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DocumentModel> GetDocuments()
        {
            var documents = this
                .All()
                .Select(x => new DocumentModel
                {
                    DocumentID = x.DocumentID,
                    DocumentTypeCode = x.DocumentType.Code,
                    FileName = x.FileName,
                    Guid = x.Guid
                })
                ;

            return documents;
        }

        public DocumentEditModel GetDocumentForEdit(long documentId)
        {
            DocumentEditModel documentEditModel = this
                .All()
                .Where(x => x.DocumentID == documentId)
                .Select(x => new DocumentEditModel
                {
                    DocumentID = x.DocumentID,
                    FileName = x.FileName,
                    DocumentTypeID = x.DocumentTypeID,
                    Comment = x.Comment,
                    RowVersion = EF.Property<byte[]>(x, "RowVersion")

                }).First();

            return documentEditModel;
        }

        public void UpdateDocument(DocumentEditModel documentEditModel)
        {
            Document document = this.GetById(documentEditModel.DocumentID);

            document.FileName = documentEditModel.FileName;
            document.DocumentTypeID = documentEditModel.DocumentTypeID;
            document.Comment = documentEditModel.Comment;

            this.Update(document);
        }

        public void UpdateDocumentByAttach(DocumentEditModel documentEditModel)
        {
            var document = new Document
            {
                DocumentID = documentEditModel.DocumentID,
                FileName = documentEditModel.FileName,
                DocumentTypeID = documentEditModel.DocumentTypeID,
                Comment = documentEditModel.Comment,
            };

            context.Attach(document);
            context.Entry(document).Property(p => p.FileName).IsModified = true;
            context.Entry(document).Property(p => p.DocumentTypeID).IsModified = true;
            context.Entry(document).Property(p => p.Comment).IsModified = true;
            context.Entry(document).Property("RowVersion").OriginalValue = documentEditModel.RowVersion;
        }
    }
}
