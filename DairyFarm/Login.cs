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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\DairyFarm\DataBase\DairyFarmDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void ResetLbl_Click(object sender, EventArgs e)
        {
            UNameTb.Text = "";
            PasswordTb.Text = "";
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter The Username And Password");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {
                    if (RoleCb.SelectedItem.ToString() == "Admin")

                    {
                        if (PasswordTb.Text == "Admin" && UNameTb.Text == "Admin")
                        {
                            Employee ob = new Employee();
                            ob.Show();

                            this.Hide();
                        }
                        else
                        {

                            MessageBox.Show("Wrong Username or Password");
                        }
                    }
                    else 
                    {
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTbl where EmpName= '" + UNameTb.Text + "' and Password='" + PasswordTb.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Cows ob = new Cows();
                            ob.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Username or Password");
                        }
                        Con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select a Role");
                }
            }
        }
    }
}
