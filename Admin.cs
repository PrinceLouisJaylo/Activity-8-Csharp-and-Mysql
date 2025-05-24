using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RobielPrinceCafe
{

    public partial class Admin : Form
    {
        string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            LoadAdmins();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO admin (Username, Password, FullName, Email) VALUES (@Username, @Password, @FullName, @Email)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username.Text);
                    cmd.Parameters.AddWithValue("@Password", password.Text);
                    cmd.Parameters.AddWithValue("@FullName", fullname.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Admin created.");
                LoadAdmins();
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM admin WHERE Admin_id = @Admin_id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Admin_id", id.Text);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username.Text = reader["Username"].ToString();
                            password.Text = reader["Password"].ToString();
                            fullname.Text = reader["FullName"].ToString();
                            email.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Admin not found.");
                        }
                    }
                }
            }

            // Load full admin list in DataGridView after reading a single admin
            LoadAdmins();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE admin SET Username = @Username, Password = @Password, FullName = @FullName, Email = @Email WHERE Admin_id = @Admin_id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username.Text);
                    cmd.Parameters.AddWithValue("@Password", password.Text);
                    cmd.Parameters.AddWithValue("@FullName", fullname.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.Parameters.AddWithValue("@Admin_id", id.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Admin updated.");
                LoadAdmins();
            }
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM admin WHERE Admin_id = @Admin_id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Admin_id", id.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Admin deleted.");
                LoadAdmins();
            }
        }

        
        private void LoadAdmins()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Admin_id, Username, FullName, Email FROM admin";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells["Admin_id"].Value.ToString();
                username.Text = row.Cells["Username"].Value.ToString();
                fullname.Text = row.Cells["FullName"].Value.ToString();
                email.Text = row.Cells["Email"].Value.ToString();
            }
        }

    }
}