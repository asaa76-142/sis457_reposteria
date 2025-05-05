using CadMielva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class EmpleadosCln
    {
        public static int insertar(Empleado empleado)
        {
            using (var context = new MielvaEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();
                return empleado.id;
            }
        }
        public static int actualizar(Empleado empleado)
        {
            using (var context = new MielvaEntities())
            {
                var existente = context.Empleado.Find(empleado.id);
                existente.nombre = empleado.nombre;
                existente.apellido = empleado.apellido;
                existente.direccion = empleado.direccion;
                existente.telefono = empleado.telefono;
                existente.usuarioRegistro = empleado.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuario)
        {
            using (var context = new MielvaEntities())
            {
                var empleado = context.Empleado.Find(id);
                empleado.estado = -1;
                empleado.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }
        public static Empleado obtenerUno(int id)
        {
            using (var context = new MielvaEntities())
            {
                return context.Empleado.Find(id);
            }
        }
        public static List<Empleado> listar()
        {
            using (var context = new MielvaEntities())
            {
                return context.Empleado.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paEmpleadoListar_Result> listar(string parametro)
        {
            using (var context = new MielvaEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }
    }
}
