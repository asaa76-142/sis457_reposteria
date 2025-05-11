using CadMielva;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class Usuario
    {

        public static UsuarioCd validar(string usuario, string clave)
        {
            using (var context = new MielvaEntities())
            {
                return context.Usuario
                    .Where(u => u.usuario1 == usuario && u.clave == clave)
                    .FirstOrDefault();
            }
        }

        public static int actualizar(UsuarioCd usuario)
        {
            using (var context = new MielvaEntities())
            {
                var existente = context.Usuario.Find(usuario.id);
                existente.usuario1 = usuario.usuario1;
                existente.usuarioRegistro = usuario.usuarioRegistro;
                return context.SaveChanges();
            }

        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new MielvaEntities())
            {
                var usuario = context.Usuario.Find(id);
                usuario.estado = -1;
                usuario.usuarioRegistro = usuarioRegistro;
                return context.SaveChanges();
            }

        }

        public static int insertar(UsuarioCd usuario)
        {
            using (var context = new MielvaEntities())
            {
                context.Usuario.Add(usuario);
                context.SaveChanges();
                return usuario.id;
            }

        }

        public static UsuarioCd obtenerUnoPorEmpleado(int idEmpleado)
        {
            using (var context = new MielvaEntities())
            {
                return context.Usuario.Where(x => x.idEmpleado == idEmpleado)
                    .FirstOrDefault();
            }

        }

    }
}
