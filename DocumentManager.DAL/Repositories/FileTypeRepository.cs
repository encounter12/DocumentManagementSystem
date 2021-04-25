using DocumentManager.DAL.Model;
using DocumentManager.DAL.Repositories.Contracts;
using DocumentManager.DTO.Models;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManager.DAL.Repositories
{
    public class FileTypeRepository : GenericRepository<FileType>, IFileTypeRepository
    {
        private readonly DocumentContext context;

        public FileTypeRepository(DocumentContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<FileTypeModel> GetFileTypes()
        {
            var fileTypes = this
                .All()
                .Select(x => new FileTypeModel
                {
                    FileTypeCode = x.FileTypeCode,
                    Name = x.Name
                })
                .ToList();

            return fileTypes;
        }
    }
}
