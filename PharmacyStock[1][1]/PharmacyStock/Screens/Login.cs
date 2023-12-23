using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using PharmacyStock.Screens;
using PharmacyStock.DB;
using System.Threading;
using System.Data.Entity;
using System.Security.Cryptography;

namespace PharmacyStock
{
    
    public partial class Login : Form
    {
        PharmacyStockEntities2 db=new PharmacyStockEntities2();
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string enteredUsername = textBox1.Text;
                var input = db.employees.FirstOrDefault(x => x.Name == enteredUsername );
                string enteredPassword = textBox2.Text;

                if (input != null)
                {
                    var hashedPass = input.Password;

                    bool isValid = VerifyPassword(enteredPassword, hashedPass);
                    if (isValid)
                    {
                        this.Close();
                        Thread th = new Thread(openform);
                        th.SetApartmentState(ApartmentState.STA);
                        th.Start();
                        user.name = input.Name;
                        user.ID = input.NationalID;
                        user.image = input.image;
                        user.possition = input.position;
                        user.email = input.Email;
                        user.phone = input.phone;
                    }
                    else
                    {
                        MessageBox.Show(" Password is incorrect.");
                    }
                }
                else
                {
                    MessageBox.Show("User is not found.");
                }
            }
            catch {
                MessageBox.Show("something wrong");
            }

        }
        void openform()
        {
            Application.Run(new Home());
        }
        
        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        //private string EncryptPassword(string password, byte[] salt)
        //{
        //    using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        //    {
        //        byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for SHA256
        //        return Convert.ToBase64String(hash);
        //    }
        //}
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }


    static class user
    {
        static public string name { get; set; }
        static public string ID { get; set; }
        static public string image { get; set; }
        static public string possition { get; set; }
        static public string email { get; set; }
        static public string phone { get; set; }
    }
}
