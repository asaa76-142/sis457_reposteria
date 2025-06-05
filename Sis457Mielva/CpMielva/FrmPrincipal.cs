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
    public partial class FrmPrincipal: Form
    {
        private FrmAutenticacion frmAutenticacion;
        public FrmPrincipal(FrmAutenticacion frmAutenticacion)
        {
              InitializeComponent();
              this.frmAutenticacion = frmAutenticacion;
        }

        private void btnCaProductos_Click(object sender, EventArgs e)
        {
            new FrmProducto().ShowDialog();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmAutenticacion.Show();
        }

        private void btnCaEmpleadosUsuarios_Click(object sender, EventArgs e)
        {
            new FrmEmpleado().ShowDialog();
        }

        private void btnCaVentas_Click(object sender, EventArgs e)
        {
            new FrmReporteVenta().ShowDialog();
        }

        private void btnCaClientes_Click(object sender, EventArgs e)
        {
            new FrmCliente().ShowDialog();
        }

        private void btnCaRegistrarVenta_Click(object sender, EventArgs e)
        {
            new FrmRegistroVenta().ShowDialog();
        }

        private void btnCaSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
