using CadMielva;
using ClnMielva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class VentaCln
    {
        public static int insertar(Venta venta)
        {
            using (var context = new LabMielvaEntities())
            {
                context.Venta.Add(venta);
                context.SaveChanges();
                return venta.id;
            }
        }
        public static int actualizar(Venta venta)
        {
            using (var context = new LabMielvaEntities())
            {
                var existente = context.Venta.Find(venta.id);
                existente.fecha = venta.fecha;
                existente.transaccion = venta.transaccion;
                existente.usuarioRegistro = venta.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabMielvaEntities())
            {
                var venta = context.Venta.Find(id);
                venta.estado = -1;
                venta.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }
        public static int registrarVenta(
        int idCliente,
        int idUsuario,
        string usuarioRegistro,
        List<(int idProducto, decimal cantidad, decimal precioUnitario)> detalles)
        {
            using (var context = new LabMielvaEntities())
            {
                int transaccion = (int)(DateTime.Now.Ticks % int.MaxValue);
                var venta = new Venta
                {
                    idUsuario = idUsuario,
                    idCliente = idCliente,
                    transaccion = transaccion,
                    fecha = DateTime.Now,
                    usuarioRegistro = usuarioRegistro,
                    fechaRegistro = DateTime.Now,
                    estado = 1,
                    VentaDetalle = new List<VentaDetalle>()
                };

                foreach (var detalle in detalles)
                {
                    venta.VentaDetalle.Add(new VentaDetalle
                    {
                        idProducto = detalle.idProducto,
                        cantidad = detalle.cantidad,
                        precioUnitario = detalle.precioUnitario,
                        total = detalle.cantidad * detalle.precioUnitario,
                        usuarioRegistro = usuarioRegistro,
                        fechaRegistro = DateTime.Now,
                        estado = 1
                    });
                }

                if (venta.VentaDetalle.Count == 0)
                    throw new ArgumentException("Debe registrar al menos un producto.");

                context.Venta.Add(venta);
                context.SaveChanges();

                return venta.id;
            }
        }

        public static Venta obtenerUno(int id)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.Venta.Find(id);
            }
        }
        public static List<Venta> listar()
        {
            using (var context = new LabMielvaEntities())
            {
                return context.Venta.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paVentaListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.paVentaListar(parametro).ToList();
            }
        }
    }
}
