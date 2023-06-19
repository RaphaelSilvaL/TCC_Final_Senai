using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PartyMaker3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteLuanaTCC3.Controllers
{
    public class PerfilController : Controller
    {
        public IActionResult Index()
        {
            Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("teste"));
            String te = u.Nome;
            String te2 = u.Email;
            int id = u.Id;
           


            TempData["Mensagem"] = te;
            TempData["MensagemE"] = te2;
            return View(Evento.ListaEventoFavi(id));

        }
    }
}
