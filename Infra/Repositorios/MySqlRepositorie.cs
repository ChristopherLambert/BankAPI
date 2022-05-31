using Infra.DataBase;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositorie
{
    public static class MySqlRepositorie
    {
        public static Empresa GetEmpresa(int id)
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                var empresas = context.Empresa;
                return empresas.FirstOrDefault(emp => emp.Id == id);
            }
        }

        public static bool AddEmpresa(Empresa empresa)
        {
            try
            {
                using (var context = new MySqlDbContext())
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    context.Empresa.Add(empresa);
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateEmpresa(Empresa empresa)
        {
            try
            {
                using (var context = new MySqlDbContext())
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    context.Empresa.Update(empresa);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Retorno GetRetorno(int id)
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                var retornos = context.Retorno;
                return retornos.FirstOrDefault(ret => ret.Id == id);
            }
        }

        public static List<Retorno> GetAllRetorno()
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                return context.Retorno.ToList();
            }
        }

        public static bool AddRetorno(Retorno retorno)
        {
            try
            {
                using (var context = new MySqlDbContext())
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    context.Retorno.Add(retorno);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
