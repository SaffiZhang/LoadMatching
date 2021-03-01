using System;

namespace LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands
{
    public class UpdateTemplatePostingSPCommand
    {
        public int UserId { get; set; }
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string PostType { get; set; }
        public DateTime DateAvail { get; set; }
        public int SrceID { get; set; }
        public string SrceCity { get; set; }
        public string SrceSt { get; set; }
        public int SrceRadius { get; set; }
        public int DestID { get; set; }
        public string DestCity { get; set; }
        public string DestSt { get; set; }
        public int DestRadius { get; set; }
        public long VSize { get; set; }
        public int VType { get; set; }
        public string Comment { get; set; }
        public string PostMode { get; set; }
        public string ClientRefNum { get; set; }
        public long PAttrib { get; set; }
        public int NetworkId { get; set; }
        public string Corridor { get; set; }
        public string CustCd { get; set; }
        public bool? CustomerTracking { get; set; }
    }
}
