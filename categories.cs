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
    public partial class categories : Form
    {
        string connectionString = "server=localhost;user=root;password=Corgi_1101;database=robielprincecafe_db;";
        public categories()
        {
            InitializeComponent();
        }

        private void category_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void displaybtn_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT category_id, category_name FROM categories";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    category.DataSource = table; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
