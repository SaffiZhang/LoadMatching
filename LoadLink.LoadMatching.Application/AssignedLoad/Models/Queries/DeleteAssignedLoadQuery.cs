
namespace LoadLink.LoadMatching.Application.AssignedLoad.Models.Queries
{
    public class DeleteAssignedLoadQuery
    {
        public string CustCd { get; set; }
        public int? EquipCreatorUserId { get; set; } //TO DO: SP Change - default should be INT (0) not ''
        public int? EToken { get; set; }
        public string LoadCustCd { get; set; }
        public int? LoadCreatorUserId { get; set; }
        public int Token { get; set; }
        public int? DriverID { get; set; }
    }
}
