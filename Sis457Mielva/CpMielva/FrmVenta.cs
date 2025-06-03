using C1.Framework;
using CadMielva;
using ClnMielva;
using CpMielva;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpMielva
{
    public partial class FrmVenta : Form
    {
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotal.Text) && !string.IsNullOrEmpty(txtEfectivo.Text))
            {
                txtCambio.Text = (Convert.ToDouble(txtEfectivo.Text) - Convert.ToDouble(txtTotal.Text)).ToString();
            }
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            dtpFecha.MaxDate = DateTime.Now;
            //txtTransaccion.KeyPress += Util.onlyNumbers;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var clientes = ClienteCln.listarPa2(txtNit.Text.Trim());
            if (clientes.Count > 0)
            {
                var cliente = clientes[0];
                // Supón que tienes un txtRazonSocial para mostrar la razón social
                txtNombre.Text = cliente.razonSocial;
                // Puedes guardar el id del cliente para usarlo al guardar la venta
                // this.clienteId = cliente.id;
            }
            else
            {
                MessageBox.Show("Cliente no encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Text = "";
                // this.clienteId = 0;
            }
        }
    }
}