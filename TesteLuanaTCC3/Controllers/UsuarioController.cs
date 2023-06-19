using javax.jws;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PartyMaker3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyMaker3.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]// Ação GET para exibir a página de login
        public ActionResult Logar()
        {
            return View();
        }
        [HttpPost]// Ação POST para realizar o login
        public IActionResult Logar(string email, string senha)
        {
            Usuario user = Usuario.Logar(email, senha);// Chama o método estático Logar da classe Usuario para autenticar o usuário
            if (user != null)
            {
                // Autenticação bem-sucedida, guarda o usuário na Session
                //Guardo na Session
                // Serializa o objeto Usuario em uma string JSON
                // Armazena a string JSON na Session, usando a chave "teste"
                HttpContext.Session.SetString("teste", JsonConvert.SerializeObject(user));

                // Redireciona para a ação "Listar"
                TempData["Mensagem"] = user.Nome;
                TempData["MensagemE"] = user.Email;
                return RedirectToAction("Index", "Perfil");
            }
            else
            {
                return RedirectToAction("Index", "Home");// Autenticação falhou, redireciona de volta para a página de login
            }
        }
        //session funcionando

        



     
        [HttpPost]// Ação POST para realizar a ação de Likee
        public IActionResult Likee(int id, int id_evento)
        {
            // Obtém o usuário da Session e extrai o ID
            Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("teste"));
           

            string vaii = HttpContext.Session.GetString("talvezid");
            int valorInt;
            if (int.TryParse(vaii, out valorInt))
            {
                int i = u.Id;
                id = i; // Define o ID extraído como o valor para o parâmetro "id"

                id_evento = valorInt;

                Usuario usuario = new Usuario(id, id_evento);// Cria um novo objeto Usuario com os dados fornecidos

                usuario.likee();// Chama o método "likee" do objeto Usuario para realizar a ação de Likee
              
            }

            return RedirectToAction("Index", "Home");// Redireciona para a ação "Likee"


        }

        [HttpPost("Favorito")]
        public IActionResult Favorito(int id, int id_evento)
        {
            // Obtém o usuário da Session e extrai o ID
            Usuario u = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("teste"));
            string vaii = HttpContext.Session.GetString("talvezid");
            int valorInt;
            if (int.TryParse(vaii, out valorInt))
            {
                int i = u.Id;
                id = i; // Define o ID extraído como o valor para o parâmetro "id"

                id_evento = valorInt;

                Usuario usuario = new Usuario(id, id_evento);// Cria um novo objeto Usuario com os dados fornecidos

                usuario.Favorito();// Chama o método "likee" do objeto Usuario para realizar a ação de Likee
            }
            return RedirectToAction("Index", "Home");// Redireciona para a ação "Likee"

        }




        public IActionResult Listar()// Ação para listar os usuários
        {
            return View(Usuario.Listar());
        }

        public IActionResult Editar(int id)// Ação para exibir a página de edição de usuário
        {
            return View(Usuario.BuscarPessoa(id));
        }
        [HttpPost]// Ação POST para realizar a edição do usuário
        public IActionResult Editar(int id, string nome, string email, string senha, DateTime data_nasc)
        {
            Usuario p = new Usuario(id, nome, email, senha, data_nasc);// Cria um novo objeto Usuario com os dados fornecidos
            TempData["msg"] = p.Editar();// Chama o método "Editar" do objeto Usuario para realizar a edição
            return RedirectToAction("Listar");// Redireciona para a ação "Listar" para exibir a lista atualizada de usuários
        }

        public IActionResult Cadastrar()// Ação para exibir a página de cadastro de usuário
        {
            return View();
        }

        [HttpPost]// Ação POST para realizar o cadastro de um novo usuário
        public IActionResult Cadastrar(int id, string nome, string email, string senha, DateTime data_nasc)
        {
            Usuario p = new Usuario(id, nome, email, senha, data_nasc);// Cria um novo objeto Usuario com os dados fornecidos
            TempData["msg"] = p.Salvar();// Chama o método "Salvar" do objeto Usuario para realizar o cadastro
            return RedirectToAction("Index", "Home");// Redireciona para a ação "Listar" para exibir a lista atualizada de usuários
        }

        public IActionResult Excluir(int id, string nome, string email, string senha, DateTime data_nasc)// Ação para excluir um usuário
        {
            Usuario p = new Usuario(id, nome, email, senha, data_nasc);// Cria um novo objeto Usuario com os dados do usuário a ser excluído
            TempData["msg"] = p.Excluir(); // Chama o método "Excluir" do objeto Usuario para realizar a exclusão
            return RedirectToAction("Listar");// Redireciona para a ação "Listar" para exibir a lista atualizada de usuários
        }
    }
}
