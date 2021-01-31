
namespace LoadLink.LoadMatching.Application.USMemberSearch.Models.Commands
{
    public enum SearchType
    {
        All = 0, // Only Included 
        Included = 1, // All the result (Included + Excluded)
        Excluded = 2  // Only Excluded
    }
}
