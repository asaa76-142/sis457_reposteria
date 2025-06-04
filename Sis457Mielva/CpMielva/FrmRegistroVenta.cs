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
        private void CalcularCambio()
        {
            if (!string.IsNullOrEmpty(txtTotal.Text) && !string.IsNullOrEmpty(txtEfectivo.Text))
            {
                decimal total, efectivo;
                if (decimal.TryParse(txtTotal.Text, out total) && decimal.TryParse(txtEfectivo.Text, out efectivo))
                {
                    decimal cambio = efectivo - total;
                    txtCambio.Text = cambio.ToString("0.00");
                }
            }
            else
            {
                txtCambio.Text = "0.00";
            }
        }
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
            CalcularCambio(); // Agregamos esta línea para recalcular el cambio
        }
        public FrmRegistroVenta()
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
            CalcularCambio(); // Usamos método separado para calcular cambio
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
                txtNombre.Text = cliente.razonSocial;
                // guardar el id del cliente para usarlo al guardar la venta
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
        private decimal precioPastelCumpleMujer2 = 65m;
        private decimal precioPastelNormalVaron = 85m;
        private decimal precioPastelNormalMujer = 85m;
        private decimal precioEmpanada = 3.5m;
        private decimal precioGalletaNaranja = 0.5m;
        private decimal precioGalletaMaicena = 0.5m;

        private void ActualizarTotal()
        {
            decimal total = 0;
            total += precioPastelCumpleVaron * nudPastelCumpleVaron.Value;
            total += precioPastelCumpleMujer * nudPastelCumpleMujer.Value;
            total += precioPastelCumpleVaron2 * nudPastelCumpleVaron2.Value;
            total += precioPastelCumpleMujer2 * nudPastelCumpleMujer2.Value;
            total += precioPastelNormalVaron * nudPastelNormalVaron.Value;
            total += precioPastelNormalMujer * nudPastelNormalMujer.Value;
            total += precioEmpanada * nudEmpanada.Value;
            total += precioGalletaNaranja * nudGalletaNaranja.Value;
            total += precioGalletaMaicena * nudGalletaMaicena.Value;

            txtTotal.Text = total.ToString("0.00");
        }
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            CalcularCambio(); // También recalculamos cambio cuando cambie el total
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Buscar cliente por NIT
            var clientes = ClienteCln.listarPa2(txtNit.Text.Trim());
            if (clientes.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var cliente = clientes[0];

            // 2. Obtener usuario logueado (ajusta según tu lógica)
            int idUsuario = Util.usuario.id;

            // 3. Generar número de transacción (puedes mejorarlo)
            int transaccion = (int)(DateTime.Now.Ticks % int.MaxValue);

            // 4. Crear la venta
            var venta = new Venta
            {
                idUsuario = idUsuario,
                idCliente = cliente.id,
                transaccion = transaccion,
                fecha = DateTime.Now,
                usuarioRegistro = Util.usuario.usuario1,
                fechaRegistro = DateTime.Now,
                estado = 1,
                VentaDetalle = new List<VentaDetalle>()
            };

            // 5. Agregar detalles de productos vendidos
            if (nudPastelCumpleVaron.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 1, cantidad = nudPastelCumpleVaron.Value, precioUnitario = 85, total = nudPastelCumpleVaron.Value * 85, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudPastelCumpleMujer.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 2, cantidad = nudPastelCumpleMujer.Value, precioUnitario = 85, total = nudPastelCumpleMujer.Value * 85, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudPastelCumpleVaron2.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 4, cantidad = nudPastelCumpleVaron2.Value, precioUnitario = 65, total = nudPastelCumpleVaron2.Value * 65, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudPastelCumpleMujer2.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 5, cantidad = nudPastelCumpleMujer2.Value, precioUnitario = 65, total = nudPastelCumpleMujer2.Value * 65, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudPastelNormalVaron.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 6, cantidad = nudPastelNormalVaron.Value, precioUnitario = 85, total = nudPastelNormalVaron.Value * 85, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudPastelNormalMujer.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 7, cantidad = nudPastelNormalMujer.Value, precioUnitario = 85, total = nudPastelNormalMujer.Value * 85, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudEmpanada.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 8, cantidad = nudEmpanada.Value, precioUnitario = 3.5m, total = nudEmpanada.Value * 3.5m, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudGalletaNaranja.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 9, cantidad = nudGalletaNaranja.Value, precioUnitario = 0.5m, total = nudGalletaNaranja.Value * 0.5m, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });
            if (nudGalletaMaicena.Value > 0)
                venta.VentaDetalle.Add(new VentaDetalle { idProducto = 10, cantidad = nudGalletaMaicena.Value, precioUnitario = 0.5m, total = nudGalletaMaicena.Value * 0.5m, usuarioRegistro = Util.usuario.usuario1, fechaRegistro = DateTime.Now, estado = 1 });

            if (venta.VentaDetalle.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 6. Guardar la venta
            VentaCln.insertar(venta);

            MessageBox.Show("Venta guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 7. Cerrar y actualizar el reporte
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnRegistrarNuevaVenta_Click(object sender, EventArgs e)
        {
            new FrmCliente().ShowDialog();
        }
    }
}