using System.Data;
using System.Windows.Forms;

namespace FrontEnd.Extensions
{
    public static class BindingSourceExtensions
    {
        /// <summary>
        /// Obtain the underlying data source as a DataTable
        /// </summary>
        /// <param name="pBindingSource"></param>
        /// <returns></returns>
        /// <remarks>A runtime exception is thrown if DataSource is not a DataTable</remarks>
        public static DataTable DataTable(this BindingSource pBindingSource)
        {
            return (DataTable)pBindingSource.DataSource;
        }
        /// <summary>
        /// Cast current row to a DataRow
        /// </summary>
        /// <param name="pBindingSource"></param>
        /// <returns></returns>
        public static DataRow CurrentRow(this BindingSource pBindingSource)
        {
            return ((DataRowView)pBindingSource.Current).Row;
        }
        /// <summary>
        /// Determine if there is a current row
        /// </summary>
        /// <param name="pBindingSource"></param>
        /// <returns></returns>
        public static bool CurrentIsValid(this BindingSource pBindingSource)
        {
            return pBindingSource.Current != null;
        }
    }

}
