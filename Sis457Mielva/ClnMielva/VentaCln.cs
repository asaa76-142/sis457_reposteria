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
