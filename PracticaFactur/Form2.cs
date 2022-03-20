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

namespace PracticaFactur
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection oCon = new SqlConnection("Data Source=LAPTOP-EHB7IG9Q;Initial Catalog=Factura;Integrated Security=True");
        SqlCommand cmd1;
        SqlCommand cmd2;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            oCon.Open();
            DataTable dt = new DataTable();
            cmd1 = new SqlCommand($"select * from Ventas where IdVenta = '{id}'", oCon);

            SqlDataAdapter dr = new SqlDataAdapter(cmd1);
            dr.Fill(dt);

            DataTable dt2 = new DataTable();
            cmd2 = new SqlCommand($"select * from ProductoVenta where IdVenta = '{id}'", oCon);

            dr = new SqlDataAdapter(cmd2);
            dr.Fill(dt2);

            oCon.Close();

            CrystalReport1 cr1 = new CrystalReport1();
            cr1.Database.Tables["Ventas"].SetDataSource(dt);
            cr1.Database.Tables["ProductoVenta"].SetDataSource(dt2);
            this.crystalReportViewer1.ReportSource = cr1;
        }
    }
}
