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

        public IActionResult Index(string date)
        {
            try
            {
                List<Retorno> retornos = null;
                if (string.IsNullOrEmpty(date))
                {
                    //retornos = MySqlServices.GetAllRetorno();
                    retornos = MySqlServices.GetDateRetorno(DateTime.Now);
                }
                else
                {
                    string[] validformats = new[] {"dd/MM/yyyy" , "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
                                        "MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff" };

                    CultureInfo provider = new CultureInfo("pt-BR");
                    DateTime dateTime = DateTime.ParseExact(date, validformats, provider);

                    retornos = MySqlServices.GetDateRetorno(dateTime);
                }

                return View(retornos.Select(
                rep => new HistoricoViewModel()
                     {
                         Id = rep.Id,
                         TransacaoID = rep.TransacaoID,
                         Empresa = rep.Empresa,
                         Cliente = rep.Cliente,
                         Status = rep.Status,
                         Valor = rep.Valor,
                         Ocorrencia = rep.Ocorrencia,
                         InsertData = rep.InsertData,
                     }).ToList());
            }
            catch(Exception ex)
            {
                return View(new List<HistoricoViewModel>());
            }
        }
    }
}
