using BankAPI.Models;
using Domain.Entidades;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            try
            {
                var retornos = MySqlServices.GetAllRetorno();

                return View(retornos.Select(
                rep => new HistoricoViewModel()
                     {
                         Id = rep.Id,
                         Empresa = rep.Empresa,
                         Status = rep.Status,
                         Update = rep.Atualizacao
                     }).ToList());
            }
            catch(Exception ex)
            {
                return View(new List<HistoricoViewModel>());
            }
        }
    }
}
