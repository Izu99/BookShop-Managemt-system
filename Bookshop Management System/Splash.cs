using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookshop_Management_System
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int starpos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            starpos += 1;
            MyProgress.Value = starpos;
            Percentagelbl.Text = starpos +"%";
            if (MyProgress.Value == 100) {
                MyProgress.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }
        int starpos1 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {            
            starpos1 += 1;
            guna2CircleProgressBar1.Value = starpos1;
            if (guna2CircleProgressBar1.Value == 100)
            {
               
            }
        }
    }
}
