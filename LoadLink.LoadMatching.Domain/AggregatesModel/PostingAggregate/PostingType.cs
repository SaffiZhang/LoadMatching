using System;
using System.Collections.Generic;
using System.Text;

namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public enum PostingType
    {
        EquipmentPosting,
        DatEquipmentPosting,
        LegacyEquipmentPosting,
        LoadPosting,
        DatLoadPosting,
        LegacyLoadPosting
    }
}
