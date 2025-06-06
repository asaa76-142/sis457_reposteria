﻿using CadMielva;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnMielva
{
    public class EmpleadoCln
    {
        public static int insertar(Empleado empleado, Usuario usuario)
        {
            using (var context = new LabMielvaEntities())
            {
                context.Empleado.Add(empleado);
                context.SaveChanges();

                if (usuario != null && UsuarioCln.obtenerUnoPorEmpleado(empleado.id) == null)
                {
                    usuario.idEmpleado = empleado.id;
                    usuario.usuarioRegistro = empleado.usuarioRegistro;
                    usuario.fechaRegistro = empleado.fechaRegistro;
                    usuario.estado = empleado.estado;
                    UsuarioCln.insertar(usuario);
                }

                return empleado.id;
            }
        }

        public static int actualizar(Empleado empleado, string nombreUsuario, string clave)
        {
            using (var context = new LabMielvaEntities())
            {
                var existente = context.Empleado.Find(empleado.id);
                existente.cedulaIdentidad = empleado.cedulaIdentidad;
                existente.nombres = empleado.nombres;
                existente.primerApellido = empleado.primerApellido;
                existente.segundoApellido = empleado.segundoApellido;
                existente.direccion = empleado.direccion;
                existente.celular = empleado.celular;
                existente.idCargo = empleado.idCargo;
                existente.usuarioRegistro = empleado.usuarioRegistro;

                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    var usuario = UsuarioCln.obtenerUnoPorEmpleado(existente.id);
                    if (usuario != null)
                    {
                        usuario.usuario1 = nombreUsuario;
                        usuario.usuarioRegistro = empleado.usuarioRegistro;
                        UsuarioCln.actualizar(usuario);
                    }
                    else
                    {
                        usuario = new Usuario
                        {
                            idEmpleado = existente.id,
                            usuario1 = nombreUsuario,
                            clave = clave,
                            estado = 1,
                            fechaRegistro = DateTime.Now,
                            usuarioRegistro = empleado.usuarioRegistro
                        };
                        UsuarioCln.insertar(usuario);
                    }
                }

                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var context = new LabMielvaEntities())
            {
                var empleado = context.Empleado.Find(id);
                empleado.estado = -1;
                empleado.usuarioRegistro = usuario;

                var usuarioEmpleado = UsuarioCln.obtenerUnoPorEmpleado(empleado.id);
                if (usuarioEmpleado != null)
                {
                    UsuarioCln.eliminar(usuarioEmpleado.id, usuario);
                }

                return context.SaveChanges();
            }
        }

        public static Empleado obtenerUno(int id)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.Empleado.Include(x => x.Usuario).Where(x => x.id == id).FirstOrDefault();
            }
        }

        public static List<paEmpleadoListar_Result> listarPa(string parametro)
        {
            using (var context = new LabMielvaEntities())
            {
                return context.paEmpleadoListar(parametro).ToList();
            }
        }
    }
}