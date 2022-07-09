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
    public partial class FrmSalesList : Form
    {
        public FrmSalesList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSales frm = new FrmSales();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillData();
            CleanFilters();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        private void FrmSalesList_Load(object sender, EventArgs e)
        {
            FillData();
            chDate.Checked = false;
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
            dataGridView1.Columns[0].Visible = false; //--> Sales ID
            dataGridView1.Columns[1].Visible = false; //--> CustomerID
            dataGridView1.Columns[2].HeaderText = "Customer Name";
            dataGridView1.Columns[3].Visible = false; //--> CategoryID
            dataGridView1.Columns[4].HeaderText = "Category";
            dataGridView1.Columns[5].Visible = false; //--> ProductID
            dataGridView1.Columns[6].HeaderText = "Product Name";
            dataGridView1.Columns[7].HeaderText = "Sales Amount";
            dataGridView1.Columns[8].HeaderText = "Sales Price";
            dataGridView1.Columns[9].Visible = false; //--> Stock Amount
            dataGridView1.Columns[10].HeaderText = "Sales Date";
        }

        private void FillData()
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Sales;
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SalesDetailDTO> search = dto.Sales;
            if (txtCustomerName.Text.Trim() != "")
                search = search.Where(s => s.CustomerName.ToLower().Contains(txtCustomerName.Text.ToLower())).ToList();
            if (txtProductName.Text.Trim() != "")
                search = search.Where(s => s.ProductName.ToLower().Contains(txtProductName.Text.ToLower())).ToList();
            if (cmbCategory.SelectedIndex != -1)
                search = search.Where(s => s.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
            if (txtPrice.Text != "")
            {
                if (rbPriceEquals.Checked)
                    search = search.Where(x => x.ProductSalesPrice == Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    search = search.Where(x => x.ProductSalesPrice > Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    search = search.Where(x => x.ProductSalesPrice < Convert.ToInt32(txtPrice.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from price group.");
            }
            if (txtStock.Text != "")
            {
                if (rbStockEquals.Checked)
                    search = search.Where(x => x.ProductSalesAmount == Convert.ToInt32(txtStock.Text)).ToList();
                else if (rbStockMore.Checked)
                    search = search.Where(x => x.ProductSalesAmount > Convert.ToInt32(txtStock.Text)).ToList();
                else if (rbStockLess.Checked)
                    search = search.Where(x => x.ProductSalesAmount < Convert.ToInt32(txtStock.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from sales amount group.");
            }
            if (chDate.Checked)
                search = search.Where(s => s.SalesDate > dtpStart.Value && s.SalesDate < dtpEnd.Value).ToList();
            dataGridView1.DataSource = search;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            txtCustomerName.Clear();
            txtProductName.Clear();
            cmbCategory.SelectedIndex = -1;
            rbPriceEquals.Checked = false;
            rbStockEquals.Checked = false;
            rbPriceMore.Checked = false;
            rbStockMore.Checked = false;
            rbPriceLess.Checked = false;
            rbStockLess.Checked = false;
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            chDate.Checked = false;
            dataGridView1.DataSource = dto.Sales;
        }

        private void chDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpStart.Enabled = (chDate.Checked) ? true : false;
            dtpEnd.Enabled = (chDate.Checked) ? true : false;

        }
    }
}
