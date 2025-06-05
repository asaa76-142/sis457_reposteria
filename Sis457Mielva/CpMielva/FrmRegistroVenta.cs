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
    public partial class FrmRegistroVenta : Form
    {
        private int clienteSeleccionadoId = 0;

        public FrmRegistroVenta()
        {
            InitializeComponent();
            ConfigurarEventos();
        }

        private void ConfigurarEventos()
        {
            // Configurar eventos para recalcular automáticamente
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
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
            CalcularCambio(); // Agregamos esta línea para recalcular el cambio
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio(); // Usamos método separado para calcular cambio
        }
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio();
        }

        private void ActualizarTotal()
        {
            var detalles = ObtenerDetallesVenta();
            decimal total = VentaCln.calcularTotalVenta(detalles);
            txtTotal.Text = total.ToString("0.00");
        }

        private void CalcularCambio()
        {
            if (decimal.TryParse(txtTotal.Text, out decimal total) &&
                decimal.TryParse(txtEfectivo.Text, out decimal efectivo))
            {
                decimal cambio = efectivo - total;
                txtCambio.Text = cambio.ToString("0.00");
            }
            else
            {
                txtCambio.Text = "0.00";
            }
        }

        private List<(int idProducto, decimal cantidad, decimal precioUnitario)> ObtenerDetallesVenta()
        {
            return new List<(int, decimal, decimal)>
            {
                (1, nudPastelCumpleVaron.Value, 85m),
                (2, nudPastelCumpleMujer.Value, 85m),
                (4, nudPastelCumpleVaron2.Value, 65m),
                (5, nudPastelCumpleMujer2.Value, 65m),
                (6, nudPastelNormalVaron.Value, 85m),
                (7, nudPastelNormalMujer.Value, 85m),
                (8, nudEmpanada.Value, 3.5m),
                (9, nudGalletaNaranja.Value, 0.5m),
                (10, nudGalletaMaicena.Value, 0.5m)
            };
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
            try
            {
                var clientes = ClienteCln.listarPa2(txtNit.Text.Trim());
                if (clientes.Count > 0)
                {
                    var cliente = clientes[0];
                    txtNombre.Text = cliente.razonSocial;
                    clienteSeleccionadoId = cliente.id;
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "...::: Mielva - Mensaje :::...",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Text = "";
                    clienteSeleccionadoId = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar cliente: {ex.Message}",
                              "...::: Mielva - Error :::...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var detalles = ObtenerDetallesVenta();
                int idUsuario = Util.usuario.id;
                string usuarioRegistro = Util.usuario.usuario1;

                // Usar el método con validación mejorada
                int ventaId = VentaCln.registrarVentaConValidacion(clienteSeleccionadoId, idUsuario,
                                                                usuarioRegistro, detalles);

                MessageBox.Show("Venta guardada correctamente.", "...::: Mielva - Mensaje :::...",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "...::: Mielva - Advertencia :::...",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar la venta.\n{ex.Message}",
                              "...::: Mielva - Error :::...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarNuevoCliente_Click(object sender, EventArgs e)
        {
            new FrmCliente().ShowDialog();
        }
        private void txtNit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }
    }
}