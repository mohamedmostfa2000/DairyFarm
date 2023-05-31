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
    public partial class CowHealth : Form
    {
        public CowHealth()
        {
            InitializeComponent();
            FillCowId();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DairyFarm\DataBase\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");


        private void FillCowId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CowId from CowTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            CowIdCb.ValueMember = "CowId";
            CowIdCb.DataSource = dt;
            Con.Close();
        }


        private void populate()
        {
            
            Con.Open();
            string query = "select * from HealthTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            HealthDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetCowName()
        {
            Con.Open();
            string query = "select * from CowTbl where CowId =" + CowIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CowNameTb.Text = dr["CowName"].ToString();
            }
            Con.Close();
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

        private void label18_Click(object sender, EventArgs e)
        {
            Finance ob = new Finance();
            ob.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            DashBoard ob = new DashBoard();
            ob.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || VetNameTb.Text == "" || DiagnosisTb.Text == "" || TreatmentTb.Text == "")
            {
                MessageBox.Show("Missing Data!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into HealthTbl values ('" + CowIdCb.SelectedValue.ToString() + "','" + CowNameTb.Text + "','" + Date.Value.Date + "','" + EventTb.Text + "'," + DiagnosisTb.Text + "," + TreatmentTb.Text + ",'" + CostTb.Text + ",'" + VetNameTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Health issue Saved Successfully");
                    Con.Close();
                    populate();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        int key = 0;
        private void HealthDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CowIdCb.SelectedValue = HealthDGV.SelectedRows[0].Cells[1].Value.ToString();
            CowNameTb.Text = HealthDGV.SelectedRows[0].Cells[2].Value.ToString();
            Date.Text = HealthDGV.SelectedRows[0].Cells[3].Value.ToString();
            EventTb.Text = HealthDGV.SelectedRows[0].Cells[4].Value.ToString();
            DiagnosisTb.Text = HealthDGV.SelectedRows[0].Cells[5].Value.ToString();
            TreatmentTb.Text = HealthDGV.SelectedRows[0].Cells[6].Value.ToString();
            CostTb.Text = HealthDGV.SelectedRows[0].Cells[7].Value.ToString();
            VetNameTb.Text = HealthDGV.SelectedRows[0].Cells[8].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HealthDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
