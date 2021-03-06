using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries;
using LoadLink.LoadMatching.Domain.Entities;
using LoadLink.LoadMatching.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public class PostingBase:Entity<int>,IAggregateRoot
    {
        public PostingBase()
        {
        }
        public override int Id
        {
            get
            {
                Id = this.Token;
                return Token;
            }
        }
        public PostingBase(string custCD, DateTime dateAvail,
            string srceCity, string srceSt, int srceRadius, 
            string destCity, string destSt, int destRadius, 
            int vSize, int vType, string vehicleSizeStr, string vehicleTypeStr,
            long pAttrib, string pAttribStr,
            string comment, string postMode, string clientRefNum, string productName,  
             int networkId, string corridor,  bool customerTracking,
             int createdBy)
        {
            if (custCD == null)
                throw new ArgumentNullException("Exception: PostingBase(...) constructor, the CustCD is null! please check!");
            if (srceCity == null)
                throw new ArgumentNullException("Exception: PostingBase(...) constructor, the srceCity is null! please check!");
            if (srceSt == null)
                throw new ArgumentNullException("Exception: PostingBase(...) constructor, the srceSt is null! please check!");
            if (destCity == null)
                throw new ArgumentNullException("Exception: PostingBase(...) constructor, the destCity is null! please check!");
            if (destSt == null)
                throw new ArgumentNullException("Exception: PostingBase(...) constructor, the destSt  is null! please check!");
            CustCD = custCD;
            DateAvail = dateAvail;
            SrceCity = srceCity;
            SrceSt = srceSt;
            SrceRadius = srceRadius;
            DestCity = destCity;
            DestSt = destSt;
            DestRadius = destRadius;
            VSize = vSize;
            VType = vType;
            VehicleSizeStr = vehicleSizeStr;
            VehicleTypeStr = vehicleTypeStr;

            Comment = comment;
            PostMode = postMode;
            ClientRefNum = clientRefNum;
            ProductName = productName;
            PAttrib = pAttrib;
            PAttribStr = pAttribStr;
            CreatedBy = createdBy;
            NetworkId = networkId;
            Corridor = corridor;
            //IsGlobalExcluded = globalExcluded;
            CustomerTracking = customerTracking;
            PStatus = "O";
            CreatedOn = DateTime.Now;
            
        }

       

        public void UpdateDistanceAndPointId(PostingDistanceAndPointId distanceAndPointId)
        {
            
            Token =Token==0? distanceAndPointId.Token:Token;

            SrceID = SrceID == 0 ? distanceAndPointId.SrceID : SrceID;

            SrceCountry = SrceCountry == 0 ? distanceAndPointId.SrceCountry : SrceCountry;
            SrceLong = SrceLong == 0 ? distanceAndPointId.SrceLong : SrceLong;
            SrceLat = SrceLat == 0 ? distanceAndPointId.SrceLat : SrceLat;
            SrceMarketAreaID = distanceAndPointId.SrceMarketAreaID;

            DestID = DestID == 0 ? distanceAndPointId.DestID : DestID;
            DestCountry = DestCountry == 0 ? distanceAndPointId.DestCountry : DestCountry;
            DestLong = DestLong == 0 ? distanceAndPointId.DestLong : DestLong;
            DestLat = DestLat == 0 ? distanceAndPointId.DestLat : DestLat;
            DestMarketAreaID = distanceAndPointId.DestMarketAreaID;

            //Distance = distanceAndPointId.Distance;
            //GoogleMileage = distanceAndPointId.GoogleMileage;

            //switch (postingType)
            //{
            //    case "E":
            //        AddDomainEvent(new EquipmentPostingSavedDomainEvent(this));
            //        break;
            //    case "L":
            //        AddDomainEvent(new LoadPostingSavedDomainEvent(this));
            //        break;
            //}

        }
        
     
        public int Token { get; set; }
        public string CustCD { get; set; }
        public DateTime DateAvail { get; set; }
        public int SrceID { get; set; }
        public string SrceCity { get; set; }
        public string SrceSt { get; set; }
        public int SrceCountry { get; set; }
        public double SrceLong { get; set; }
        public double SrceLat { get; set; }
        public int SrceRadius { get; set; }
        public int SrceMarketAreaID { get; set; }
        public int DestID { get; set; }
        public string DestCity { get; set; }
        public string DestSt { get; set; }
        public int DestCountry { get; set; }
        public double DestLong { get; set; }
        public double DestLat { get; set; }
        public int DestRadius { get; set; }
        public int DestMarketAreaID { get; set; }
        public int VSize { get; set; }
        public int VType { get; set; }
        public string VehicleSizeStr { get; set; }
        public string VehicleTypeStr { get; set; }
        public string Comment { get; set; }
        public string PostMode { get; set; }
        public string ClientRefNum { get; set; }
        public string ProductName { get; set; }
        public int? Route { get; set; }
        public long PAttrib { get; set; }
        public string PAttribStr { get; set; }
        public int? Distance { get; set; }
        public int? GoogleMileage { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public string PStatus { get; set; }
        public int NetworkId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public string Corridor { get; set; }
        public int OriginalToken { get; set; }
        public int? InitialLeadsCount { get; set; }
        public bool? CustomerTracking { get; set; }
        //saffi-- 2021-09-10
        //public bool IsGlobalExcluded { get; set; }
        public bool IsLinkLeadCompleted { get; set; }
        public bool IsDatLeadCompleted { get; set; }
        public bool IsLegacyLeadCompleted { get; set; }
        public int LeadsCount { get; set; } = 0;
        public List<LeadBase> SecondaryLeads { get; set; }
        public void Add2ndLeads( LeadBase lead)
        {
            this.SecondaryLeads = SecondaryLeads?? new List<LeadBase>();
            SecondaryLeads.Add(lead);
            
        }
        public Route GetRoute()
        {
            return new Route(GetSourcePoint(), GetDestinationPoint());
        }
        public Point GetSourcePoint()
        {
            return new Point(SrceLong,
                                   SrceLat,
                                   SrceRadius,
                                   SrceID,
                                   SrceCity,
                                   SrceSt,
                                   (short)SrceCountry,
                                   SrceMarketAreaID
                                   );
        }
        public Point GetDestinationPoint()
        {
            return new Point(       DestLong,
                                   DestLat,
                                    DestRadius,
                                    DestID,
                                   DestCity,
                                   DestSt,
                                  
                                   (short)DestCountry,
                                  
                                   DestMarketAreaID);
        }
        
    }
}
