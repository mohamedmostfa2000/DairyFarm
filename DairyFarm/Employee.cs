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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            populate();
            Clear();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DairyFarm\DataBase\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void populate()
        {
            
            Con.Open();
            string query = "select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }


        private void Clear()
        {
            NameTb.Text = "";
            PhoneTb.Text = ""; 
            AddressTb.Text = "";
            GenCb.SelectedIndex = -1;
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }

        private void Employee_Load_1(object sender, EventArgs e)
        {

        }
    }
}
