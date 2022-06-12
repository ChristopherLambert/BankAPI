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

        // RETORNO
        public static Retorno GetRetorno(int id)
        {
            var retorno = MySqlRepositorie.GetRetorno(id);
            return new Retorno()
            {
                Id = retorno.Id,
                Empresa = retorno.Empresa,
                Cliente = retorno.Cliente,
                Status = retorno.Status,
                Atualizacao = retorno.Atualizacao,
                TransacaoID = retorno.TransacaoID
            };
        }

        public static List<Retorno> GetAllRetorno()
        {
            return MySqlRepositorie.GetAllRetorno().Select(rep => 
            new Retorno()
            {
                Id = rep.Id,
                Empresa = rep.Empresa,
                Cliente = rep.Cliente,
                Status = rep.Status,
                Atualizacao = rep.Atualizacao,
                TransacaoID = rep.TransacaoID
            }).ToList();
        }

        public static List<Retorno> GetDateRetorno(DateTime date)
        {
            return MySqlRepositorie.GetDateRetorno(date).Select(rep =>
            new Retorno()
            {
                Id = rep.Id,
                Empresa = rep.Empresa,
                Cliente = rep.Cliente,
                Status = rep.Status,
                Atualizacao = rep.Atualizacao,
                TransacaoID = rep.TransacaoID
            }).ToList();
        }

        public static bool AddRetorno(Retorno retorno)
        {
            return MySqlRepositorie.AddRetorno(new Infra.Entidades.Retorno()
            {
                Id = retorno.Id,
                Empresa = retorno.Empresa,
                Cliente = retorno.Cliente,
                Status = retorno.Status,
                Atualizacao = retorno.Atualizacao,
                TransacaoID = retorno.TransacaoID
            });
        }
    }
}
