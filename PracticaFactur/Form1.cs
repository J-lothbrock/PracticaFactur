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
    public partial class Form1 : Form
    {
        SqlConnection oCon = new SqlConnection("Data Source=LAPTOP-EHB7IG9Q;Initial Catalog=Factura;Integrated Security=True");
        SqlCommand cmd1;
        SqlCommand cmd2;

        public int IdVenta { get; set; }
        string sql1, sql2;
        const double itbis = 0.18;

        //Datos productos
        private string nombreProducto;
        private double precio;
        private int cantidad;
        private double _total;

        //Datos pagos
        private double total;       
        private double _itbis ;           
        private double subtotal ;
        private double pago;

        public Form1()
        {
            InitializeComponent();
        }


        public void RegistrarPago()
        {
            try
            {
                oCon.Open();

                double total = Convert.ToDouble(txtTotal.Text);
                double _itbis = Convert.ToDouble(txtItbis.Text);  
                double subtotal = Convert.ToDouble(txtSubTotal.Text);
                double pago = Convert.ToDouble(txtPago.Text);
                double bal = Convert.ToDouble(txtBalance.Text);

                sql1 = $"insert into Ventas (Total, Itbis, Subtotal, Pago, Balance) values ('{total}', '{_itbis}', '{subtotal}', '{pago}', '{bal}' ) select scope_identity() as IdVenta";
                cmd1 = new SqlCommand(sql1, oCon);

                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    IdVenta = Convert.ToInt32(dr["IdVenta"].ToString());
                }

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                MessageBox.Show(mensaje);
                oCon.Close();
            }
            oCon.Close();

        }

        public void RegistrarProducto()
        {
            try
            {
                oCon.Open();
                sql2 = $"insert into ProductoVenta (IdVenta, NombreProd, Precio, Cantidad, Total) values ('{IdVenta}', '{nombreProducto}', '{precio}', {cantidad}, {total})";

                cmd2 = new SqlCommand(sql2, oCon);
                cmd2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                MessageBox.Show(mensaje);
                oCon.Close();
            }

            MessageBox.Show("Pago Completado", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Form2 p = new Form2();

            p.Id = IdVenta;
            p.Show();


            oCon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nombreProducto = "Laptop";
            precio = 1000.00;
            cantidad = 2;
            _total = precio * cantidad;

            total = _total;
            _itbis = total * itbis;
            subtotal = total + _itbis;

            txtTotal.Text = total.ToString();
            txtItbis.Text = _itbis.ToString();
            txtSubTotal.Text = subtotal.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pago = Convert.ToDouble(txtPago.Text);
            subtotal = Convert.ToDouble(txtSubTotal.Text);
            double balance = pago - subtotal;

            txtBalance.Text = balance.ToString();

            RegistrarPago();
            RegistrarProducto();

        }
    }
}
