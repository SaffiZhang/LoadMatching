
namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspCreateSharedDocumentResult
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string EToken { get; set; }
        public bool IsSelected { get; set; }
    }
}
