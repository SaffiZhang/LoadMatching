using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Application.AssignedLoad.Models.Commands
{
    public class UpdateCustomerTrackingCommand
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int UpdatedBy { get; set; }
        public bool CustTracking { get; set; }
    }
}
