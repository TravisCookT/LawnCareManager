using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace CustomerDashboard
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            txtNewFirstname.Text = "";
            txtNewLastname.Text = "";
            txtNewUsername.Text = "";
            txtNewPassword.Password = "";
            txtNewUsername.Text = "";
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if(txtNewUserEmail.Text.Length == 0)
            {
                txtNewUserEmail.Foreground = Brushes.Red;

            }
            else if (!Regex.IsMatch(txtNewUserEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtNewUserEmail.Foreground = Brushes.Red;
                txtNewUserEmail.Select(0, txtNewUserEmail.Text.Length);
            }
            else
            {
                string username = txtNewUsername.Text;
                string firstname = txtNewFirstname.Text;
                string lastname = txtNewLastname.Text;
                string email = txtNewUserEmail.Text;
                string password = txtNewPassword.Password;
                if(txtNewPassword.Password.Length == 0)
                {
                    txtNewPassword.Foreground = Brushes.Red;
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LawnCareDb;Integrated Security=True;" +
                        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into dbo.tblUser (UserId,UserFirstName,UserLastName,UserEmail,UserPassword) values('" + username + "','" + firstname + "','" + lastname + "','" + email + "','" + password + "')", con);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Reset();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }
    }
}
