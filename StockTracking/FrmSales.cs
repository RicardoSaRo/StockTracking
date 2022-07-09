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
    public partial class FrmSales : Form
    {
        public FrmSales()
        {
            InitializeComponent();
        }

        private void txtSalesAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        SalesDetailDTO detail = new SalesDetailDTO();
        bool comboFull = false;
        private void FrmSales_Load(object sender, EventArgs e)
        {
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            comboFull = true;

            gridProduct.DataSource = dto.Products;
            gridProduct.Columns[0].Visible = false; //--> ProductID
            gridProduct.Columns[1].HeaderText = "Product Name";
            gridProduct.Columns[2].Visible = false; //--> CategoryID
            gridProduct.Columns[3].Visible = false; //--> CategoryName
            gridProduct.Columns[4].Visible = false; //--> StockAmount
            gridProduct.Columns[5].Visible = false; //--> Price

            gridCostumer.DataSource = dto.Customers;
            gridCostumer.Columns[0].Visible = false;
            gridCostumer.Columns[1].HeaderText = "Customer Name";
        }

        private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> customerlist = new List<CustomerDetailDTO>();
            customerlist = dto.Customers;
            customerlist = customerlist.Where(x => x.CustomerName.ToLower().Contains(txtCustomerName.Text.ToLower())).ToList();
            gridCostumer.DataSource = customerlist;
            if (customerlist.Count == 0)
            {
                txtSalesAmount.Clear();
                txtCustomerSearch.Clear();
                txtStock.Clear();
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<ProductDetailDTO> productlist = new List<ProductDetailDTO>();
                productlist = dto.Products;
                productlist = productlist.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                gridProduct.DataSource = productlist;
                if (productlist.Count == 0)
                {
                    txtSalesAmount.Clear();
                    txtCustomerSearch.Clear();
                    txtStock.Clear();
                }
            }
        }

        private void gridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductID = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[0].Value);
            txtProductName.Text = gridProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.ProductName = txtProductName.Text;
            detail.CategoryID = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[2].Value);
            detail.CategoryName = Convert.ToString(gridProduct.Rows[e.RowIndex].Cells[3].Value);
            txtStock.Text = gridProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            detail.StockAmount = Convert.ToInt32(txtStock.Text);
            txtPrice.Text = gridProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
            detail.ProductSalesPrice = Convert.ToInt32(txtPrice.Text);
        }

        private void gridCostumer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.CustomerID = Convert.ToInt32(gridCostumer.Rows[e.RowIndex].Cells[0].Value);
            txtCustomerName.Text = gridCostumer.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CustomerName = Convert.ToString(txtCustomerName.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "")
                MessageBox.Show("Please select a product from table.");
            else if (txtCustomerName.Text.Trim() == "")
                MessageBox.Show("Please select a customer from table.");
            else if (txtSalesAmount.Text.Trim() == "")
                MessageBox.Show("Please enter the product sale amount.");
            else if (Convert.ToInt32(txtSalesAmount.Text) > detail.StockAmount)
                MessageBox.Show("Not enough product stock for that sale amount.");
            else
            {
                detail.ProductSalesAmount = Convert.ToInt32(txtSalesAmount.Text);
                detail.SalesDate = DateTime.Today;
                //detail.ProductSalesPrice *= Convert.ToInt32(txtStock.Text); //--> Sale Total
                if (bll.Insert(detail))
                {
                    MessageBox.Show("Sale was added.");
                    bll = new SalesBLL();
                    dto = bll.Select();
                    gridProduct.DataSource = dto.Products;
                    gridCostumer.DataSource = dto.Customers;
                    comboFull = false;
                    cmbCategory.DataSource = dto.Categories;
                    if (dto.Products.Count == 0)
                        comboFull = true;
                    cmbCategory.SelectedIndex = -1;
                    txtSalesAmount.Clear();
                    txtCustomerSearch.Clear();
                }
            }
        }
    }
}
