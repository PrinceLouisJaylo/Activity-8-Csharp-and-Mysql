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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RobielPrinceCafe
{
    public partial class menu_item : Form
    {
        string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
        public menu_item()
        {
            InitializeComponent();
        }

        private void create_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO menu_items (category_id, name, price) VALUES (@category_id, @name, @price)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@category_id", category1.Text);
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@price", price.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Menu item added successfully!");
          
            }
        }

        private void read_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM menu_items WHERE menu_item_id = @menu_item_id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@menu_item_id", menu1.Text); // 'id' is the textbox for ID input

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category1.Text = reader["category_id"].ToString();
                            name.Text = reader["name"].ToString();
                            price.Text = reader["price"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Menu item not found.");
                        }
                    }
                }
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE menu_items SET category_id = @category_id, name = @name, price = @price WHERE menu_item_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", menu1.Text);
                cmd.Parameters.AddWithValue("@category_id", category1.Text);
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@price", price.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Menu item updated.");
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM menu_items WHERE menu_item_id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@menu_item_id", menu1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Menu item deleted.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                menu1.Text = row.Cells["menu_item_id"].Value.ToString();
                category1.Text = row.Cells["category_id"].Value.ToString();
                name.Text = row.Cells["name"].Value.ToString();
                price.Text = row.Cells["price"].Value.ToString();
            }
        }
    }
}
