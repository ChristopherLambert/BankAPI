using Infra.DataBase;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositorie
{
    public static class MySqlRepositorieRetorno
    {
        // RETORNO
        #region Retorno
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

        public static Retorno GetRetornoByTitulo(string titulo)
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                var retornos = context.Retorno;
                return retornos.FirstOrDefault(ret => ret.Titulo.Equals(titulo));
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

        public static List<Retorno> GetDateRetorno(DateTime date)
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                return context.Retorno.Where(re => re.InsertData.Day == date.Day).ToList();
            }
        }

        public static bool UpdateRetorno(Retorno retorno)
        {
            try
            {
                using (var context = new MySqlDbContext())
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    retorno.UpdateData = DateTime.Now;
                    context.Retorno.Update(retorno);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
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

                    retorno.UpdateData = DateTime.Now;
                    retorno.InsertData = DateTime.Now;
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
        #endregion
    }
}
