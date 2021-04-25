using DocumentManager.DTO.Models;
using System.Collections.Generic;

namespace DocumentManager.Services.Contracts
{
    public interface IFileTypeService
    {
        IEnumerable<FileTypeModel> GetFileTypes();
    }
}
