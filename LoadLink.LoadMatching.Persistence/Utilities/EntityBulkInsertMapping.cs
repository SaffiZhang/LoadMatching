
using System.Data.SqlClient;

namespace LoadLink.LoadMatching.Persistence.Utilities
{
    public static class BulkInsertMapping<T>  where T:class
    {
        public static SqlBulkCopy ColumnMappingToParaClass ( SqlBulkCopy sqlBulkCopy)
        {
            if (sqlBulkCopy == null)
                return null;
            
          
            var tPropetyInfo = typeof(T).GetProperties();
            foreach (var tp in tPropetyInfo)
            {
                sqlBulkCopy.ColumnMappings.Add(tp.Name, tp.Name);
            }
            return sqlBulkCopy;

        }
    }
}
