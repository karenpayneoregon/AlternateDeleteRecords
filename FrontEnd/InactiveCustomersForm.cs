using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEndData;
using FrontEnd.Extensions;

namespace FrontEnd
{
    public partial class InactiveCustomersForm : Form
    {
        private List<CustomerItem> inactiveCustomers;
        public InactiveCustomersForm()
        {
            InitializeComponent();
        }

        public InactiveCustomersForm(List<CustomerItem> pList )
        {
            InitializeComponent();
            inactiveCustomers = pList;
            Shown += InactiveCustomersForm_Shown;
        }

        private void InactiveCustomersForm_Shown(object sender, EventArgs e)
        {
            foreach (CustomerItem item in inactiveCustomers)
            {
                dataGridView1.Rows.Add(item.Id, item.Name);
            }

            dataGridView1.ExpandColumns();
        }
    }
}
