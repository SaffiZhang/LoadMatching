
using System;



namespace LoadLink.LoadMatching.Domain.Entities
{
    public class DatCustomerBlocked
    {
        public string CustCD { get; set; }
        public string DatCustXref { get; set; }
        public int CustCdIdentity { get; set; }
        public string TCSI_OFFICE_ID { get; set; }
        public string XMT_NAME { get; set; }
        public string XMT_PH1 { get; set; }
        public string XMT_PH2 { get; set; }
        public string XMT_PH3 { get; set; }
        public string XMT_PHONE { get; set; }
        public string ICC { get; set; }
        public string HOMEPROV { get; set; }
        public DateTime? LAST_USED { get; set; }
        public int? TIA_CompanyID { get; set; }
        public bool P3Partner { get; set; }
        public string COMPANY_ID { get; set; }
        public string POSTER_TYPE { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public DateTime? OPEN_DATE { get; set; }
        public DateTime? CLOSE_DATE { get; set; }
        public DateTime? CUSTOMER_SINCE { get; set; }
        public string ACCOUNT_COMMENT { get; set; }
        public string ACCOUNT_STATUS { get; set; }
        public string COMPANY_FAX_PHONE { get; set; }
        public string DUNS { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ADDRESS3 { get; set; }
        public string CITY { get; set; }
        public string POSTAL_CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string CONTACT_FAX_PHONE { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public decimal? CREDIT_SCORE { get; set; }
        public DateTime? CREDIT_SCORE_DATE { get; set; }
        public string RivieraGuid { get; set; }
        public string QPstatus { get; set; }
        public string AccountType { get; set; }
        public string GETLOADED_XREF { get; set; }
        public bool TIA_MEMBER { get; set; }
        public int TIA_LEVEL { get; set; }
        public string BROKER_DOCKET_NUMBER { get; set; }
        public string CARRIER_DOCKET_NUMBER { get; set; }
        public string DOT_NUMBER { get; set; }
        public string ACCOUNT_ID { get; set; }
        public string USERID { get; set; }
        public DateTime? CREATEDON_DATE { get; set; }
    }
}