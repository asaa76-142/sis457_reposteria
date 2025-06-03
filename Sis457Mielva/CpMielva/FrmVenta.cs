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
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }
        public FrmVenta()
        {
            InitializeComponent();
            nudPastelCumpleVaron.ValueChanged += nud_ValueChanged;
            nudPastelCumpleMujer.ValueChanged += nud_ValueChanged;
            nudPastelCumpleVaron2.ValueChanged += nud_ValueChanged;
            nudPastelCumpleMujer2.ValueChanged += nud_ValueChanged;
            nudPastelNormalVaron.ValueChanged += nud_ValueChanged;
            nudPastelNormalMujer.ValueChanged += nud_ValueChanged;
            nudEmpanada.ValueChanged += nud_ValueChanged;
            nudGalletaNaranja.ValueChanged += nud_ValueChanged;
            nudGalletaMaicena.ValueChanged += nud_ValueChanged;
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
        private void txtNit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }
        private decimal precioPastelCumpleVaron = 85m;
        private decimal precioPastelCumpleMujer = 85m;
        private decimal precioPastelCumpleVaron2 = 65m;
        private decimal precioPastelCumpleMujero2 = 65m;
        private decimal precioPastelNormalVaron = 85m;
        private decimal precioPastelNormalMujero = 85m;
        private decimal precioEmpanada = 3.5m;
        private decimal precioGalletaNaranja = 0.5m;
        private decimal precioGalletaMaicena = 0.5m;

        private void ActualizarTotal()
        {
            decimal total = 0;
            total += precioPastelCumpleVaron * nudPastelCumpleVaron.Value;
            total += precioPastelCumpleMujer * nudPastelCumpleMujer.Value;
            total += precioPastelCumpleVaron2 * nudPastelCumpleVaron2.Value;
            total += precioPastelCumpleMujero2 * nudPastelCumpleMujer2.Value;
            total += precioPastelNormalVaron * nudPastelNormalVaron.Value;
            total += precioPastelNormalMujero * nudPastelNormalMujer.Value;
            total += precioEmpanada * nudEmpanada.Value;
            total += precioGalletaNaranja * nudGalletaNaranja.Value;
            total += precioGalletaMaicena * nudGalletaMaicena.Value;

            txtTotal.Text = total.ToString("0.00");
        }
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}