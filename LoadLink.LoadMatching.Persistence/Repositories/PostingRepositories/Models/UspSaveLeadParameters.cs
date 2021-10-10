using System;
using System.Collections.Generic;
using System.Text;


namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.Models
{
    public class UspSaveLeadParameters
    {
		public string CustCD { get; set; }
		public int EToken { get; set; }
		public int LToken { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public string LeadType { get; set; }
		public string MCustCD { get; set; }
		public DateTime MDateAvail { get; set; }
		public int MSrceID { get; set; }
		public string MSrceCity { get; set; }
		public string MSrceSt { get; set; }
		public int MSrceCountry { get; set; }
		public double MSrceLong { get; set; }
		public double MSrceLat { get; set; }
		public int MDestID { get; set; }
		public string MDestCity { get; set; }
		public string MDestSt { get; set; }
		public int MDestCountry { get; set; }
		public double MDestLong { get; set; }
		public double MDestLat { get; set; }
		public string VehicleSize { get; set; }
		public string VehicleType { get; set; }
		public string MComment { get; set; }
		public string MPostMode { get; set; }
		public string MClientRefNum { get; set; }
		public string MProductName { get; set; }
		public string MPostingAttrib { get; set; }
		public int MCreatedBy { get; set; }
		public DateTime MCreatedOn { get; set; }
		public decimal Distance { get; set; }
		public decimal GoogleMileage { get; set; }
		public int TCUS { get; set; }
		public string DirO { get; set; }
		public decimal DFO { get; set; }
		public decimal DFD { get; set; }
		public decimal DFO_Google { get; set; }
		public decimal DFD_Google { get; set; }
		public string PType { get; set; }
		public bool CustomerTracking { get; set; }

	}
}
