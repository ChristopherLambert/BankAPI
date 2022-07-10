using Domain.Entidades;
using Infra.Repositorie;
using System.Linq;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
    public static class MySqlServicesEmpresa
    {
        // EMPRESA 
        #region EMPRESA
        public static Empresa GetEmpresa(int id)
        {
            var empresa = MySqlRepositorieEmpresa.GetEmpresa(id);
            if (empresa == null)
                return null;

            return new Empresa()
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Numero = empresa.Numero,
                Revenda = empresa.Revenda,
                RevendaNumero = empresa.RevendaNumero,
                Origem = empresa.Origem,
                Departamento = empresa.Departamento,
                Banco = empresa.Banco
            };
        }

        public static List<Empresa> GetAllEmpresa()
        {
            return MySqlRepositorieEmpresa.GetAllEmpresa().Select(rep =>
            new Empresa()
            {
                Id = rep.Id,
                Nome = rep.Nome,
                Numero = rep.Numero,
                Revenda = rep.Revenda,
                RevendaNumero = rep.RevendaNumero,
                Origem = rep.Origem,
                Departamento = rep.Departamento,
                Banco = rep.Banco
            }).ToList();
        }

        public static RespDB AddEmpresa(Empresa empresa)
        {
            var resp = MySqlRepositorieEmpresa.AddEmpresa(new Infra.Entidades.Empresa()
            {
                Nome = empresa.Nome,
                Numero = empresa.Numero,
                Revenda = empresa.Revenda,
                RevendaNumero = empresa.RevendaNumero,
                Origem = empresa.Origem,
                Departamento = empresa.Departamento,
                Banco = empresa.Banco
            });

            return new RespDB()
            {
                Id = resp.Id,
                Success = resp.Success
            };
        }

        public static RespDB UpdateEmpresa(Empresa empresa)
        {
            var resp = MySqlRepositorieEmpresa.UpdateEmpresa(new Infra.Entidades.Empresa()
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Numero = empresa.Numero,
                Revenda = empresa.Revenda,
                RevendaNumero = empresa.RevendaNumero,
                Origem = empresa.Origem,
                Departamento = empresa.Departamento,
                Banco = empresa.Banco
            });

            return new RespDB()
            {
                Id = resp.Id,
                Success = resp.Success
            };
        }

        public static bool DeleteEmpresa(int id)
        {
            var resp = MySqlRepositorieEmpresa.DeleteEmpresa(id);
            return resp;
        }
        #endregion
    }
}
