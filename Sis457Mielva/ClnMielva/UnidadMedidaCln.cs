using CadMielva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class UnidadMedidaCln
    {
        public static List<UnidadMedida> listar()
        {
            using (var context = new LabMielvaEntities())
            {
                return context.UnidadMedida.Where(x => x.estado != -1).ToList();
            }
        }
    }
}