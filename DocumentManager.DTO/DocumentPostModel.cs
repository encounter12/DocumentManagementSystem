namespace DocumentManager.DTO
{
    public class DocumentPostModel
    {
        public string FileName { get; set; }

        public long DocumentTypeID { get; set; }

        public string FileTypeCode { get; set; }

        public string Comment { get; set; }
    }
}
