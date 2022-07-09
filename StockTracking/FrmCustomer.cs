using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.BLL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;

namespace StockTracking
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        CustomerBLL bll = new CustomerBLL();
        public CustomerDetailDTO detail = new CustomerDetailDTO();
        public bool isUpdate = false;
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            txtCustomerName.Text = detail.CustomerName;
            btnClose.Text = "Cancel";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() == "")
                MessageBox.Show("Customer name is empty.");
            else
            {
                CustomerDetailDTO customer = new CustomerDetailDTO();
                customer.CustomerName = txtCustomerName.Text;
                if (isUpdate)
                {
                    customer.ID = detail.ID;
                    if (bll.Update(customer))
                    {
                        MessageBox.Show("Customer was updated.");
                        this.Close();
                    }
                }
                else
                {
                    
                    if (bll.Insert(customer))
                    {
                        MessageBox.Show("Customer was added.");
                        txtCustomerName.Clear();
                    }
                }
            }
            
        }
    }
}
