using com.sun.tools.@internal.ws.processor.model;
using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PartyMaker3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static com.sun.tools.@internal.xjc.reader.xmlschema.bindinfo.BIConversion;

namespace TesteLuanaTCC3.Controllers
{
    public class EventoController : Controller
    {
        static MySqlConnection con = new MySqlConnection(
       "server=10.87.100.6;database=db_eventoRB;user id=aluno; password=Senai1234");// Cria uma conexão MySqlConnection para o banco de dados


        public IActionResult ListarEvento(string categoria)
        {
            List<Evento> eventos = Evento.ListarEvento(categoria);// Obtém a lista de eventos chamando o método estático ListarEvento da classe Evento
            return View(eventos);// Retorna a view passando a lista de eventos como modelo
        }

        public IActionResult EditarEvento(int id_evento)
        {
            return View(Evento.BuscarEvento(id_evento));// Chama o método estático BuscarEvento da classe Evento para obter os detalhes do evento com o ID fornecido
        }

        [HttpPost]
        public IActionResult EditarEvento(int id_evento, string nome, string descricao, DateTime data_evento, int capacidade, string categoria, string rua, string logradouro, string cep, string cidade)
        {
            Evento p = new Evento(id_evento, nome, descricao, data_evento, capacidade, categoria, rua, logradouro, cep, cidade);// Cria um objeto Evento com os dados fornecidos
            TempData["msg"] = p.EditarEvento();// Edita o evento e armazena a mensagem de retorno na TempData
            return RedirectToAction("ListarEvento");// Redireciona para a página de listagem de eventos

        }

        public IActionResult CadastrarEvento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarEvento(int id_evento, string nome, string descricao, DateTime data_evento, int capacidade, string categoria, string rua, string logradouro, string cep, string cidade, byte[] imagem)
        {
            // Cria um objeto Evento com os dados fornecidos
            Evento p = new Evento(id_evento, nome, descricao, data_evento, capacidade, categoria, rua, logradouro, cep, cidade, imagem);
            TempData["msg"] = p.SalvarEvento();// Salva o evento e armazena a mensagem de retorno na TempData
            // Percorre todos os arquivos enviados no formulário
            foreach (IFormFile arq in Request.Form.Files)
            {
                string tipoArquivo = arq.ContentType;
                string extensao = System.IO.Path.GetExtension(arq.FileName);

                if (tipoArquivo.Contains("image") || tipoArquivo.Contains("audio"))
                {//se for imagem eu vou gravar no banco
                    // Se o arquivo for uma imagem ou áudio, é gravado no banco de dados
                    MemoryStream s = new MemoryStream();
                    arq.CopyTo(s);
                    byte[] bytesArquivo = s.ToArray();

                    try
                    {
                        con.Open();// Abre a conexão com o banco de dados
                        MySqlCommand qry = new MySqlCommand(
                            "UPDATE evento SET banner_url = @banner_url where nome = @nome", con);// Prepara o comando SQL para atualizar a coluna 'banner_url' na tabela 'evento' com a imagem do evento
                        qry.Parameters.AddWithValue("@banner_url", bytesArquivo);
                        qry.Parameters.AddWithValue("@nome", nome);

                        qry.ExecuteNonQuery();// Executa o comando SQL para atualizar a coluna 'banner_url'

                        con.Close();// Fecha a conexão com o banco de dados
                        TempData["msg"] = "Salvo com sucesso!";
                    }
                    catch (Exception ex)
                    {
                        TempData["msg"] = "Erro: " + ex.Message;
                        con.Close();
                    }
                }
                else
                {
                    // Se o arquivo não for uma imagem ou áudio, é salvo no HD
                    FileStream stream = new FileStream("C:\\Nova Pasta\\" + nome + extensao,
                        FileMode.Create);
                    arq.CopyTo(stream);
                    stream.Close();
                }
            }
            return RedirectToAction("Index","Home");// Redireciona para a página de listagem de eventos
        }
        public IActionResult ExcluirEvento(int id_evento, string nome, string descricao, DateTime data_evento, int capacidade, string categoria, string rua, string logradouro, string cep, string cidade, byte[] imagem)
        {
            // Cria um objeto Evento com os dados fornecidos
            Evento p = new Evento(id_evento, nome, descricao, data_evento, capacidade, categoria, rua, logradouro, cep, cidade, imagem);
            TempData["msg"] = p.ExcluirEvento();// Exclui o evento e armazena a mensagem de retorno na TempData
            return RedirectToAction("ListarEvento");// Redireciona para a página de listagem de eventos
        }

        public IActionResult ListaFavorito(int id)// Ação para exibir a página de edição de usuário
        {

            Evento u = JsonConvert.DeserializeObject<Evento>(HttpContext.Session.GetString("teste"));
            int i = u.Id_usuarioE;
            id = i;
            return View(Evento.ListaEventoFavi(id));
        }

        public IActionResult paginaEvento(int id)
        {
            HttpContext.Session.SetString("talvezid", JsonConvert.SerializeObject(null));
            HttpContext.Session.SetString("talvezid", JsonConvert.SerializeObject(id));
            return View(Evento.BuscarEvento(id));
        }

    }
}
