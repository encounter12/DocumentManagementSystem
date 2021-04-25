using System;

namespace DocumentManager.DTO.Models
{
    public class DocumentModel
    {
        public long DocumentID { get; set; }

        public string DocumentTypeCode { get; set; }

        public string FileName { get; set; }

        public Guid Guid { get; set; }
    }
}
