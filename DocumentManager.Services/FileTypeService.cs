using DocumentManager.DAL.Repositories.Container;
using DocumentManager.DTO.Models;
using DocumentManager.Services.Contracts;
using System.Collections.Generic;

namespace DocumentManager.Services
{
    public class FileTypeService : IFileTypeService
    {

        private readonly IRepositoryContainer _repositoryContainer;

        public FileTypeService(IRepositoryContainer repositoryContainer)
        {
            _repositoryContainer = repositoryContainer;
        }

        public IEnumerable<FileTypeModel> GetFileTypes()
        {
            var fileTypes = _repositoryContainer.FileTypeRepository.GetFileTypes();

            return fileTypes;
        }
    }
}
