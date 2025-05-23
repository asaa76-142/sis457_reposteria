﻿using System;
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
            using (var context = new MielvaEntities())
            {
                context.Cliente.Add(cliente);
                context.SaveChanges();
                return cliente.id;
            }
        }
        public static int actualizar(Cliente cliente)
        {
            using (var context = new MielvaEntities())
            {
                var existente = context.Cliente.Find(cliente.id);
                existente.nit = cliente.nit;
                existente.razonSocial = cliente.razonSocial;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario)
        {
            using (var context = new MielvaEntities())
            {
                var cliente = context.Cliente.Find(id);
                cliente.estado = -1;
                cliente.usuarioRegistro = usuario;
                return context.SaveChanges();
            }
        }

        public static Cliente obtenerUno(int id)
        {
            using (var context = new MielvaEntities())
            {
                return context.Cliente.Find(id);
            }
        }

        public static List<Cliente> listar()
        {
            using (var context = new MielvaEntities())
            {
                return context.Cliente.Where(x => x.estado != -1).ToList();
            }
        }
        public static List<paClienteListar_Result> listarPa(string parametro)
        {
            using (var context = new MielvaEntities())
            {
                return context.paClienteListar(parametro).ToList();
            }
        }
    }
}
