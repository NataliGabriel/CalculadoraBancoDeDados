using CalculadoraPekus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CalculadoraPekus.Models;


namespace CalculadoraPekus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {

            
            return View();
        }

        public IActionResult Calcular(float n1, float n2, string Operacao)
        {
            string error = string.Empty;
            float total = 0;
            BancoPostgres banco = new BancoPostgres();
            var DataHora = DateTime.Now;
          
            var Resposta = 0;
            if (Convert.ToString(n1) != "" && Convert.ToString(n2) != "")
            {
                if (Operacao == "somar")
                {
                    total = n1 + n2;
                    Operacao = "+";
                }
                else if (Operacao == "subtrair")
                {
                    total = n1 - n2;
                    Operacao = "-";

                }
                else if (Operacao == "multiplicar")
                {
                    total = n1 * n2;
                    Operacao = "*";

                }
                else if (Operacao == "dividir")
                {
                    if (n2 != 0)
                    {
                        total = n1 / n2;
                        Operacao = "/";
                    }
                    else
                    {
                        error = "''Não se pode dividir por 0''";
                        return View("Index", $"{error}");
                    }
                }
                Resposta = banco.InsereDados("insert into sc_calculadora.tb_contas(valor1, operacao, valor2, resultado, datetime) values " +
                       "           (" + n1 + ",'" + Operacao + "'," + n2 + ", " + total + ", '"+ DataHora+"')");
                if(Resposta == 1)
                {
                    ViewBag.Message = "Calculo armazenado com sucesso";
                }
                return View("Index", $"{n1} {Operacao} {n2} = {total}");


            }
            else
            {
                error = "''Valores inválidos''";
                return View("Index", $"{error}");
            }




        }

        public ActionResult Historico()
        {
            Calculadora calculadora = new Calculadora();
            return View(calculadora.Informacoes());
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
    }
}
