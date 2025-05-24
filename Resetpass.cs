using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RobielPrinceCafe
{
    public partial class Resetpass : Form
    {
        string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
        public Resetpass()
        {
            InitializeComponent();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            if (string.IsNullOrEmpty(username)) return;

            string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM admin WHERE Username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        label2.Text = "✅ Username found";
                        label2.ForeColor = Color.Green;
                    }
                    else
                    {
                        label2.Text = "❌ Username not found";
                        label2.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    label2.Text = "⚠️ Error checking username.";
                    label2.ForeColor = Color.OrangeRed;
                }
            }
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {
            string password = txtpassword.Text;
            if (password.Length < 6)
            {
                label3.Text = "❌ Too short (min 6 chars)";
                label3.ForeColor = Color.Red;
            }
            else if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                label3.Text = "⚠️ Weak (use numbers and uppercase)";
                label3.ForeColor = Color.Orange;
            }
            else
            {
                label3.Text = "✅ Strong password";
                label3.ForeColor = Color.Green;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string newPassword = txtpassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE admin SET Password = @password WHERE Username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", newPassword); // Use hashed password in real-world apps

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password reset successful. You may now log in.");
                        LogIn loginForm = new LogIn(); // Open login form
                        loginForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username not found. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
