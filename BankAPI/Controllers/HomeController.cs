using BankAPI.Models;
using Domain.Entidades;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace BankAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var empresa = MySqlServices.GetEmpresa(1);
                return View(new EmpresaViewModel()
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome,
                    Revenda = empresa.Revenda
                });
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Salvar(string empresa, string revenda)
        {
            try
            {
                var resp = MySqlServices.UpdateEmpresa(new Empresa()
                {
                    Nome = empresa,
                    Revenda = revenda
                });

                if (!resp)
                {
                    resp = MySqlServices.AddEmpresa(new Empresa()
                    {
                        Nome = empresa,
                        Revenda = revenda
                    });
                }

                return new JsonResult(new { success = resp });
            }
            catch (Exception)
            {

                return new JsonResult(new { success =  false });
            }
        }
    }
}
