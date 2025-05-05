using CadMielva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class UsuarioCln
    {
        public static int insertar(Usuario usuario)
        {
            using (var context = new MielvaEntities())
            {
                context.Usuario.Add(usuario);
                context.SaveChanges();
                return usuario.id;
            }
        }
        public static int actualizar(Usuario usuario)
        {
            using (var context = new MielvaEntities())
            {
                var existente = context.Usuario.Find(usuario.id);
                existente.usuarioRegistro = usuario.usuarioRegistro;
                return context.SaveChanges();
            }
        }
        public static int eliminar(int id, string usuario)
        {
            using (var context = new MielvaEntities())
            {
                var user = context.Usuario.Find(id);
                user.estado = -1;
                user.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }
        public static Usuario obtenerUno(int id)
        {
            using (var context = new MielvaEntities())
            {
                return context.Usuario.Find(id);
            }
        }
        public static List<Usuario> listar()
        {
            using (var context = new MielvaEntities())
            {
                return context.Usuario.Where(x => x.estado != -1).ToList();
            }
        }
    }
}
