using System.Windows.Forms;

namespace FrontEnd.Extensions
{
    public static class KarenDialogs
    {
        /// <summary>
        /// Provides a dialog with yes/no buttons with "no" as the default button.
        /// </summary>
        /// <param name="pText"></param>
        /// <returns></returns>
        public static bool Question(string pText)
        {
            return (MessageBox.Show(pText, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }
    }
}
