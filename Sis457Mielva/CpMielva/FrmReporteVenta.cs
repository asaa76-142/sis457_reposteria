using CadMielva;
using ClnMielva;
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
    public partial class FrmReporteVenta : Form
    {
        public FrmReporteVenta()
        {
            InitializeComponent();
        }

        private void CargarVentas()
        {
           var lista = VentaCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["idVenta"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["fecha"].Visible = false;
            dgvLista.Columns["Usuario"].Visible = false;
            dgvLista.Columns["transaccion"].HeaderText = "Transacción";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            //dgvLista.Columns["razonSocial"].HeaderText = "Cliente";
            dgvLista.Columns["nit"].HeaderText = "NIT";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario de Registro";
            dgvLista.Columns["cantidad"].HeaderText = "Cantidad";
            dgvLista.Columns["precioUnitario"].HeaderText = "Precio Unitario";
            dgvLista.Columns["total"].HeaderText = "Total";
            //dgvLista.Columns["precioUnitario"].DefaultCellStyle.Format = "N2";
            //dgvLista.Columns["total"].DefaultCellStyle.Format = "N2";


            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["transaccion"];
            btnEliminar.Enabled = lista.Count > 0;

        }

        private void FrmVentaDetalle_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void btnRegistrarNuevaVenta_Click(object sender, EventArgs e)
        {
            var frm = new FrmRegistroVenta();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Vuelve a cargar la lista de ventas
                CargarVentas();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["idVenta"].Value);
            string transaccion = dgvLista.Rows[index].Cells["transaccion"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar la venta {transaccion}?", 
                "...::: Mielva - Mensaje :::... ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                VentaCln.eliminar(id, Util.usuario.usuario1);
                CargarVentas();
                MessageBox.Show("Venta eliminada correctamente", "...::: Mielva - Mensaje :::... ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) CargarVentas();
        }
    }
}
