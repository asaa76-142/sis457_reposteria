using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadMielva;

namespace ClnMielva
{
    public class ClienteCln
    {
        public static int insertar(Cliente cliente)
        {
            using (var context = new LabMielvaEntities())
            {
                // Validación para evitar NIT duplicado
                bool existe = context.Cliente.Any(c => c.nit == cliente.nit && c.estado != -1);
                if (existe)
                {
                    throw new Exception("Ya existe un cliente con ese NIT/CI.");
                }

                context.Cliente.Add(cliente);
                context.SaveChanges();
                return cliente.id;
            }
        }

        public static int actualizar(Cliente cliente)
        {
            using (var context = new LabMielvaEntities())
            {
                // Verificar si el nuevo NIT ya lo tiene otro cliente
                bool existe = context.Cliente.Any(c => c.nit == cliente.nit && c.id != cliente.id && c.estado != -1);
                if (existe)
                {
                    throw new Exception("Ya existe otro cliente con ese NIT/CI.");
                }

                var existente = context.Cliente.Find(cliente.id);
                existente.nit = cliente.nit;
                existente.razonSocial = cliente.razonSocial;
                existente.usuarioRegistro = cliente.usuarioRegistro;
                return context.SaveChanges();
            }
        }


        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabMielvaEntities())
            {
                var cliente = context.Cliente.Find(id);
                cliente.estado = -1;
                cliente.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }

        public static Cliente obtenerUno(int id)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.Cliente.Find(id);
            }
        }

        public static List<Cliente> listar()
        {
            using (var context = new LabMielvaEntities())
            {
                return context.Cliente.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paClienteListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.paClienteListar(parametro).ToList();
            }
        }
        public static List<paVentaClienteListar_Result> listarPa2(string parametro)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.paVentaClienteListar(parametro).ToList();
            }
        }
    }
}
