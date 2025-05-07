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
        public static Usuario validar(string usuario, string clave)
        {
            using (var context = new MielvaEntities())
            {
                return context.Usuario
                    .Where(u => u.usuario1 == usuario && u.clave == clave)
                    .FirstOrDefault();
            }
        }
    }
}
