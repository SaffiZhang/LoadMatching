using System.Collections.Generic;
using Xunit;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Persistence.Utilities;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace LoadLink.LoadMatching.Persistence.Test
{
    public class LeadBulkInsertTest
    {
        [Fact]
        public async Task BulkInsertLeadTest()
        {
            var lead = FakePosting.LeadBase();
            lead.LeadType = "P";
            
            lead.EToken = 1;
            lead.LToken = 1;


            var leads = new List<LeadBase>() { lead };
            var con = new SqlConnection(DbConnectionStr.ConnectionStr);
            SqlBulkCopy bulk = new SqlBulkCopy(con);
            bulk.DestinationTableName = "EquipmentLead";
            bulk.ColumnMappings.Add("ID", "ID");
            bulk.ColumnMappings.Add("CustCD", "CustCD");
            bulk.ColumnMappings.Add("EToken", "EToken");
            bulk.ColumnMappings.Add("LToken", "LToken");
            bulk.ColumnMappings.Add("CreatedOn", "CreatedOn");
            bulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
            bulk.ColumnMappings.Add("LeadType", "LeadType");
            bulk.ColumnMappings.Add("MCustCD", "MCustCD");
            bulk.ColumnMappings.Add("MDateAvail", "MDateAvail");
            con.Open();
            await bulk.WriteToServerAsync(ListToDataTableConverter.ToDataTable<LeadBase>(leads));
            con.Close();







        }
    }
}
