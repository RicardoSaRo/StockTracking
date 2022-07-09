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
using StockTracking.DAL.DTO;

namespace StockTracking
{
    public partial class FrmCustomerList : Form
    {
        public FrmCustomerList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCustomer frm = new FrmCustomer();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
        }

        CustomerBLL bll = new CustomerBLL();
        CustomerDTO dto = new CustomerDTO();
        private void FrmCustomerList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Customers;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Customer Name";
            //dataGridView1.Columns[2].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            CustomerDTO list = new CustomerDTO();
            list = bll.Select();
            list.Customers = list.Customers.Where(x => x.CustomerName.ToLower().Contains(txtCustomerName.Text.ToLower())).ToList();
            dataGridView1.DataSource = list.Customers;
        }

        CustomerDetailDTO detail = new CustomerDetailDTO();
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmCustomer frm = new FrmCustomer();
            frm.detail = detail;
            frm.isUpdate = true;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            bll = new CustomerBLL();
            dto = bll.Select();
            dataGridView1.DataSource = dto.Customers;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
