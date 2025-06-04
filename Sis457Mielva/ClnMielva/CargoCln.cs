using CadMielva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class CargoCln
    {
        public static List<Cargo> listar()
        {
            using (var context = new LabMielvaEntities())
            {
                return context.Cargo.Where(x => x.estado != -1).ToList();
            }
        }
    }
}
