using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories.Contracts;

namespace DocumentManager.DAL.Repositories.Container
{
    public class RepositoryContainer : IRepositoryContainer
    {
        private readonly IDocumentRepository _documentRepository;

        private readonly IDocumentTypeRepository _documentTypeRepository;

        private readonly IFileTypeRepository _fileTypeRepository;

        public RepositoryContainer(
            DocumentContext context,
            IDocumentRepository documentRepository,
            IDocumentTypeRepository documentTypeRepository,
            IFileTypeRepository fileTypeRepository)
        {
            this.Context = context;
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
            _fileTypeRepository = fileTypeRepository;
        }

        public DocumentContext Context { get; }

        public IDocumentRepository DocumentRepository => _documentRepository;

        public IDocumentTypeRepository DocumentTypeRepository => _documentTypeRepository;

        public IFileTypeRepository FileTypeRepository => _fileTypeRepository;

        public void SaveChanges() => 
            this.Context.SaveChanges();
    }
}
