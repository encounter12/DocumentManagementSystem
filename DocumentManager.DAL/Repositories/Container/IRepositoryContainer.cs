using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories.Contracts;

namespace DocumentManager.DAL.Repositories.Container
{
    public interface IRepositoryContainer
    {
        DocumentContext Context { get; }

        IDocumentRepository DocumentRepository { get; }

        IDocumentTypeRepository DocumentTypeRepository { get; }

        IFileTypeRepository FileTypeRepository { get; }

        void SaveChanges();
    }
}
