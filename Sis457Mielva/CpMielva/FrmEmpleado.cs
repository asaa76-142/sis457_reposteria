﻿using CadMielva;
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
    public partial class FrmEmpleado : Form
    {
        private bool esNuevo = false;

        public FrmEmpleado()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var lista = EmpleadoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["usuario"].HeaderText = "Usuario";
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvLista.Columns["nombres"].HeaderText = "Nombres";
            dgvLista.Columns["primerApellido"].HeaderText = "Primer Apellido";
            dgvLista.Columns["segundoApellido"].HeaderText = "Segundo Apellido";
            dgvLista.Columns["cargo"].HeaderText = "Cargo";
            dgvLista.Columns["direccion"].HeaderText = "Dirección";
            dgvLista.Columns["celular"].HeaderText = "Celular";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["cedulaIdentidad"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void cargarCargos()
        {
            var listaCargos = CargoCln.listar();
            cbxCargo.DataSource = listaCargos;
            cbxCargo.DisplayMember = "descripcion";
            cbxCargo.ValueMember = "id";
        }
        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            Size = new Size(1124, 431);
            listar();
            txtCelular.KeyPress += Util.onlyNumbers;
            txtNombres.KeyPress += Util.onlyLetters;
            txtPrimerApellido.KeyPress += Util.onlyLetters;
            txtSegundoApellido.KeyPress += Util.onlyLetters;
            cbxCargo.KeyPress += Util.onlyLetters;
            cargarCargos();
        }
        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            txtUsuario.Clear();
            cbxCargo.SelectedIndex = -1;
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
            txtCedulaIdentidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(1124, 593);


            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var empleado = EmpleadoCln.obtenerUno(id);
            var usuario = empleado.Usuario.Count > 0 ? empleado.Usuario.First().usuario1 : "";
            txtCedulaIdentidad.Text = empleado.cedulaIdentidad;
            txtNombres.Text = empleado.nombres;
            txtPrimerApellido.Text = empleado.primerApellido;
            txtSegundoApellido.Text = empleado.segundoApellido;
            txtDireccion.Text = empleado.direccion;
            txtCelular.Text = empleado.celular.ToString();
            txtUsuario.Text = usuario;
            cbxCargo.SelectedValue = empleado.idCargo;
            txtCedulaIdentidad.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpCedulaIdentidad.SetError(txtCedulaIdentidad, "");
            erpNombres.SetError(txtNombres, "");
            erpApellidos.SetError(txtPrimerApellido, "");
            erpApellidos.SetError(txtSegundoApellido, "");
            erpDireccion.SetError(txtDireccion, "");
            erpCelular.SetError(txtCelular, "");
            erpCargo.SetError(cbxCargo, "");

            if (string.IsNullOrEmpty(txtCedulaIdentidad.Text))
            {
                erpCedulaIdentidad.SetError(txtCedulaIdentidad, "El campo CI es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                erpNombres.SetError(txtNombres, "El campo Nombres es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtPrimerApellido.Text) && string.IsNullOrEmpty(txtSegundoApellido.Text))
            {
                erpApellidos.SetError(txtPrimerApellido, "Debe introducir al menos un apellido");
                erpApellidos.SetError(txtSegundoApellido, "Debe introducir al menos un apellido");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                erpDireccion.SetError(txtDireccion, "El campo Dirección es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtCelular.Text) || txtCelular.Text.Length != 8)
            {
                erpCelular.SetError(txtCelular, "El campo Celular es obligatorio o no tiene la longitud correcta");
                esValido = false;
            }
            if (string.IsNullOrEmpty(cbxCargo.Text))
            {
                erpCargo.SetError(cbxCargo, "El campo Cargo es obligatorio");
                esValido = false;
            }

            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var empleado = new Empleado();
                empleado.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
                empleado.nombres = txtNombres.Text.Trim();
                empleado.primerApellido = txtPrimerApellido.Text.Trim();
                empleado.segundoApellido = txtSegundoApellido.Text.Trim();
                empleado.direccion = txtDireccion.Text.Trim();
                empleado.celular = Convert.ToInt64(txtCelular.Text);
                empleado.idCargo = Convert.ToInt32(cbxCargo.SelectedValue);
                empleado.usuarioRegistro = Util.usuario.usuario1;

                Usuario usuario = null;
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    usuario = new Usuario();
                    usuario.usuario1 = txtUsuario.Text.Trim();
                    usuario.clave = Util.Encrypt("hola123");
                }

                if (esNuevo)
                {
                    var lista = EmpleadoCln.listarPa(txtCedulaIdentidad.Text.Trim());
                    if (lista.Any(c => c.cedulaIdentidad == txtCedulaIdentidad.Text.Trim()))
                    {
                        MessageBox.Show("Ya existe es Cedula de Identidad.", "...::: Mielva - Advertencia :::...",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    empleado.fechaRegistro = DateTime.Now;
                    empleado.estado = 1;
                    EmpleadoCln.insertar(empleado, usuario);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    empleado.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    EmpleadoCln.actualizar(empleado, txtUsuario.Text.Trim(), Util.Encrypt("hola123"));
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Registro guardado correctamente", "::: Mielva - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string ci = dgvLista.Rows[index].Cells["cedulaIdentidad"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el empleado {ci}?",
                "::: Mielva - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                EmpleadoCln.eliminar(id, Util.usuario.usuario1);
                listar();
                MessageBox.Show("Empleado eliminado correctamente", "::: Mielva - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtParametro_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
