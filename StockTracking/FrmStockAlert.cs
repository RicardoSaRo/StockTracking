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
    public partial class FrmStockAlert : Form
    {
        public FrmStockAlert()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FrmMain frm = new FrmMain();
            this.Hide();
            frm.ShowDialog();
        }

        ProductBLL bll = new ProductBLL();
        ProductDTO dto = new ProductDTO(); 
        private void FrmStockAlert_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
            dto.Products = dto.Products.Where(x => x.StockAmount <= 25).ToList();

            dataGridView1.DataSource = dto.Products;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Category";
            dataGridView1.Columns[4].HeaderText = "Stock Amount";
            dataGridView1.Columns[5].HeaderText = "Price";

            if (dto.Products.Count() == 0)
            {
                FrmMain frm = new FrmMain();
                this.Hide();
                frm.ShowDialog();
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
