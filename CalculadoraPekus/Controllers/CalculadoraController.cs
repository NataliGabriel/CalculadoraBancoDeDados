using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculadoraPekus.Models;

namespace CalculadoraPekus.Controllers
{
    public class CalculadoraController : Controller
    {
        public ActionResult Index()
        {


             Calculadora calculadora = new Calculadora();
             return View(calculadora.Informacoes());

           
        }
    }
}
