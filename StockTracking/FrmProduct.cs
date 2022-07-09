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
using StockTracking.DAL;
using StockTracking.DAL.DTO;
using StockTracking.DAL.DAO;

namespace StockTracking
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
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

        public ProductDTO dto = new ProductDTO();
        ProductBLL bll = new ProductBLL();
        ProductDetailDTO product = new ProductDetailDTO();

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "")
                MessageBox.Show("Product Name is empty.");
            else if (cmbCategory.SelectedIndex == -1)
                MessageBox.Show("Please select a category.");
            else if (txtPrice.Text.Trim() == "")
                MessageBox.Show("Please enter a price for the product.");
            else
            {
                product.ProductName = txtProductName.Text;
                product.CategoryID = Convert.ToInt32(cmbCategory.SelectedValue);
                product.Price = Convert.ToInt32(txtPrice.Text);
                if (bll.Insert(product))
                {
                    MessageBox.Show("Product was added.");
                    txtProductName.Clear();
                    cmbCategory.SelectedIndex = -1;
                    txtPrice.Clear();
                    //this.Close();
                }
            }
        }
    }
}
