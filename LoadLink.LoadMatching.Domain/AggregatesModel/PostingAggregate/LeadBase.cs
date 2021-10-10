using System;
using System.Collections.Generic;
using System.Text;
using LoadLink.LoadMatching.Domain.Seedwork;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.Events;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public  class LeadBase: Entity<int>, IAggregateRoot
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
			DateAvail = posting.DateAvail;
			

		

			var m = matchedPosting;
			
			MCustCD = m.CustCD;
			MDateAvail = m.DateAvail;

            MSrceID = m.SrceID;
            MSrceCity = m.SrceCity;
            MSrceSt = m.SrceSt;
            MSrceCountry = m.SrceCountry;
          
            MDestID = m.DestID;
            MDestCity = m.DestCity;
            MDestSt = m.DestSt;
            MDestCountry = m.DestCountry;
     
            VehicleSize = m.VehicleSizeStr;
            VehicleType = m.VehicleTypeStr;
            MComment = m.Comment;
            MPostMode = m.PostMode;
       
            MPostingAttrib = m.PAttribStr;

            
		
            MCreatedOn = m.CreatedOn;
           
			CreatedOn = DateTime.Now;
			
        }
		public int ID { get; set; }
		public string CustCD { get; set; }
		public int EToken { get; set; }
		public int LToken { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public string LeadType { get; set; }//P--primary; S--2nd
		public string MCustCD { get; set; }
		public DateTime DateAvail { get; set; }
		public DateTime MDateAvail { get; set; }
		public int MSrceID { get; set; }
		public string MSrceCity { get; set; }
		public string MSrceSt { get; set; }
		public int MSrceCountry { get; set; }
		public int MDestID { get; set; }
		public string MDestCity { get; set; }
		public string MDestSt { get; set; }
		public int MDestCountry { get; set; }
		public string VehicleSize { get; set; }
		public string VehicleType { get; set; }
		public string MComment { get; set; }
		public string MPostMode { get; set; }
		public string MPostingAttrib { get; set; }
		public string MCompany { get; set; }
		public int MTCC { get; set; }
		public int MEquifax { get; set; }
		public int MQpStatus { get; set; }
		public DateTime MCreatedOn { get; set; }
		public decimal Distance { get; set; }
		public decimal GoogleMileage { get; set; }
		public string DirO { get; set; }
		public decimal DFO { get; set; }
		public decimal DFD { get; set; }
		public decimal DFO_Google { get; set; }
		public decimal DFD_Google { get; set; }
		public int Contacted { get; set; }//Reflesh by contact
		

		public override int Id { get => base.Id; set => base.Id = value; }
		public void SetId(int id)
        {
			this.Id = id;
        }

    }
}
