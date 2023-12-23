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

namespace PharmacyStock.Screens
{
    public partial class transaction : Form
    {
        PharmacyStockEntities2 db=new PharmacyStockEntities2();

        public transaction()
        {
            InitializeComponent();
            dataGridView2.DataSource = db.PurchasesBills.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pharmacyStockDataSet21.purchesBillDetails' table. You can move, or remove it, as needed.
            this.purchesBillDetailsTableAdapter3.Fill(this.pharmacyStockDataSet21.purchesBillDetails);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet20.PurchasesBill' table. You can move, or remove it, as needed.
            this.purchasesBillTableAdapter1.Fill(this.pharmacyStockDataSet20.PurchasesBill);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet18.purchesBillDetails' table. You can move, or remove it, as needed.
            this.purchesBillDetailsTableAdapter2.Fill(this.pharmacyStockDataSet18.purchesBillDetails);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet11.purchesBillDetails' table. You can move, or remove it, as needed.
            //this.purchesBillDetailsTableAdapter1.Fill(this.pharmacyStockDataSet11.purchesBillDetails);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet7.purchesBillDetails' table. You can move, or remove it, as needed.
            //this.purchesBillDetailsTableAdapter.Fill(this.pharmacyStockDataSet7.purchesBillDetails);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet6.PurchasesBill' table. You can move, or remove it, as needed.
            //this.purchasesBillTableAdapter.Fill(this.pharmacyStockDataSet6.PurchasesBill);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        int id = 0;
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            id = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var rr = db.PurchasesBills.SingleOrDefault(x => x.ID== id);
            dataGridView1.DataSource = rr.purchesBillDetails.ToList();
            textBox5.Text = rr.ID.ToString();
            textBox2.Text = rr.Supplier.Name;
            textBox3.Text = rr.employee.Name;
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
            this.Close();
            Thread th = new Thread(openformProc);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void openformProc()
        {
            Application.Run(new ProcBill());
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
