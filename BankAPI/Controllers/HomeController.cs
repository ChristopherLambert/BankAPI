using BankAPI.Models;
using Domain.Entidades;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
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

        public IActionResult Index(int id)
        {
            try
            {
                List<Empresa> empresas = MySqlServices.GetAllEmpresa();

                var homeModel = new HomeViewModel();
                homeModel.EmpresaId = 0;
                homeModel.Empresas = empresas.Select((item) =>
                {
                    return new EmpresaViewModel()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Revenda = item.Revenda,
                        Banco = item.Banco,
                        Origem = item.Origem,
                        Departamento = item.Departamento
                    };
                }).ToList();

                if (id != 0)
                {
                    var empresa = empresas.FirstOrDefault(emp => emp.Id == id);
                    if (empresa != null)
                    {
                        homeModel.EmpresaId = id;
                        homeModel.MainEmpresa = new EmpresaViewModel()
                        {
                            Id = empresa.Id,
                            Nome = empresa.Nome,
                            Revenda = empresa.Revenda,
                            Banco = empresa.Banco,
                            Origem = empresa.Origem,
                            Departamento = empresa.Departamento
                        };
                    }
                    return View(homeModel);

                }
                else
                {
                    var empresa = empresas.FirstOrDefault();
         
                    if (empresa != null)
                    {
                        homeModel.EmpresaId = empresa.Id;
                        homeModel.MainEmpresa = new EmpresaViewModel()
                        {
                            Id = empresa.Id,
                            Nome = empresa.Nome,
                            Revenda = empresa.Revenda,
                            Banco = empresa.Banco,
                            Origem = empresa.Origem,
                            Departamento = empresa.Departamento
                        };
                    }
                    return View(homeModel);
                }

            }
            catch (Exception ex)
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

        public IActionResult Salvar(int id, string empresa, string revenda, string banco, string departamento, string origem)
        {
            try
            {
                var resp = MySqlServices.UpdateEmpresa(new Empresa()
                {
                    Id = id,
                    Nome = empresa,
                    Revenda = revenda,
                    Origem = origem,
                    Departamento = departamento,
                    Banco = banco
                });

                if (!resp)
                {
                    resp = MySqlServices.AddEmpresa(new Empresa()
                    {
                        Nome = empresa,
                        Revenda = revenda,
                        Origem = origem,
                        Departamento = departamento,
                        Banco = banco
                    });
                }

                return new JsonResult(new { success = resp });
            }
            catch (Exception)
            {

                return new JsonResult(new { success = false });
            }
        }
    }
}
