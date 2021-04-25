using DocumentManager.Services.Contracts;

namespace DocumentManager.Services.Container
{
    public interface IServiceContainer
    {
        IDocumentService DocumentService { get; }

        IDocumentTypeService DocumentTypeService { get; }

        IFileTypeService FileTypeService { get; }
    }
}
