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
using SqlRepository;

using static FrontEnd.Extensions.KarenDialogs;

namespace FrontEnd
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Container for Customers DataTable
        /// </summary>
        private readonly BindingSource _bsCustomers = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var ops = new SqlServerOperations();

            /*
             * Read only active customers and active contacts
             */
            _bsCustomers.DataSource = ops.Customers(true);

            bindingNavigator1.BindingSource = _bsCustomers;
            dataGridView1.DataSource = _bsCustomers;
            
            /*
             * Configure how columns are shown, not shown
             */
            var configItems = ops.CustomerConfigurationItems();
            foreach (DataGridViewColumnDefinition item in configItems)
            {

                if (!dataGridView1.Columns.Contains(item.Name)) continue;

                dataGridView1.Columns[item.Name].DisplayIndex = item.Position;
                dataGridView1.Columns[item.Name].Visible = item.Visible;
                dataGridView1.Columns[item.Name].HeaderText = item.DisplayText;

            }

            dataGridView1.ExpandColumns();

            cmdGetInactiveCustomers.Enabled = ops.InactiveCustomers().Count > 0;
            cmdActivateCustomer.DataBindings.Add("Enabled", cmdGetInactiveCustomers, "Enabled");


        }
        /// <summary>
        /// Adding is disabled, show user a message indicating this.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Disabled for this code sample");
        }
        /// <summary>
        /// Promot user to deactivate the currently selected customer record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorMakeInActive_Click(object sender, EventArgs e)
        {
            if (_bsCustomers.CurrentIsValid())
            {
                if (Question($"Remove '{_bsCustomers.CurrentRow().Field<string>("CompanyName")}'"))
                {
                    var ops = new SqlServerOperations();
                    
                    if (ops.InactivateCustomer(_bsCustomers.CurrentRow().Field<int>("CustomerIdentifier")))
                    {
                        _bsCustomers.RemoveCurrent();
                        cmdGetInactiveCustomers.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(ops.LastExceptionMessage);
                    }
                }
            }
        }
        /// <summary>
        /// Reset all Customers to active status, requires a restart. Normally
        /// would never been done in an application but this is to show it's easily done.
        /// 
        /// Under normal conditions this would not be an available option, it's here for
        /// demonstrative purposed of this code sample.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdActivateAllCustomers_Click(object sender, EventArgs e)
        {
            if (Question("This will set all customers to active status, continue"))
            {
                var ops = new SqlServerOperations();
                MessageBox.Show(ops.ActivateAllCustomer() ? "Finished, Press OK to restart." : ops.LastExceptionMessage);
                Application.Restart();
            }
        }
        /// <summary>
        /// Get in active customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInActiveCustomers_Click(object sender, EventArgs e)
        {
            var ops = new SqlServerOperations();
            var customerList = ops.InactiveCustomers();

            if (customerList.Count >0)
            {
                var f = new InactiveCustomersForm(customerList);
                try
                {
                    f.ShowDialog();
                }
                finally
                {
                    f.Dispose();
                }
            }
            else
            {
                MessageBox.Show("There are no inactive customers at this time");
            }
        }
        /// <summary>
        /// Get all inactive customers to use for reactivating one or more
        /// customers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetInactiveCustomers_Click(object sender, EventArgs e)
        {
            var ops = new SqlServerOperations();
            var customerList = ops.InactiveCustomers();

            if (customerList.Count > 0)
            {
                cboInactiveCustomers.AutoCompleteMode = AutoCompleteMode.Suggest;
                cboInactiveCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                cboInactiveCustomers.DataSource = customerList;
                cboInactiveCustomers.DisplayMember = "Name";
                cboInactiveCustomers.ValueMember = "id";
            }
            else
            {
                MessageBox.Show("Currently there are no inactive customers.");
            }

        }
        /// <summary>
        /// Used to activate the currently selected inactive customer in the
        /// ComboBox to the left.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdActivateCustomer_Click(object sender, EventArgs e)
        {
            if (cboInactiveCustomers.SelectedIndex > -1)
            {
                var ops = new SqlServerOperations();
                if (ops.ActivateCustomer((int)cboInactiveCustomers.SelectedValue))
                {
                    MessageBox.Show($"'{cboInactiveCustomers.Text}' will be available when starting this application again.");
                }
                else
                {
                    MessageBox.Show("Failed to activate customer");
                }
            }
        }
    }
}
