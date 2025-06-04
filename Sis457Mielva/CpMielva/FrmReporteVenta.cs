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
            using (var context = new LabMielvaEntities())
            {
                var lista = (from v in context.Venta
                             join c in context.Cliente on v.idCliente equals c.id
                             join u in context.Usuario on v.idUsuario equals u.id
                             join vd in context.VentaDetalle on v.id equals vd.idVenta
                             join p in context.Producto on vd.idProducto equals p.id
                             where v.estado == 1
                             select new
                             {
                                 v.id,
                                 v.transaccion,
                                 v.fecha,
                                 Cliente = c.razonSocial,
                                 NIT = c.nit,
                                 Usuario = u.usuario1,
                                 Producto = p.descripcion,
                                 vd.cantidad,
                                 vd.precioUnitario,
                                 vd.total
                             }).ToList();
                dgvLista.DataSource = lista;
            }
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
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
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
    }
}
