using Domain.Entidades;
using Infra.Repositorie;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Domain.Services
{
    public static class MySqlServicesRetorno
    {
        // RETORNO
        #region RETORNO
        public static Retorno GetRetorno(int id)
        {
            var retorno = MySqlRepositorieRetorno.GetRetorno(id);
            return new Retorno()
            {
                Id = retorno.Id,
                TransacaoID = retorno.TransacaoID,
                Empresa = retorno.Empresa,
                Cliente = retorno.Cliente,
                Status = retorno.Status,
                Ocorrencia = retorno.Ocorrencia,

            };
        }

        public static List<Retorno> GetAllRetorno()
        {
            return MySqlRepositorieRetorno.GetAllRetorno().Select(rep => 
            new Retorno()
            {
                Id = rep.Id,
                TransacaoID = rep.TransacaoID,
                Empresa = rep.Empresa,
                Cliente = rep.Cliente,
                Ocorrencia = rep.Ocorrencia,
                Valor = rep.Valor,
                Status = rep.Status,
                InsertData = rep.InsertData.ToString("dd/MM/yyyy")
            }).ToList();
        }

        public static List<Retorno> GetDateRetorno(DateTime date)
        {
            return MySqlRepositorieRetorno.GetDateRetorno(date).Select(rep =>
            new Retorno()
            {
                Id = rep.Id,
                TransacaoID = rep.TransacaoID,
                Empresa = rep.Empresa,
                Cliente = rep.Cliente,
                Ocorrencia = rep.Ocorrencia,
                Valor = rep.Valor,
                Status = rep.Status,
                InsertData = rep.InsertData.ToString("dd/MM/yyyy")
            }).ToList();
        }

        public static bool AddRetorno(Retorno rep)
        {
            return MySqlRepositorieRetorno.AddRetorno(new Infra.Entidades.Retorno()
            {
                Id = rep.Id,
                TransacaoID = rep.TransacaoID,
                Empresa = rep.Empresa,
                Cliente = rep.Cliente,
                Ocorrencia = rep.Ocorrencia,
                Valor = rep.Valor,
                Status = rep.Status
            });
        }

        public static bool AddOrUpdateRetorno(Retorno rep)
        {
            var retorno = MySqlRepositorieRetorno.GetRetornoByTitulo(rep.Titulo.ToString());
            if(retorno == null)
            {
                return MySqlRepositorieRetorno.AddRetorno(new Infra.Entidades.Retorno()
                {
                    Id = rep.Id,
                    TransacaoID = rep.TransacaoID,
                    Empresa = rep.Empresa,
                    Cliente = rep.Cliente,
                    Ocorrencia = rep.Ocorrencia,
                    Valor = rep.Valor,
                    Titulo = rep.Titulo,
                    Status = rep.Status
                });
            }
            else
            {
                retorno.Status = rep.Status;
                retorno.Ocorrencia = rep.Ocorrencia;
                retorno.OcorrenciaCampos = rep.OcorrenciaCampos;
                return MySqlRepositorieRetorno.UpdateRetorno(retorno);
            }
        }
        #endregion
    }
}
