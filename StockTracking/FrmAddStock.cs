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
    public partial class FrmAddStock : Form
    {
        public FrmAddStock()
        {
            InitializeComponent();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool comboFull = false;
        ProductBLL pbll = new ProductBLL();
        ProductDTO dto = new ProductDTO();
        private void FrmAddStock_Load(object sender, EventArgs e)
        {
            FillData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Category";
            dataGridView1.Columns[4].HeaderText = "Stock Amount";
            dataGridView1.Columns[5].HeaderText = "Price";
        }

        private void FillData()
        {
            dto = pbll.Select();
            cmbCategory.DataSource = dto.Categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = -1;
            if (dto.Categories.Count > 0)
                comboFull = true;
            dataGridView1.DataSource = dto.Products;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cmbCategory.SelectedValue)).ToList();
                dataGridView1.DataSource = list;
                if (list.Count() == 0)
                {
                    txtPrice.Clear();
                    txtProductName.Clear();
                    txtStock.Clear();
                }
            }
        }

        ProductDetailDTO detail = new ProductDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductName.Text = detail.ProductName;
            detail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            txtPrice.Text = detail.Price.ToString();
            detail.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            //txtStock.Text = detail.StockAmount.ToString();
            detail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductName.Text.Trim() == "")
                MessageBox.Show("Please select a product from the table.");
            else if (txtStock.Text.Trim() == "")
                MessageBox.Show("Please give a stock amount.");
            else
            {
                detail.StockAmount += Convert.ToInt32(txtStock.Text);
                if (pbll.Update(detail))
                {
                    MessageBox.Show("Stock quantity updated.");
                    FillData();
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                if (Convert.ToInt32(e.Value) == 0)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.DarkRed;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else if (Convert.ToInt32(e.Value) <= 10)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.White;
                }
                else if (Convert.ToInt32(e.Value) <= 25)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Goldenrod;
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Yellow;
                }
            }
        }
    }
}
