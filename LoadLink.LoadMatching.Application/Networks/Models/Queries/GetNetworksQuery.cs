
namespace LoadLink.LoadMatching.Application.Networks.Models.Queries
{
    public class GetNetworksQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public string CustCD { get; set; }
    }
}

