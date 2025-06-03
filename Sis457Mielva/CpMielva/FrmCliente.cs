using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CadMielva;
using ClnMielva;

namespace CpMielva
{
    public partial class FrmCliente : Form
    {
        private bool esNuevo = false;
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var lista = ClienteCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["razonSocial"].HeaderText = "Razón Social";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["nit"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(1124, 431);
            listar();
        }

        private void limpiar()
        {
            txtNit.Clear();
            txtRazonSocial.Clear();
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
            txtNit.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(1124, 593);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var cliente = ClienteCln.obtenerUno(id);
            txtNit.Text = cliente.nit;
            txtRazonSocial.Text = cliente.razonSocial;
            txtNit.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private bool validar()
        {
            bool esValido = true;
            erpCodigo.SetError(txtNit, "");
            erpDescripcion.SetError(txtRazonSocial, "");

            if (string.IsNullOrEmpty(txtNit.Text))
            {
                erpCodigo.SetError(txtNit, "El campo nit es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtRazonSocial.Text))
            {
                erpDescripcion.SetError(txtRazonSocial, "El campo Razón Social es obligatorio");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var cliente = new Cliente();
                cliente.nit = txtNit.Text.Trim();
                cliente.razonSocial = txtRazonSocial.Text.Trim();
                cliente.usuarioRegistro = "admin";
                //cliente.usuarioRegistro = Util.usuario.usuario1;

                if (esNuevo)
                {
                    cliente.fechaRegistro = DateTime.Now;
                    cliente.estado = 1;
                    ClienteCln.insertar(cliente);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    cliente.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    ClienteCln.actualizar(cliente);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Cliente guardado correctamente", "::: Minerva - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string nit = dgvLista.Rows[index].Cells["nit"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el cliente {nit}?",
                "::: Mielva - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ClienteCln.eliminar(id, "admin");
                //ClienteCln.eliminar(id, Util.usuario.usuario1);
                listar();
                MessageBox.Show("Cliente dado de baja correctamente", "::: Minerva - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
    }
}
