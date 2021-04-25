using DocumentManager.DAL.Repositories.Container;
using DocumentManager.DTO.Models;
using DocumentManager.Services.Contracts;
using System.Collections.Generic;

namespace DocumentManager.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IRepositoryContainer _repositoryContainer;

        public DocumentTypeService(IRepositoryContainer repositoryContainer)
        {
            _repositoryContainer = repositoryContainer;
        }

        public IEnumerable<DocumentTypeModel> GetDocumentTypes()
        {
            var documentTypes = _repositoryContainer.DocumentTypeRepository.GetDocumentTypes();

            return documentTypes;
        }
    }
}
