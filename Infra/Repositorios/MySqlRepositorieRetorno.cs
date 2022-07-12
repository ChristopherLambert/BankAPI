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

// DADOS MOCADOS
//INSERT INTO uTestBoleto.Retorno
//(TransacaoID, Titulo, Empresa, Revenda, RevendaCodigo, Parcela, Cliente, Ocorrencia, OcorrenciaCampos, Valor, Status, InsertData, UpdateData)
//VALUES('1234', '0009678', '1', 'Teste', '1', '009112', 'CHRISTOPHER', 'Erro ao gerar remessa', ' CPF Invalido', '123', 'REMESSA COM FALHA', CURDATE(), CURDATE())

//INSERT INTO uTestBoleto.Retorno
//(TransacaoID, Titulo, Empresa, Revenda, RevendaCodigo, Parcela, Cliente, Ocorrencia, OcorrenciaCampos, Valor, Status, InsertData, UpdateData)
//VALUES('1234', '0009678', '1', 'Teste', '2', '009112', 'CHRISTOPHER', 'Erro ao gerar remessa', ' CNPJ Invalido', '123', 'REMESSA COM FALHA', CURDATE(), CURDATE())

//INSERT INTO uTestBoleto.Retorno
//(TransacaoID, Titulo, Empresa, Revenda, RevendaCodigo, Parcela, Cliente, Ocorrencia, OcorrenciaCampos, Valor, Status, InsertData, UpdateData)
//VALUES('1234', '0009678', '1', 'Teste', '1', '009112', 'CHRISTOPHER', '', '', '123', 'REMESSA GERADA', CURDATE(), CURDATE())

//INSERT INTO uTestBoleto.Retorno
//(TransacaoID, Titulo, Empresa, Revenda, RevendaCodigo, Parcela, Cliente, Ocorrencia, OcorrenciaCampos, Valor, Status, InsertData, UpdateData)
//VALUES('1234', '0009678', '1', 'Teste 2', '2', '009112', 'CHRISTOPHER', '', '', '123', 'REMESSA GERADA', CURDATE(), CURDATE())

//INSERT INTO uTestBoleto.Retorno
//(TransacaoID, Titulo, Empresa, Revenda, RevendaCodigo, Parcela, Cliente, Ocorrencia, OcorrenciaCampos, Valor, Status, InsertData, UpdateData)
//VALUES('1234', '0009678', '1', 'Teste 3', '3', '009112', 'CHRISTOPHER', '', '', '123', 'REMESSA GERADA', CURDATE(), CURDATE())

//INSERT INTO uTestBoleto.Retorno
//(TransacaoID, Titulo, Empresa, Revenda, RevendaCodigo, Parcela, Cliente, Ocorrencia, OcorrenciaCampos, Valor, Status, InsertData, UpdateData)
//VALUES('1234', '0009678', '2', 'Teste 3', '3', '009112', 'CHRISTOPHER', '', '', '123', 'REMESSA GERADA', CURDATE(), CURDATE())