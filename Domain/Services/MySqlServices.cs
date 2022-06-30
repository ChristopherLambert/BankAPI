using Domain.Entidades;
using Infra.Repositorie;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Domain.Services
{
    public static class MySqlServices
    {
        // EMPRESA 
        #region EMPRESA
        public static Empresa GetEmpresa(int id)
        {
            var empresa = MySqlRepositorie.GetEmpresa(id);
            if (empresa == null)
                return null;

            return new Empresa()
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Revenda = empresa.Revenda,
                Origem = empresa.Origem,
                Departamento = empresa.Departamento,
                Banco = empresa.Banco
            };
        }

        public static List<Empresa> GetAllEmpresa()
        {
            return MySqlRepositorie.GetAllEmpresa().Select(rep =>
            new Empresa()
            {
                Id = rep.Id,
                Nome = rep.Nome,
                Revenda = rep.Revenda,
                Origem = rep.Origem,
                Departamento = rep.Departamento,
                Banco = rep.Banco
            }).ToList();
        }

        public static bool AddEmpresa(Empresa empresa)
        {
           return MySqlRepositorie.AddEmpresa(new Infra.Entidades.Empresa() 
           {
               Id = empresa.Id,
               Nome = empresa.Nome,
               Revenda = empresa.Revenda,
               Origem = empresa.Origem,
               Departamento = empresa.Departamento,
               Banco = empresa.Banco
           });
        }

        public static bool UpdateEmpresa(Empresa empresa)
        {
            return MySqlRepositorie.UpdateEmpresa(new Infra.Entidades.Empresa()
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Revenda = empresa.Revenda,
                Origem = empresa.Origem,
                Departamento = empresa.Departamento,
                Banco = empresa.Banco
            });
        }
        #endregion

        // RETORNO
        #region RETORNO
        public static Retorno GetRetorno(int id)
        {
            var retorno = MySqlRepositorie.GetRetorno(id);
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
            return MySqlRepositorie.GetAllRetorno().Select(rep => 
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
            return MySqlRepositorie.GetDateRetorno(date).Select(rep =>
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
            return MySqlRepositorie.AddRetorno(new Infra.Entidades.Retorno()
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
            var retorno = MySqlRepositorie.GetRetornoByTitulo(rep.Titulo.ToString());
            if(retorno == null)
            {
                return MySqlRepositorie.AddRetorno(new Infra.Entidades.Retorno()
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
                return MySqlRepositorie.UpdateRetorno(retorno);
            }
        }
        #endregion

    }
}
