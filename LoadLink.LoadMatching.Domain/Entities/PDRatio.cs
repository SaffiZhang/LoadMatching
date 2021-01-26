


namespace LoadLink.LoadMatching.Domain.Entities
{
    public class PDRatio
    {
        public int SrceMarketAreaID { get; set; }
        public int DestMarketAreaID { get; set; }
        public int VType { get; set; }
        public int WkOutEquip { get; set; }
        public int WkOutLoad { get; set; }
        public decimal WkOutRatio { get; set; }
        public int MthOutEquip { get; set; }
        public int MthOutLoad { get; set; }
        public decimal MthOutRatio { get; set; }
    }
}