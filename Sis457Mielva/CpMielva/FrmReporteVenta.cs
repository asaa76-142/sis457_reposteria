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

        private void FrmVentaDetalle_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrarNuevaVenta_Click(object sender, EventArgs e)
        {
            new FrmRegistroVenta().ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
