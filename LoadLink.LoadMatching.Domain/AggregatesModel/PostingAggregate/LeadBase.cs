using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Seedwork;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public abstract class LeadBase: Entity<int>, IAggregateRoot
    {
        public LeadBase()
        {
        }

        public LeadBase(PostingBase posting,
			PostingBase matchedPosting, string dirO,
			bool? isGlobalExcluded=false)
        {
			if (posting == null)
				throw new ArgumentNullException("Exception in LeadBase constructor, posting is null!");
			if (matchedPosting==null)
				throw new ArgumentNullException("Exception in LeadBase constructor, MatchedPosting is null!");

			CreatedBy = posting.CreatedBy;

			DirO = dirO;
            

			this.CustCD = posting.CustCD;
			
			

			this.DestId = posting.DestID;
			this.SrceId = posting.SrceID;

			var m = matchedPosting;
			
			MCustCD = m.CustCD;
			MDateAvail = m.DateAvail;

            MSrceID = m.SrceID;
            MSrceCity = m.SrceCity;
            MSrceSt = m.SrceSt;
            MSrceCountry = m.SrceCountry;
            MSrceLong = m.SrceLong;
            MSrceLat = m.SrceLat;
            MDestID = m.DestID;
            MDestCity = m.DestCity;
            MDestSt = m.DestSt;
            MDestCountry = m.DestCountry;
            MDestLong = m.DestLong;
            MDestLat = m.DestLat;
            VehicleSize = m.VehicleSizeStr;
            VehicleType = m.VehicleTypeStr;
            MComment = m.Comment;
            MPostMode = m.PostMode;
            MClientRefNum = m.ClientRefNum;
            MProductName = m.ProductName;
            MPostingAttrib = m.PAttribStr;

            Distance = m.Distance;
            GoogleMileage = m.GoogleMileage;
			
			MCreatedBy = m.CreatedBy;
            MCreatedOn = m.CreatedOn;
            MDeletedBy = m.DeletedBy;
            MDeletedOn = m.DeletedOn;
            CustomerTracking = m.CustomerTracking?? false;
			
        }
        public int GetSrceId() { return this.SrceId; }
        public int GetDestId() { return this.DestId; }

        public string CustCD { get; set; }
		public int EToken { get; set; }
		public int LToken { get; set; }

		private int SrceId { get; set; }
		private int DestId { get; set; }

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
		public int? MDeletedBy { get; set; }
		public DateTime? MDeletedOn { get; set; }

		public int? Distance { get; set; }
		public int? GoogleMileage { get; set; }

		
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public string LeadType { get; set; }

		public int TCUS { get; set; }
		public string DirO { get; set; }
		public decimal DFO { get; set; }
		public decimal DFD { get; set; }
		public decimal DFO_Google { get; set; }
		public decimal DFD_Google { get; set; }
		public string PType { get; set; }
		public bool CustomerTracking { get; set; }
		public string Contacted { get; set; }
		public string Flag { get; set; }

	}
}
