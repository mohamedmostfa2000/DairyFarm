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
    public partial class Breedings : Form
    {
        public Breedings()
        {
            InitializeComponent();
            FillCowId();
            populate();
            Clear();


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
            string query = "select * from BreedTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BreedDGV.DataSource = ds.Tables[0];
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
                CowAgeTb.Text = dr["Age"].ToString();
            }
            Con.Close();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Breedings_Load(object sender, EventArgs e)
        {

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
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarksTb.Text == "" || CowAgeTb.Text == "")
            {
                MessageBox.Show("Missing Data!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into BreedTbl values ('" + HeatDate.Value.Date + "','" + BreedDate.Value.Date + "','" + CowIdCb.SelectedValue.ToString() + "','" + CowNameTb.Text + "','" + PregDate.Value.Date + "','" + ExpDate.Value.Date + "','" + DateCalved.Value.Date + "','" + CowAgeTb.Text + "','" + RemarksTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    populate();
                    Clear();

                    MessageBox.Show("Breeding Report Saved Successfully");
                    Con.Close();
                    

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void Clear()
        {
            CowNameTb.Text = "";
            RemarksTb.Text = "";
            CowAgeTb.Text = "";
            key = 0;
        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BreedDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HeatDate.Text = BreedDGV.SelectedRows[0].Cells[1].Value.ToString();
            CowNameTb.Text = BreedDGV.SelectedRows[0].Cells[2].Value.ToString();
            CowIdCb.SelectedValue = BreedDGV.SelectedRows[0].Cells[3].Value.ToString();
            CowNameTb.Text = BreedDGV.SelectedRows[0].Cells[4].Value.ToString();
            PregDate.Text = BreedDGV.SelectedRows[0].Cells[5].Value.ToString();
            ExpDate.Text = BreedDGV.SelectedRows[0].Cells[6].Value.ToString();
            DateCalved.Text = BreedDGV.SelectedRows[0].Cells[7].Value.ToString();
            CowAgeTb.Text = BreedDGV.SelectedRows[0].Cells[8].Value.ToString();
            RemarksTb.Text = BreedDGV.SelectedRows[0].Cells[9].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(BreedDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarksTb.Text == "" || CowAgeTb.Text == "")
            {
                MessageBox.Show("Missing Data!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "update BreedTbl set HeatDate=" + HeatDate.Value.Date + "',BreedDate='" + BreedDate.Value.Date + "',CowId='" + CowIdCb.SelectedValue.ToString() + ",CowName='" + CowNameTb.Text + "',PregDate='" + PregDate.Value.Date + "',ExpDateCalve='" + ExpDate.Value.Date + "',DateCalved='" + DateCalved.Value.Date + ",CowAge='" + CowAgeTb.Text + ",Remarks='" + RemarksTb.Text + "' where BrId= " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    populate();
                    Clear();
                    MessageBox.Show("Breeding Updated Successfully");
                    Con.Close();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Breed Report Be Deleted!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from BreedTbl where BrId = " + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    populate();
                    Clear();
                    MessageBox.Show("Breed Deleted Successfully");
                    Con.Close();
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
    
}
