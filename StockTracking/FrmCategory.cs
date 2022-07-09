using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.DAL.DTO;
using StockTracking.BLL;

namespace StockTracking
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        CategoryBLL bll = new CategoryBLL();
        public CategoryDetailDTO detail = new CategoryDetailDTO();
        public bool isUpdate = false;
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                txtCategoryName.Text = detail.CategoryName;
                btnClose.Text = "Cancel";
            }
            
        }
        
        /*
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == "")
                MessageBox.Show("Category name is empty.");
            else
            {
                CategoryDetailDTO category = new CategoryDetailDTO();
                category.CategoryName = txtCategoryName.Text;
                if(bll.Insert(category))
                {
                    MessageBox.Show("Category was added.");
                    txtCategoryName.Clear();
                }
            }
        }*/

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (txtCategoryName.Text == "")
                MessageBox.Show("Category name is empty.");
            else
            {
                CategoryDetailDTO category = new CategoryDetailDTO();
                category.CategoryName = txtCategoryName.Text;
                if (isUpdate)
                {
                    category.ID = detail.ID;
                    if (bll.Update(category))
                    {
                        MessageBox.Show("Category was updated.");
                        txtCategoryName.Clear();
                        this.Close();
                    }
                }
                else
                {
                    if (bll.Insert(category))
                    {
                        MessageBox.Show("Category was added.");
                        txtCategoryName.Clear();
                    }
                }
            }
        }
    }
}
