using PharmacyStock.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PharmacyStock.Screens
{
    public partial class SaleBill : Form
    {
        PharmacyStockEntities2 db=new PharmacyStockEntities2();
        
        public SaleBill()
        {
            InitializeComponent();
            textBox4.Text = user.name;

            // comboBox1.DataSource = db.Products.ToList();
            //var r = db.Products.ToList();
            //comboBox1.DisplayMember = r.PName;
            //comboBox1.ValueMember = "Pcode";
            // comboBox1.SelectedValue = "Pcode";    
            textBox6.Text = "0";
            textBox3.Text = "0";  
            textBox2.Text = "0";
        }

        private void SaleBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pharmacyStockDataSet13.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter1.Fill(this.pharmacyStockDataSet13.Products);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter.Fill(this.pharmacyStockDataSet.Products);
            comboBox1.SelectedIndex = -1;
            
            //dataGridView1.DataSource = db.Products.ToList();
        }
        int? id;
        string namee;
        private void button1_Click(object sender, EventArgs e)
        {
            float TotalPrice = 0;
                      
            try
            {
                namee= (comboBox1.SelectedValue.ToString());
                if (namee != null)
                {
                    var r = db.Products.SingleOrDefault(x => x.Name == namee);
                    int quantity = int.Parse(textBox5.Text);
                    int qnt = r.Quantity - quantity;
                    if (qnt > 0)
                    {
                        r.Quantity = r.Quantity - quantity;
                        textBox6.Text = (float.Parse(textBox6.Text)+(quantity * r.Price)).ToString();
                        textBox3.Text = textBox6.Text;
                        dataGridView1.Rows.Add(r.Name, r.code, r.Price, quantity, float.Parse(textBox5.Text)*r.Price);
                    }
                    else
                    {
                        MessageBox.Show("The Quantity of this Product is not enough\nAvailable Quantity is "+r.Quantity);
                    }
                    
                    db.SaveChanges();
                }
               
            }
            catch { }
        }
        int i ;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
                i = int.Parse(comboBox1.SelectedIndex.ToString());
                var r = db.Products.SingleOrDefault(x => x.code == i);
            

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SalesBill s=new SalesBill();
                s.EmployeeNationalID = user.ID;
                s.SaleDate = dateTimePicker1.Value.Date;
                s.TatalPriceBeforeDiscount = float.Parse(textBox6.Text);
                s.discount=float.Parse(textBox2.Text);
                s.TotalPriceAfterDiscount = float.Parse(textBox3.Text);
               
                for(int i=0;i<dataGridView1.Rows.Count-1;i++) 
                {
                    SaleBillDetail ss = new SaleBillDetail();
                    ss.SaleBillID = s.ID;
                    for (int j = 0; j <= 4; j++)
                    {
                        var x = dataGridView1.Rows[i].Cells[j].Value;
                        if (j == 0)
                        {
                            ss.ProductName = x.ToString();
                        }
                        if (j == 1)
                        {
                            ss.ProductCode = int.Parse(x.ToString());
                        }
                        if (j == 2)
                        {
                            ss.ProductPrice = float.Parse(x.ToString());
                        }
                        if (j == 3)
                        {
                            ss.Quantity = int.Parse(x.ToString());
                        }
                        if (j == 4)
                        {
                            ss.TotalPrice = float.Parse(x.ToString());
                        }
                    }
                    db.SaleBillDetails.Add(ss);
                    
                }
                
                db.SalesBills.Add(s);
                
                db.SaveChanges();
                textBox6.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox2.Text= string.Empty;
                comboBox1.SelectedIndex = -1;
                dataGridView1.Rows.Clear();
                MessageBox.Show("Done");


        }
            catch {
                MessageBox.Show("Something is incorrect.");
            }
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

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "0")
            {
                float discount = float.Parse(textBox2.Text);
                float discount2 = (100 - discount) / 100;
                textBox3.Text = (discount2 * float.Parse(textBox6.Text)).ToString();
            }

            //dataGridView1.Rows.Add(r.PName, r.Pcode, r.PPrice, quantity, float.Parse(textBox3.Text) * r.PPrice);

        }
    }
}
