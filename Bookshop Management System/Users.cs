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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            loadDatagridView();
        }

        SqlConnection con = new SqlConnection(@"Data Source=ISURU;Initial Catalog=bookShopDB;Integrated Security=True");

        private void loadDatagridView()
        {
            con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvUserList.DataSource = ds.Tables[0];
            con.Close();
        }        

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtUserName.Text == "" || txtPhone.Text == "" ||  txtAddress.Text == "" || txtPassword.Text == "")
            {

                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into UserTbl values('" + txtUserName.Text + "','" + txtPhone.Text + "','" + txtAddress.Text + "','" + txtPassword.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved Successfully");
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

        private void reset()
        {
            txtUserName.Text = "";
            txtPhone.Text = "";           
            txtAddress.Text = "";
            txtPassword.Text = "";
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
            loadDatagridView();
        }

        int key;

        private void dgvUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtUserName.Text = dgvUserList.SelectedRows[0].Cells[1].Value.ToString();
            txtPhone.Text = dgvUserList.SelectedRows[0].Cells[2].Value.ToString();
            txtAddress.Text = dgvUserList.SelectedRows[0].Cells[3].Value.ToString();
            txtPassword.Text = dgvUserList.SelectedRows[0].Cells[4].Value.ToString();

            if (txtUserName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(dgvUserList.SelectedRows[0].Cells[0].Value.ToString());
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
                    string query = "delete from UserTbl where UId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully");
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
            if (txtUserName.Text == "" || txtPhone.Text == "" || txtAddress.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "update UserTbl set UName='" + txtUserName.Text + "',UPhone='" + txtPhone.Text + "',UAdd='" + txtAddress.Text + "',UPass='" + txtPassword.Text + "' where UId=" + key + ";";
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

