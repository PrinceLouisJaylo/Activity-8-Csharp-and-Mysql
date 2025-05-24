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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin adminForm = new Admin();  
            adminForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            categories categoriesForm = new categories();  
            categoriesForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            orders_items orders_itemsForm = new orders_items();
            orders_itemsForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            menu_item menuItemsForm = new menu_item(); // use correct class and variable name
            menuItemsForm.Show(); // use same variable here
        }
    }
}
