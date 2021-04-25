namespace DocumentManager.DTO.Models
{
    public class DocumentEditResponseModel
    {
        public DocumentEditModel ProposedValues { get; set; }

        public DocumentEditModel DatabaseValues { get; set; }

        public bool HasConcurrencyError { get; set; }
    }
}
