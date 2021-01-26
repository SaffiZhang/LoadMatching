
using System;

namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetExceptionEventsResult
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string ReferenceNumber { get; set; }
        public int? Hresult { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public DateTime? LoggedOn { get; set; }
    }
}
