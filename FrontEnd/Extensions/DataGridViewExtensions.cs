using System.Windows.Forms;

namespace FrontEnd.Extensions
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Expand all columns excluding in this case Orders column
        /// </summary>
        /// <param name="sender"></param>
        public static void ExpandColumns(this DataGridView sender)
        {
            foreach (DataGridViewColumn col in sender.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}
