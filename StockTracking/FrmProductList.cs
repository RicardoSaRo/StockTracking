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
    public partial class FrmProductList : Form
    {
        public FrmProductList()
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmProduct frm = new FrmProduct();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillData();
            CleanData();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO();

        void FillData()
        {
            dto = bll.Select();
            dataGridView1.DataSource = dto.Products;
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
        }

        private void FrmProductList_Load(object sender, EventArgs e)
        {
            
            FillData();
            CleanData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Category";
            dataGridView1.Columns[4].HeaderText = "Stock Amount";
            dataGridView1.Columns[5].HeaderText = "Price";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dto = bll.Select();
            if (txtProductName.Text.Trim() != "")
                dto.Products = dto.Products.Where(x => x.ProductName.ToLower().Contains((txtProductName.Text.ToLower()))).ToList();
            if (cmbCategory.SelectedIndex != -1)
                dto.Products = dto.Products.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
            if (txtPrice.Text != "")
            {
                if (rbPriceEquals.Checked)
                    dto.Products = dto.Products.Where(x => x.Price == Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    dto.Products = dto.Products.Where(x => x.Price > Convert.ToInt32(txtPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    dto.Products = dto.Products.Where(x => x.Price < Convert.ToInt32(txtPrice.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from price group.");
            }
            if (txtStock.Text != "")
            {
                if (rbStockEquals.Checked)
                    dto.Products = dto.Products.Where(x => x.StockAmount == Convert.ToInt32(txtStock.Text)).ToList();
                else if (rbStockMore.Checked)
                    dto.Products = dto.Products.Where(x => x.StockAmount > Convert.ToInt32(txtStock.Text)).ToList();
                else if (rbStockLess.Checked)
                    dto.Products = dto.Products.Where(x => x.StockAmount < Convert.ToInt32(txtStock.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from stock group.");
            }
            dataGridView1.DataSource = dto.Products;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            CleanData();
            FillData();
        }

        private void CleanData()
        {
            txtProductName.Clear();
            cmbCategory.SelectedValue = -1;
            txtPrice.Clear();
            rbPriceEquals.Checked = false;
            rbPriceMore.Checked = false;
            rbPriceLess.Checked = false;
            txtStock.Clear();
            rbStockEquals.Checked = false;
            rbStockMore.Checked = false;
            rbStockLess.Checked = false;
        }
    }
}
