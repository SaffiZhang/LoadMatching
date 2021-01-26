
namespace LoadLink.LoadMatching.Domain.Procedures
{
    public class UspGetPDRatioResult
    {
        public int? WkOutEquip { get; set; }
        public int? WkOutLoad { get; set; }
        public decimal? WkOutRatio { get; set; }
        public int? MthOutEquip { get; set; }
        public int? MthOutLoad { get; set; }
        public decimal? MthOutRatio { get; set; }
    }
}
