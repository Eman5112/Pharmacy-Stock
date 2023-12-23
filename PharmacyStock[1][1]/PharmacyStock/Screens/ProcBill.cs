using PharmacyStock.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PharmacyStock.Screens
{
    public partial class ProcBill : Form
    {
        PharmacyStockEntities2 db=new PharmacyStockEntities2();

        public ProcBill()
        {
            InitializeComponent();
            textBox7.Text = user.name;
            //textBox3.Text = "0";
            //textBox5.Text = "0";
            //comboBox2.DataSource = db.Suppliers.ToList();
            //comboBox2.ValueMember = "id";
            //comboBox2.DisplayMember = "name";
            textBox3.Text = "0";
            textBox5.Text = "0";
            textBox4.Text = "0";
        }

        private void SaleBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pharmacyStockDataSet15.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter1.Fill(this.pharmacyStockDataSet15.Products);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet14.Suppliers' table. You can move, or remove it, as needed.
            this.suppliersTableAdapter1.Fill(this.pharmacyStockDataSet14.Suppliers);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet2.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.pharmacyStockDataSet2.Products);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet1.Suppliers' table. You can move, or remove it, as needed.
          //  this.suppliersTableAdapter.Fill(this.pharmacyStockDataSet1.Suppliers);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

        }
        int? i;
        int code = 0;
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PurchasesBill t = new PurchasesBill();
                string name = textBox2.Text;
                if (IsValidName(name) == true)
                {
                    t.ContactPerson = name;
                }
                else
                {
                    MessageBox.Show("The name of contact person is not valid.\nThis should contain only letters.");
                }
                t.EmployeeNationalID = user.ID;
                t.TransactionDate = dateTimePicker1.Value.Date;
                t.SupplierID = int.Parse(comboBox2.SelectedValue.ToString());
                t.TotalPriceBeforeDiscount = float.Parse(textBox3.Text);
                t.Discount = float.Parse(textBox4.Text);
                t.TotalPriceAfterDiscount = float.Parse(textBox5.Text);
                string phone = textBox8.Text;
                if (IsValidPhone(phone) == true)
                {
                    t.ContactPersonPhone = phone;
                }
                else
                {
                    MessageBox.Show("The phone number of contact person is not valid.\nThis should contain only number and begin with 01");
                }
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    purchesBillDetail pd = new purchesBillDetail();
                    pd.PurchesBillID = t.ID;
                    for (int j = 0; j <= 4; j++)
                    {
                        var x = dataGridView1.Rows[i].Cells[j].Value;
                        if (j == 0)
                        {
                            pd.ProductName = x.ToString();
                        }
                        if (j == 1)
                        {
                            pd.Productcode = int.Parse(x.ToString());
                        }
                        if (j == 2)
                        {
                            pd.ProductPrice = float.Parse(x.ToString());
                        }
                        if (j == 3)
                        {
                            pd.Quantity = int.Parse(x.ToString());
                        }
                        if (j == 4)
                        {
                            pd.TotalPrice = float.Parse(x.ToString());
                        }
                    }
                    db.purchesBillDetails.Add(pd);

                }
                db.PurchasesBills.Add(t);
                db.SaveChanges();
                MessageBox.Show("Done");
                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox6.Text = string.Empty;
                textBox8.Text = string.Empty;
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                dataGridView1.Rows.Clear();

            }
            catch
            {
                MessageBox.Show("Something is incorrect.");

            }
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
        int? id;
        string name;
        private void button2_Click(object sender, EventArgs e)
        {
            float totalPrice = 0;
            try
            {
                name = (comboBox1.SelectedValue.ToString());
                
                if (name != null)
                {
                    var r = db.Products.SingleOrDefault(x => x.Name == name);
                    int quantity = int.Parse(textBox6.Text);
                    textBox3.Text =(float.Parse(textBox3.Text) +(quantity * r.Price)).ToString();
                    textBox5.Text = textBox3.Text;
                    dataGridView1.Rows.Add(r.Name, r.code, r.Price, quantity, float.Parse(textBox6.Text) * r.Price);
                    r.Quantity = r.Quantity + quantity;
                    db.SaveChanges();
                }
            }
            catch { }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = int.Parse(comboBox1.SelectedIndex.ToString());
            var r = db.Suppliers.SingleOrDefault(x => x.ID== i);
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = int.Parse(comboBox1.SelectedIndex.ToString());
            var r = db.Products.SingleOrDefault(x => x.code == i);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "0")
            {
                float discount = float.Parse(textBox4.Text);
                float discount2 = (100 - discount) / 100;
                textBox5.Text = (discount2 * float.Parse(textBox3.Text)).ToString();
            }
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
