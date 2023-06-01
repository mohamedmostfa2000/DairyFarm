using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DairyFarm
{
    public partial class Finance : Form
    {
        public Finance()
        {
            InitializeComponent();
           
        }

       


     

        private void label9_Click(object sender, EventArgs e)
        {

            Cows ob = new Cows();
            ob.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

            MilkProduction ob = new MilkProduction();
            ob.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {

            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {

            Breedings ob = new Breedings();
            ob.Show();
            this.Hide();
        }

        private void label21_Click(object sender, EventArgs e)
        {

            MilkSales ob = new MilkSales();
            ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

            DashBoard ob = new DashBoard();
            ob.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
