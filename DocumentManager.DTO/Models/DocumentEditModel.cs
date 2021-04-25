namespace DocumentManager.DTO.Models
{
    public class DocumentEditModel
    {
        public long DocumentID { get; set; }

        public string FileName { get; set; }

        public long DocumentTypeID { get; set; }

        public string Comment { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
