﻿using System;
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
            populateExp();
            ClearExp();
            populateInc();
            ClearInc();
            FillEmpId();



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
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DairyFarm\DataBase\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FillEmpId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select EmpId from EmployeeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            EmpIdCb.ValueMember = "EmpId";
            EmpIdCb.DataSource = dt;
            Con.Close();
        }

        private void SaveExpBtn_Click(object sender, EventArgs e)
        {
            if (ExpPurpCb.SelectedIndex == -1 || ExpAmountTb.Text == "" || EmpIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Data!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into ExpenditureTbl values ('" + ExpDate.Value.Date + "','" + ExpPurpCb.SelectedItem.ToString() + "','" + ExpAmountTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    populateExp();
                    ClearExp();
                    MessageBox.Show("Expenditure Saved Successfully");



                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        private void populateExp()
        {
            
            Con.Open();
            string query = "select * from ExpenditureTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ClearExp()
        {
            ExpAmountTb.Text = "";
            ExpPurpCb.SelectedIndex = -1;
        }

        private void FilterExp()
        {
            Con.Open();
            string query = "select * from ExpenditureTbl where ExpDate = '" + ExpFilter.Value.Date + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void SaveIncBtn_Click(object sender, EventArgs e)
        {
            if (IncPurpCb.SelectedIndex == -1 || IncAmountTb.Text == "" || EmpIdCb.SelectedIndex==-1 )
            {
                MessageBox.Show("Missing Data!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into IncomeTbl values ('" + IncDate.Value.Date + "','" + IncPurpCb.SelectedItem.ToString() + "','" + IncAmountTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    populateInc();
                    ClearInc();
                    MessageBox.Show("Income Saved Successfully");



                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void populateInc()
        {
            
            Con.Open();
            string query = "select * from IncomeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ClearInc()
        {
            IncAmountTb.Text = "";
            IncPurpCb.SelectedIndex = -1;
        }

        private void FilterInc()
        {
            Con.Open();
            string query = "select * from IncomeTbl where IncDate = '" + IncFilter.Value.Date + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void IncFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterInc();
        }

        private void ExpFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterExp();
        }

        private void ReloadExp_Click(object sender, EventArgs e)
        {
            populateExp();
        }

        private void ReloadInc_Click(object sender, EventArgs e)
        {
            populateInc();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
