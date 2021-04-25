using DocumentManager.Services.Contracts;

namespace DocumentManager.Services.Container
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly IDocumentService documentService;

        private readonly IDocumentTypeService documentTypeService;

        private readonly IFileTypeService fileTypeService;

        public ServiceContainer(
            IDocumentService documentService,
            IDocumentTypeService documentTypeService,
            IFileTypeService fileTypeService)
        {
            this.documentService = documentService;
            this.documentTypeService = documentTypeService;
            this.fileTypeService = fileTypeService;
        }

        public IDocumentService DocumentService => this.documentService;

        public IDocumentTypeService DocumentTypeService => this.documentTypeService;

        public IFileTypeService FileTypeService => this.fileTypeService;
    }
}
