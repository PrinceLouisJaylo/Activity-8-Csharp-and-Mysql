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
    public partial class orders_items : Form
    {
        string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
        public orders_items()
        {
            InitializeComponent();
        }

        private void create_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO order_items (menu_item_id, quantity, subtotal) VALUES (@menu_item_id, @quantity, @subtotal)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@menu_item_id", 1);
                    cmd.Parameters.AddWithValue("@quantity", int.Parse(quantity.Text));
                    cmd.Parameters.AddWithValue("@subtotal", decimal.Parse(subtotal.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record inserted!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void read_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT order_items_id, menu_item_id, quantity, subtotal FROM order_items";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE order_items SET menu_item_id = @menu_item_id, quantity = @quantity, subtotal = @subtotal WHERE order_items_id = @order_items_id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@menu_item_id", int.Parse(menu.Text));
                    cmd.Parameters.AddWithValue("@quantity", int.Parse(quantity.Text));
                    cmd.Parameters.AddWithValue("@subtotal", decimal.Parse(subtotal.Text));
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        MessageBox.Show("Record updated!");
                    else
                        MessageBox.Show("No record found with the provided ID.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM order_items WHERE order_items_id = @order_items_id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@order_items_id", int.Parse(order.Text));
                    int rowsDeleted = cmd.ExecuteNonQuery();

                    if (rowsDeleted > 0)
                        MessageBox.Show("Record deleted!");
                    else
                        MessageBox.Show("No record found with the provided ID.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void show_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM order_items";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void menu_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
