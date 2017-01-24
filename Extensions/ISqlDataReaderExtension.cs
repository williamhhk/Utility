using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ISqlReaderEx
    {
        public static T Get<T>(this IDataReader sqlReader, string fieldName)
        {
            int? ordinal = null;
            try
            {
                ordinal = sqlReader.GetOrdinal(fieldName);
            }
            catch
            {
            }
            return (ordinal.HasValue) ? (T)sqlReader.GetValue(ordinal.Value) : default(T);
            //            return (ordinal.HasValue) ? sqlReader.GetFieldValue<T>(ordinal.Value) : default(T);
        }
    }
}
