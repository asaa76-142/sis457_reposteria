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
        public static int insertar(Empleado empleado, UsuarioCd usuario)
        {
            using (var context = new MielvaEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();

                if (usuario != null && Usuario.obtenerUnoPorEmpleado(empleado.id) == null)
                {
                    usuario.idEmpleado = empleado.id;
                    usuario.usuarioRegistro = (string)empleado.usuarioRegistro;
                    usuario.fechaRegistro = empleado.fechaRegistro;
                    usuario.estado = empleado.estado;
                    Usuario.insertar(usuario);
                }

                return empleado.id;
            }
        }

        public static int actualizar(Empleado empleado, string nombreUsuario, string clave)
        {
            using (var context = new MielvaEntities())
            {
                var existente = context.Empleado.Find(empleado.id);
                existente.cedulaIdentidad = empleado.cedulaIdentidad;
                existente.nombres = empleado.nombres;
                existente.primerApellido = empleado.primerApellido;
                existente.segundoApellido = empleado.segundoApellido;
                existente.direccion = empleado.direccion;
                existente.celular = empleado.celular;
                existente.cargo = empleado.cargo;
                existente.usuarioRegistro = empleado.usuarioRegistro;

                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    var usuario = Usuario.obtenerUnoPorEmpleado(existente.id);
                    if (usuario != null)
                    {
                        usuario.usuario1 = nombreUsuario;
                        usuario.usuarioRegistro = empleado.usuarioRegistro;
                        Usuario.actualizar(usuario);
                    }
                    else
                    {
                        usuario = new UsuarioCd
                        {
                            idEmpleado = existente.id,
                            usuario1 = nombreUsuario,
                            clave = clave,
                            estado = 1,
                            fechaRegistro = DateTime.Now,
                            usuarioRegistro = empleado.usuarioRegistro
                        };
                        Usuario.insertar(usuario);
                    }
                }

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

                var usuarioEmpleado = Usuario.obtenerUnoPorEmpleado(empleado.id);
                if (usuarioEmpleado != null)
                {
                    Usuario.eliminar(usuarioEmpleado.id, usuario);
                }

                return context.SaveChanges();


            }
        }

        public static Empleado obtenerUno(int id)
        {
            using (var context = new MielvaEntities())
            {
                return context.Empleado.Include("Usuario").Where(x => x.id == id).FirstOrDefault();
            }
        }

        public static List<paEmpleadoListar_Result> listarPa(string parametro)
        {
            using (var context = new MielvaEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }

    }

}
