using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshop_Management_System
{
    public partial class Books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ISURU;Initial Catalog=bookShopDB;Integrated Security=True");
        public Books()
        {
            InitializeComponent();
            loadDatagridView();
        }

        private void loadDatagridView() {
            con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvBooklist.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Filter()
        {
            con.Open();
            string query = "select * from BookTbl where BCat = '" + cmbFilterByCatagory.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvBooklist.DataSource = ds.Tables[0];
            con.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookTitle.Text == "" || txtAuthor.Text == "" || cmbCatagories.SelectedIndex == 6 || txtQuantity.Text == "" || txtPrice.Text == "")
            {

                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into BookTbl values('" + txtBookTitle.Text + "','" + txtAuthor.Text + "','" + cmbCatagories.SelectedItem.ToString() + "','" + txtQuantity.Text + "','" + txtPrice.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Saved Successfully");
                    con.Close();
                    reset();
                    loadDatagridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbFilterByCatagory.SelectedIndex = 6;
            loadDatagridView();            
        }

        private void cmbFilterByCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void reset()
        {
            txtBookTitle.Text = "";
            txtAuthor.Text = "";
            cmbCatagories.SelectedIndex = 6;
            txtQuantity.Text = "";
            txtPrice.Text = "";     
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
            cmbFilterByCatagory.SelectedIndex = 6;
            loadDatagridView();
        }

        int key;
        private void dgvBooklist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBookTitle.Text = dgvBooklist.SelectedRows[0].Cells[1].Value.ToString();
            txtAuthor.Text = dgvBooklist.SelectedRows[0].Cells[2].Value.ToString();
            cmbCatagories.SelectedItem = dgvBooklist.SelectedRows[0].Cells[3].Value.ToString();
            txtQuantity.Text = dgvBooklist.SelectedRows[0].Cells[4].Value.ToString();
            txtPrice.Text = dgvBooklist.SelectedRows[0].Cells[5].Value.ToString();

            if (txtBookTitle.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dgvBooklist.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {

                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from BookTbl where Bid=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted Successfully");
                    con.Close();
                    loadDatagridView();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtBookTitle.Text == "" || txtAuthor.Text == "" || cmbCatagories.SelectedIndex == 6 || txtQuantity.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update BookTbl set BTitle='" + txtBookTitle.Text + "',BAuthor='" + txtAuthor.Text + "',BCat='" + cmbCatagories.SelectedItem.ToString() + "',BQty='" + txtQuantity.Text + "',BPrice='" + txtPrice.Text + "' where Bid=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Updated Successfully");
                    con.Close();
                    loadDatagridView();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }
    }
}
