using PharmacyStock.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyStock.Screens
{
    public partial class ListSaleBill : Form
    {
        PharmacyStockEntities2 db=new PharmacyStockEntities2();
        public ListSaleBill()
        {
            InitializeComponent();
            dataGridView2.DataSource=db.SalesBills.ToList();
            //dataGridView1.DataSource=db.SaleBillDetails.ToList();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int id = 0;
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            id = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var rr = db.SalesBills.SingleOrDefault(x => x.ID== id);
            dataGridView1.DataSource = rr.SaleBillDetails.ToList();
            textBox1.Text=rr.ID.ToString();
            textBox4.Text = rr.employee.Name;

        }

        private void ListSaleBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pharmacyStockDataSet23.SaleBillDetails' table. You can move, or remove it, as needed.
            this.saleBillDetailsTableAdapter2.Fill(this.pharmacyStockDataSet23.SaleBillDetails);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet22.SalesBill' table. You can move, or remove it, as needed.
            this.salesBillTableAdapter1.Fill(this.pharmacyStockDataSet22.SalesBill);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet10.SaleBillDetails' table. You can move, or remove it, as needed.
           // this.saleBillDetailsTableAdapter1.Fill(this.pharmacyStockDataSet10.SaleBillDetails);
            // TODO: This line of code loads data into the 'pharmacyStockDataSet8.SalesBill' table. You can move, or remove it, as needed.
            //this.salesBillTableAdapter.Fill(this.pharmacyStockDataSet8.SalesBill);

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
            Thread th = new Thread(openformSale);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        void openformSale()
        {
            Application.Run(new SaleBill());
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
