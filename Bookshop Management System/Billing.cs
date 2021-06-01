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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            loadDatagridView();

        }

        SqlConnection con = new SqlConnection(@"Data Source=ISURU;Initial Catalog=bookShopDB;Integrated Security=True");

        private void loadDatagridView()
        {
            con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dgvBooklist.DataSource = ds.Tables[0];
            con.Close();
        }

        private void updateBook()
        {
            int newQty = stock - Convert.ToInt32(txtQuantity.Text);
            try
            {
                con.Open();
                string query = "update BookTbl set BQty='" + newQty + "' where Bid=" + key + ";";
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

        int n = 0;
        int gridTotal = 0;

        private void btnAddToSave_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text == "" || Convert.ToInt32(txtQuantity.Text) > stock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int total = Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtPrice.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dgvBill);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = txtBookName.Text;
                newRow.Cells[2].Value = txtPrice.Text;
                newRow.Cells[3].Value = txtQuantity.Text;
                newRow.Cells[4].Value = total;
                dgvBill.Rows.Add(newRow);
                n++;
                updateBook();
                gridTotal = gridTotal + total;
                lblPrice.Text = "Rs." + gridTotal;
            }
        }

        int key = 0;

        int stock = 0;

        private void dgvBooklist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBookName.Text = dgvBooklist.SelectedRows[0].Cells[1].Value.ToString();
            txtPrice.Text = dgvBooklist.SelectedRows[0].Cells[5].Value.ToString();

            if (txtBookName.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(dgvBooklist.SelectedRows[0].Cells[0].Value.ToString());
                stock = Convert.ToInt32(dgvBooklist.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void reset()
        {
            txtBookName.Text = "";
            txtQuantity.Text = "";
            txtClientName.Text = "";
            txtPrice.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;
        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Book Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT  PRICE QUANTITY TOTAL", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["column4"].Value);
                tottal = Convert.ToInt32(row.Cells["column5"].Value);

                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos)); 
            pos = pos + 20;
        }
            e.Graphics.DrawString("Grand Total : Rs" + gridTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Chartreuse, new Point(60, pos+50));
            e.Graphics.DrawString("***********Book Store************" , new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Chartreuse, new Point(60, pos+85));
            dgvBill.Rows.Clear();
            dgvBill.Refresh();
            pos = 100;
            gridTotal = 0;

    
        }


    }
}
