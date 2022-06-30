using Infra.DataBase;
using Infra.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositorie
{
    public static class MySqlRepositorie
    {
        // EMPRESA
        #region Empresa
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

        public static List<Empresa> GetAllEmpresa()
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                return context.Empresa.ToList();
            }
        }

        public static RespDB AddEmpresa(Empresa empresa)
        {
            try
            {
                using (var context = new MySqlDbContext())
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    context.Empresa.Add(empresa);
                    context.SaveChanges();
                    return new RespDB()
                    {
                        Success = true,
                        Id = empresa.Id
                    };
                }
            }
            catch(Exception ex)
            {
                return new RespDB()
                {
                    Success = false
                };
            }
        }

        public static RespDB UpdateEmpresa(Empresa empresa)
        {
            try
            {
                using (var context = new MySqlDbContext())
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    context.Empresa.Update(empresa);
                    context.SaveChanges();
                    return new RespDB()
                    {
                        Success = true,
                        Id = empresa.Id
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespDB()
                {
                    Success = false
                };
            }
        }

        public static bool DeleteEmpresa(int id)
        {
            using (var context = new MySqlDbContext())
            {
                try
                {
                    // Creates the database if not exists
                    context.Database.EnsureCreated();

                    var empresa = GetEmpresa(id);
                    context.Empresa.Remove(empresa);
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
        #endregion

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
