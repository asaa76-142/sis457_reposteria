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
        private bool esNuevo = false;
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            Size = new Size(1124, 431);
            listar();
            //txtTransaccion.KeyPress += Util.onlyNumbers;
        }
        private void listar()
        {
            var lista = VentaCln.listar(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["transaccion"].HeaderText = "Transacción";
            dgvLista.Columns["fecha"].HeaderText = "Fecha";
        }
        private void limpiar()
        {
            //txtTransaccion.Clear();
            //txtFecha.Clear();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(1124, 431);
            limpiar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(1124, 593);

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(1124, 593);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var venta = VentaCln.obtenerUno(id);
            nudTransaccion.Value = venta.transaccion;
            dateFechaVentas.Value = venta.fecha;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpTransaccion.SetError(nudTransaccion, "");
            erpFecha.SetError(dateFechaVentas, "");

            if (nudTransaccion.Value < 0)
            {
                erpTransaccion.SetError(nudTransaccion, "El campo Transaccion es obligatorio");
                esValido = false;
            }
            if (dateFechaVentas.Value == DateTime.MinValue)
            {
                erpFecha.SetError(dateFechaVentas, "El campo Fecha es obligatorio");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var venta = new Venta();
                venta.transaccion = (int)nudTransaccion.Value;
                venta.fecha = dateFechaVentas.Value;

                if (esNuevo)
                {
                    venta.fechaRegistro = DateTime.Now;
                    venta.estado = 1;
                    VentaCln.insertar(venta);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    venta.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    VentaCln.actualizar(venta);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Venta guardada correctamente", "::: Mielva - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}