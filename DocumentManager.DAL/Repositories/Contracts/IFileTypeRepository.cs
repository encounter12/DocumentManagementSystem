using DocumentManager.DAL.Model;
using DocumentManager.DTO.Models;
using System.Collections.Generic;

namespace DocumentManager.DAL.Repositories.Contracts
{
    public interface IFileTypeRepository : IGenericRepository<FileType>
    {
        IEnumerable<FileTypeModel> GetFileTypes();
    }
}
