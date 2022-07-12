using BankAPI.Models;
using Domain.Entidades;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankAPI.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HistoryController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // STATUS 1 GERADA / 2 ERRO /  E TODAS
        public IActionResult Index(string date, int empresaId, int revendaId, int statusId = 3)
        {
            try
            {
                List<Retorno> retornos = null;
                if (string.IsNullOrEmpty(date))
                {
                    //retornos = MySqlServices.GetAllRetorno();
                    retornos = MySqlServicesRetorno.GetDateRetorno(DateTime.Now);
                }
                else
                {
                    string[] validformats = new[] {"dd/MM/yyyy" , "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
                                        "MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff" };

                    CultureInfo provider = new CultureInfo("pt-BR");
                    DateTime dateTime = DateTime.ParseExact(date, validformats, provider);

                    retornos = MySqlServicesRetorno.GetDateRetorno(dateTime);
                }

                var viewModel = new HistoricoViewModel();

                #region dropdown
                //DropDown
                viewModel.Empresas = MySqlServicesEmpresa.GetAllEmpresa().Select(empresa =>
                {
                    return new DropDownViewModel()
                    {
                        Id = empresa.Id,
                        Value = empresa.Nome
                    };
                }).ToList();

                viewModel.Revendas = retornos.GroupBy(customer => customer.RevendaCodigo)
                    .Select(retorno => retorno.First())
                    .Select(retorno =>
                    {
                        return new DropDownViewModel()
                        {
                            Id = Convert.ToInt32(retorno.RevendaCodigo),
                            Value = retorno.Revenda
                        };
                    }).ToList();

                viewModel.Status.Add(new DropDownViewModel()
                {
                    Id = 1,
                    Value = "GERADA"
                });

                viewModel.Status.Add(new DropDownViewModel()
                {
                    Id = 2,
                    Value = "COM ERRO"
                });

                viewModel.Status.Add(new DropDownViewModel()
                {
                    Id = 3,
                    Value = "TODAS"
                });
                #endregion

                if (empresaId != 0)
                {
                    var empFilter = viewModel.Empresas.First(emp => emp.Id == empresaId);
                    retornos = retornos.Where(retorno => retorno.Empresa.Equals(empFilter)).ToList();
                }

                if (revendaId != 0)
                {
                    retornos = retornos.Where(retorno => Convert.ToInt32(retorno.RevendaCodigo) == revendaId).ToList();
                }

                if (statusId == 2)
                {
                    retornos = retornos.Where(retorno => !retorno.Status.Contains("GERADA")).ToList();
                }
                if (statusId == 1)
                {
                    retornos = retornos.Where(retorno => retorno.Status.Contains("GERADA")).ToList();
                }

                viewModel.Retornos = retornos.Select(
                    rep => new RetornoViewModel()
                    {
                        Id = rep.Id,
                        TransacaoID = rep.TransacaoID,
                        Empresa = rep.Empresa,
                        Cliente = rep.Cliente,
                        Status = rep.Status,
                        Valor = rep.Valor,
                        Ocorrencia = rep.Ocorrencia,
                        InsertData = rep.InsertData,
                    }).ToList();

                return View(viewModel);
            }
            catch(Exception ex)
            {
                return View(new List<HistoricoViewModel>());
            }
        }
    }
}
