using PharmacyStock.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Security.Cryptography;
using BCrypt.Net;


namespace PharmacyStock.Screens
{
    public partial class Insert_New_Employee : Form
    {
        PharmacyStockEntities2 db = new PharmacyStockEntities2();
        string imagePath="";
        public Insert_New_Employee()
        {
            InitializeComponent();
            
        }

        private void Insert_New_Employee_Load(object sender, EventArgs e)
        {
            
        }
        
       
       


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                employee emp = new employee();
                string name = textBox1.Text;
                if (IsValidName(name) == true)
                {
                    emp.Name = name;
                }
                else
                {
                    MessageBox.Show("The name is not valid.\nThis should contain only letters.");
                }
            string password = textBox2.Text;
            string hashedPassword = HashPassword(password);
            emp.Password = hashedPassword;
            //emp.Password = textBox2.Text;

            string NID = textBox3.Text;
                if (IsValidNID(NID) == true)
                {
                    emp.NationalID = NID;
                }
                else
                {
                    MessageBox.Show("The national number is not valid.\nThis should contain 14 number.");
                }

                emp.Address = textBox4.Text;
                emp.position = comboBox1.Text.ToString();
                emp.DateOfBirth = dateTimePicker1.Value.Date;
                string email = textBox5.Text;
                if (IsValidEmail(email) == true)
                {
                    emp.Email = email;
                }
                else
                {
                    MessageBox.Show("The email is not valid.");
                }

                string phone = textBox6.Text;
                
                if (IsValidPhone(phone) == true)
                {
                    emp.phone = phone;
                }
                else
                {
                    MessageBox.Show("The phone number is not valid.\nThis should contain only number and begin with 01");
                }

                emp.image = imagePath;


                if (imagePath != "")
                {

                    string newPath = Environment.CurrentDirectory + "\\images\\Users\\" + emp.NationalID + ".jpg";
                    File.Copy(imagePath, newPath);
                    emp.image = imagePath;

                }
                db.employees.Add(emp);
                db.SaveChanges();

                MessageBox.Show("Done");
              //  this.Close();
               textBox1.Text = string.Empty;
               textBox2.Text = string.Empty;
                textBox3.Text= string.Empty;
                textBox4.Text= string.Empty;
                textBox5.Text= string.Empty;
                textBox6.Text = string.Empty;
                comboBox1.SelectedIndex = -1;
                pictureBox1.Image = null ;
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
        void openform()
        {
            Application.Run(new EditEmployee());
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op= new OpenFileDialog();
            if(op.ShowDialog() == DialogResult.OK)
            {
                imagePath = op.FileName;
                pictureBox1.Image = Image.FromFile(imagePath);
            }     
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(openformHome);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private string EncryptPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for SHA256
                return Convert.ToBase64String(hash);
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32]; // You can adjust the salt size based on your requirements
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }
            return salt;
        }

        void openformHome()
        {
            Application.Run(new Home());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(openformEditEmp);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void openformEditEmp()
        {
            Application.Run(new EditEmployee());
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
