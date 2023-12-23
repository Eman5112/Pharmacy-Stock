using PharmacyStock.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PharmacyStock.Screens
{
    public partial class EditSupplier : Form
    {
        PharmacyStockEntities2 db = new PharmacyStockEntities2();
        int id = 0;
        
        public EditSupplier()
        {
            InitializeComponent();
            //dataGridView1.DataSource = db.Suppliers.ToList();
        }
        private void LoadSup()
        {
            var sup = (from supp in db.Suppliers
                         select supp).ToList();

            dataGridView1.DataSource = sup;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Suppliers.Where(x => x.Name.Contains(textBox1.Text)).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var r = db.Suppliers.FirstOrDefault(x => x.ID == id);
                string name=textBox5.Text.ToString();
                if (IsValidName(name) == true)
                {
                    r.Name = name;
                }
                else
                {
                    MessageBox.Show("The name is not valid.\nThis should contain only letters.");
                }

                string email = textBox3.Text.ToString();
                if (IsValidEmail(email) == true)
                {
                    r.Email = email;
                }
                else
                {
                    MessageBox.Show("The email is not valid.");
                }

                r.ID = int.Parse(textBox2.Text.ToString());
                r.Addres = textBox4.Text.ToString();
                string phone=textBox7.Text.ToString();
                if (IsValidPhone(phone) == true)
                {
                    r.Phone = phone;
                }
                else
                {
                    MessageBox.Show("The phone number is not valid.\nThis should contain only number and begin with 01");
                }
                
                db.SaveChanges();
                dataGridView1.DataSource = db.Suppliers.ToList();
                MessageBox.Show("Modified successfully");
            }
            catch 
            {
                MessageBox.Show("Something is incorrect.");
            }
        }
        public bool IsValidEmail(string email)
        {
            string pattern = @"[a-zA-Z0-9\._%+-]+@[a-zA-Z0-9.-]+";
            return Regex.IsMatch(email, pattern);
        }
        public bool IsValidName(string name)
        {
            string pattern = @"[a-zA-Z]";
            return Regex.IsMatch(name, pattern);
        }
        public bool IsValidPhone(string phone)
        {
            string pattern = @"^01\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Suppliers.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (user.possition == "manager")
            {
                this.Close();
                Thread th = new Thread(openformEditSupplier);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
            {
                MessageBox.Show("Only Manager can access .");
            }

        }
        void openformEditSupplier()
        {
            Application.Run(new Insert_New_Supplier());
        }
        private void button4_Click(object sender, EventArgs e)
        {
           // var ss = dataGridView1.SelectedRows.Cast<DataGridViewRow>().Select(row=>row.DataBoundItem).FirstOrDefault() ;
            var d = MessageBox.Show("Are you sure you deleted this User?", "Confirm deletion", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                var selectedd = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                  .Select(row => row.DataBoundItem as Supplier)
                    .FirstOrDefault();

                if (selectedd != null)
                {
                    db.Suppliers.Remove(selectedd);
                    db.SaveChanges();

                    // Refresh the DataGridView
                    LoadSup();
                }
                /*
                //int row = dataGridView1.SelectedRows[0].Index;
                var r = db.Suppliers.Find(id);
                db.Suppliers.Remove(r);
                db.SaveChanges();
                //dataGridView1.Rows.RemoveAt(r);
                dataGridView1.Refresh();
                dataGridView1.DataSource = db.Suppliers.ToList();*/
                MessageBox.Show("Deleted");
                
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //var r = db.Suppliers.SingleOrDefault(x => x.SupID == id);
            try
            {
                if (dataGridView1.CurrentRow == null)
                    return;

                var selectedRow = dataGridView1.CurrentRow;
                textBox5.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[0].Value.ToString();
                textBox3.Text = selectedRow.Cells[4].Value.ToString();
                textBox4.Text = selectedRow.Cells[2].Value.ToString();
                textBox7.Text = selectedRow.Cells[3].Value.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(openformHome);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void openformHome()
        {
            Application.Run(new Home());
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void EditSupplier_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pharmacyStockDataSet26.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter1.Fill(this.pharmacyStockDataSet26.Suppliers);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet17.Suppliers' table. You can move, or remove it, as needed.
            // this.suppliersTableAdapter1.Fill(this.pharmacyStockDataSet17.Suppliers);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet5.Suppliers' table. You can move, or remove it, as needed.
            //this.suppliersTableAdapter.Fill(this.pharmacyStockDataSet5.Suppliers);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
