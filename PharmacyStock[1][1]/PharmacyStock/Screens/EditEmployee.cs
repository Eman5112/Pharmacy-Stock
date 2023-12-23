using PharmacyStock.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyStock.Screens
{
    public partial class EditEmployee : Form
    {
        PharmacyStockEntities2 db = new PharmacyStockEntities2();
        string imagePath="0";
        string NID = "";
        DB.employee r;
        
        public EditEmployee()
        {

            InitializeComponent();
            textBox5.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty; 
            textBox4.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
            
            textBox7.Text = string.Empty;
            textBox9.Text = string.Empty;
            pictureBox1.ImageLocation = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Loademp()
        {

            var emps = (from s in db.employees
                       select s).ToList();

            dataGridView1.DataSource = emps;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.employees.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.employees.Where(x => x.Name.Contains(textBox1.Text)).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(openformInsertUser);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void openformInsertUser()
        {
            Application.Run(new Insert_New_Employee());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                NID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                r = db.employees.SingleOrDefault(x => x.NationalID == NID);
            string name = textBox5.Text;
            if (IsValidName(name) == true)
            {
                r.Name = name;
            }
            else
            {
                MessageBox.Show("The name is not valid.\nThis should contain only letters.");
            }
            string email=textBox3.Text.ToString();
           
            if (IsValidEmail(email) == true)
            {
                r.Email = email;
            }
            else
            {
                MessageBox.Show("The email is not valid.");
            }
            string nid=textBox2.Text.ToString();
            if (IsValidNID(nid) == true)
            {
                r.NationalID = nid;
            }
            else
            {
                MessageBox.Show("The national number is not valid.\nThis should contain 14 number.");
            }
           
                r.Address = textBox4.Text.ToString();
                string phone1 = textBox7.Text.ToString();
               
            if (IsValidPhone(phone1) == true)
            {
                r.phone = phone1;
            }
            else
            {
                MessageBox.Show("The phone number is not valid.\nThis should contain only number and begin with 01");
            }
            r.DateOfBirth = dateTimePicker1.Value.Date;
                r.position=textBox9.Text.ToString();
           
                if (imagePath != "0")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\Users\\" + r.NationalID + ".jpg";
                    File.Copy(imagePath, newPath, true);
                    r.image = newPath;
                }
                else
                {
                    imagePath = r.image;
                    r.image = imagePath;
                }
                db.SaveChanges();
                dataGridView1.DataSource = db.employees.ToList();
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
        public bool IsValidNID(string NID)
        {
            string pattern = @"^\d{14}$"; ;
            return Regex.IsMatch(NID, pattern);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var d = MessageBox.Show("Are you sure you deleted this User?", "Confirm deletion", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                try
                {
                    var selectedd = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                        .Select(row => row.DataBoundItem as employee)
                        .FirstOrDefault();

                    if (selectedd != null)
                    {
                        db.employees.Remove(selectedd);
                        db.SaveChanges();

                        // Refresh the DataGridView
                        Loademp();
                    }
                    MessageBox.Show("Deleted");
                }
                catch
                {
                    MessageBox.Show("Can not delete this employee.");
                }
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                imagePath = op.FileName;
                pictureBox1.Image = Image.FromFile(imagePath);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow == null)
                    return;

                var selectedRow = dataGridView1.CurrentRow;
                textBox5.Text = selectedRow.Cells[0].Value.ToString();
                
                 textBox2.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[3].Value.ToString();
                textBox4.Text = selectedRow.Cells[5].Value.ToString();
                dateTimePicker1.Value= Convert.ToDateTime(selectedRow.Cells[4].Value);
                textBox7.Text = selectedRow.Cells[2].Value.ToString();
                textBox9.Text = selectedRow.Cells[6].Value.ToString();
                pictureBox1.ImageLocation = selectedRow.Cells[8].Value.ToString();
            }
            catch { }
                       
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditEmployee_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pharmacyStockDataSet24.employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter1.Fill(this.pharmacyStockDataSet24.employees);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet12.employees' table. You can move, or remove it, as needed.
            //this.employeesTableAdapter1.Fill(this.pharmacyStockDataSet12.employees);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet3.employees' table. You can move, or remove it, as needed.
            //this.employeesTableAdapter.Fill(this.pharmacyStockDataSet3.employees);
            dataGridView1.DefaultCellStyle.BackColor= Color.White;  
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
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

      

        //private void button4_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void button3_Click_1(object sender, EventArgs e)
        //{

        //}

        //private void button5_Click_1(object sender, EventArgs e)
        //{

        //}
    }
}
