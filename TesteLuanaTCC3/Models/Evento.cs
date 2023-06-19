using com.sun.net.httpserver;
using com.sun.xml.@internal.bind.v2.model.core;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using sun.tools.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyMaker3.Models
{
    public class Evento
    {

        static MySqlConnection con = new MySqlConnection(
        "server=10.87.100.6;database=db_eventoRB;user id=aluno; password=Senai1234");

        private string nome, descricao, categoria, rua, logradouro, cep, cidade;
        private int capacidade, id_evento;
        private DateTime data_evento;
        private byte[] imagem;
        private int id_usuarioE;
        
       

        public Evento(int id_evento, string nome, string descricao, DateTime data_evento, int capacidade, string categoria, string rua, string logradouro, string cep, string cidade, byte[] imagem)
        {
            this.id_evento = id_evento;
            this.nome = nome;
            this.descricao = descricao;
            this.data_evento = data_evento;
            this.capacidade = capacidade;
            this.categoria = categoria;
            this.rua = rua;
            this.logradouro = logradouro;
            this.cep = cep;
            this.cidade = cidade;
            this.Imagem = imagem;
        }
        public Evento(int id_evento, string nome, string descricao, DateTime data_evento, int capacidade, string categoria, string rua, string logradouro, string cep, string cidade)
        {
            this.id_evento = id_evento;
            this.nome = nome;
            this.descricao = descricao;
            this.data_evento = data_evento;
            this.capacidade = capacidade;
            this.categoria = categoria;
            this.rua = rua;
            this.logradouro = logradouro;
            this.cep = cep;
            this.cidade = cidade;
           
        }

        public Evento(int id_usuarioE)
        {
            this.id_usuarioE = id_usuarioE;
        }

        internal static Evento BuscarEvento(int id_evento)
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM evento where id_evento = @id_evento", con);// Cria o comando SQL para buscar o evento por ID
                qry.Parameters.AddWithValue("@id_evento", id_evento);// Adiciona o parâmetro ID ao comando SQL

                Evento p = null;// Inicializa a variável de evento como nula

                MySqlDataReader eve = qry.ExecuteReader();// Executa o comando SQL e obtém os dados em um leitor


                if (eve.Read())// Se há uma linha retornada no resultado
                {
                    byte[] imagem = (byte[])eve["banner_url"];
                    // Cria um novo objeto Evento com base nos dados obtidos do leitor
                    p = new Evento(

                    int.Parse(eve["id_evento"].ToString()),
                            eve["nome"].ToString(),
                            eve["descricao"].ToString(),
                            DateTime.Parse(eve["data_evento"].ToString()),
                            int.Parse(eve["capacidade"].ToString()),
                            eve["categoria"].ToString(),
                            eve["rua"].ToString(),
                            eve["logradouro"].ToString(),
                            eve["cep"].ToString(),
                            eve["cidade"].ToString(),
                            imagem
                        );
                }
                // p.EditarEvento();
                con.Close();// Fecha a conexão com o banco de dados
                return p;// Retorna o objeto Evento encontrado (ou nulo, se nenhum evento foi encontrado)
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();// Fecha a conexão com o banco de dados em caso de exceção
                return null;// Retorna nulo em caso de erro
            }
        }

        internal string EditarEvento()
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "UPDATE evento SET nome = @nome" +
                    ", descricao = @descricao" +
                    ", data_evento = @data_evento" +
                    ", capacidade = @capacidade" +
                    ",categoria = @categoria" +
                    ", rua = @rua" +
                    ", logradouro = @logradouro" +
                    ", cep = @cep" +
                    ", cidade = @cidade WHERE id_evento = @id_evento", con);// Cria o comando SQL para atualizar o evento por ID

                // Adiciona os parâmetros ao comando SQL com base nos valores do objeto Evento atual
                qry.Parameters.AddWithValue("@nome", this.nome);
                qry.Parameters.AddWithValue("@descricao", this.descricao);
                qry.Parameters.AddWithValue("@data_evento", this.data_evento);
                qry.Parameters.AddWithValue("@capacidade", this.capacidade);
                qry.Parameters.AddWithValue("@categoria", this.categoria);
                qry.Parameters.AddWithValue("@rua", this.rua);
                qry.Parameters.AddWithValue("@logradouro", this.logradouro);
                qry.Parameters.AddWithValue("@cep", this.cep);
                qry.Parameters.AddWithValue("@cidade", this.cidade);
                qry.Parameters.AddWithValue("@id_evento", this.id_evento);

                qry.ExecuteNonQuery();// Executa o comando SQL para atualizar o evento no banco de dados
                con.Close();// Fecha a conexão com o banco de dados
                return "Editado com sucesso";// Retorna uma mensagem de sucesso
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();// Fecha a conexão com o banco de dados em caso de exceção
                return e.Message;// Retorna a mensagem de exceção
            }
        }

        internal static List<Evento> ListarEvento(string categoria)
        {

            List<string> listaR = new List<string>();
            listaR.Add("SELECT * FROM evento LIMIT 6");
            listaR.Add("SELECT * FROM EVENTO WHERE categoria = @categoria");
                string primeiraMensagem = listaR[0];
                string segundaMensagem = listaR[1];
            int resultado = 0;
            if (categoria != null)
            {
                resultado = 1;
                
            }
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                  listaR[resultado]  , con);// Cria o comando SQL para selecionar todos os eventos
                qry.Parameters.AddWithValue("@categoria", categoria);
                List<Evento> lista = new List<Evento>();// Cria uma lista vazia para armazenar os eventos


                MySqlDataReader leitor = qry.ExecuteReader();// Executa o comando SQL e obtém os dados em um leitor

                while (leitor.Read())// Enquanto houver linhas retornadas no resultado
                {
                    // Lê os dados do leitor e cria um novo objeto Evento com base nos valores obtidos
                    byte[] imagem = (byte[])leitor["banner_url"];
                    lista.Add(new Evento(
                         int.Parse(leitor["id_evento"].ToString()),
                            leitor["nome"].ToString(),
                            leitor["descricao"].ToString(),
                            DateTime.Parse(leitor["data_evento"].ToString()),
                            int.Parse(leitor["capacidade"].ToString()),
                            leitor["categoria"].ToString(),
                            leitor["rua"].ToString(),
                            leitor["logradouro"].ToString(),
                            leitor["cep"].ToString(),
                            leitor["cidade"].ToString(),
                            imagem
                        ));
                }
                con.Close();// Fecha a conexão com o banco de dados
                return lista;// Retorna a lista de eventos encontrados

            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();// Fecha a conexão com o banco de dados em caso de exceção
                return null;// Retorna nulo em caso de erro
            }
        }

        internal static List<Evento> ListaEventoFavi(int id)
        {
            try
            {

                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "SELECT e.* FROM evento AS e JOIN favorito AS f ON e.id_evento = f.FK_EVENTO_id_evento WHERE f.FK_USUARIO_id_usuario = @id_usuario", con);// Cria o comando SQL para selecionar todos os eventos
                qry.Parameters.AddWithValue("@id_usuario", id);

                List<Evento> lista = new List<Evento>();// Cria uma lista vazia para armazenar os eventos


                MySqlDataReader leitor = qry.ExecuteReader();// Executa o comando SQL e obtém os dados em um leitor

                while (leitor.Read())// Enquanto houver linhas retornadas no resultado
                {
                    // Lê os dados do leitor e cria um novo objeto Evento com base nos valores obtidos
                    byte[] imagem = (byte[])leitor["banner_url"];
                    lista.Add(new Evento(
                         int.Parse(leitor["id_evento"].ToString()),
                            leitor["nome"].ToString(),
                            leitor["descricao"].ToString(),
                            DateTime.Parse(leitor["data_evento"].ToString()),
                            int.Parse(leitor["capacidade"].ToString()),
                            leitor["categoria"].ToString(),
                            leitor["rua"].ToString(),
                            leitor["logradouro"].ToString(),
                            leitor["cep"].ToString(),
                            leitor["cidade"].ToString(),
                            imagem
                        ));
                }
                con.Close();// Fecha a conexão com o banco de dados
                return lista;// Retorna a lista de eventos encontrados

            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();// Fecha a conexão com o banco de dados em caso de exceção
                return null;// Retorna nulo em caso de erro
            }
        }

        //Listar Eventos Where

        //internal static List<Evento> IndexWhere(string categoria)
        //{
        //    try
        //    {

        //        con.Open();// Abre a conexão com o banco de dados
        //        MySqlCommand qry = new MySqlCommand(
        //            "SELECT * FROM EVENTO WHERE categoria = @categoria", con);// Cria o comando SQL para selecionar todos os eventos
        //        qry.Parameters.AddWithValue("@categoria", categoria);

        //        List<Evento> lista = new List<Evento>();// Cria uma lista vazia para armazenar os eventos


        //        MySqlDataReader leitor = qry.ExecuteReader();// Executa o comando SQL e obtém os dados em um leitor

        //        while (leitor.Read())// Enquanto houver linhas retornadas no resultado
        //        {
        //            // Lê os dados do leitor e cria um novo objeto Evento com base nos valores obtidos
        //            byte[] imagem = (byte[])leitor["banner_url"];
        //            lista.Add(new Evento(
        //                 int.Parse(leitor["id_evento"].ToString()),
        //                    leitor["nome"].ToString(),
        //                    leitor["descricao"].ToString(),
        //                    DateTime.Parse(leitor["data_evento"].ToString()),
        //                    int.Parse(leitor["capacidade"].ToString()),
        //                    leitor["categoria"].ToString(),
        //                    leitor["rua"].ToString(),
        //                    leitor["logradouro"].ToString(),
        //                    leitor["cep"].ToString(),
        //                    leitor["cidade"].ToString(),
        //                    imagem
        //                ));
        //        }
        //        con.Close();// Fecha a conexão com o banco de dados
        //        return lista;// Retorna a lista de eventos encontrados

        //    }
        //    catch (Exception e)
        //    {
        //        if (con.State == System.Data.ConnectionState.Open)
        //            con.Close();// Fecha a conexão com o banco de dados em caso de exceção
        //        return null;// Retorna nulo em caso de erro
        //    }
        //}


        internal string ExcluirEvento()
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "DELETE FROM evento WHERE id_evento = @id_evento", con);// Cria o comando SQL para excluir o evento por ID
                qry.Parameters.AddWithValue("@id_evento", this.id_evento);// Adiciona o parâmetro ID ao comando SQL

                qry.ExecuteNonQuery();// Executa o comando SQL para excluir o evento no banco de dados
                con.Close();// Fecha a conexão com o banco de dados
                return "Excluido com sucesso";// Retorna uma mensagem de sucesso
            }
            catch (Exception e)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();// Fecha a conexão com o banco de dados em caso de exceção
                return e.Message;// Retorna a mensagem de exceção
            }
        }

        internal string SalvarEvento()

        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "INSERT INTO evento (nome, descricao, data_evento, capacidade,categoria, rua, logradouro, cep, cidade ) VALUES(@nome, @descricao, @data_evento, @capacidade, @categoria, @rua, @logradouro, @cep, @cidade)", con);// Prepara o comando SQL para inserir os dados na tabela 'evento'
                 // Define os parâmetros do comando SQL com os valores dos atributos do objeto atual
                qry.Parameters.AddWithValue("@nome", this.nome);
                qry.Parameters.AddWithValue("@descricao", this.descricao);
                qry.Parameters.AddWithValue("@data_evento", this.data_evento);
                qry.Parameters.AddWithValue("@capacidade", this.capacidade);
                qry.Parameters.AddWithValue("@categoria", this.categoria);
                qry.Parameters.AddWithValue("@rua", this.rua);
                qry.Parameters.AddWithValue("@logradouro", this.logradouro);
                qry.Parameters.AddWithValue("@cep", this.cep);
                qry.Parameters.AddWithValue("@cidade", this.cidade);

                qry.ExecuteNonQuery();// Executa o comando SQL para inserir os dados na tabela
                con.Close();// Fecha a conexão com o banco de dados
                return "Inserido com sucesso";// Retorna uma mensagem de sucesso

            }
            catch (Exception e)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return e.Message;// Retorna a mensagem de erro caso ocorra uma exceção
            }
        }
        public string Nome { get => nome; set => nome = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public string Rua { get => rua; set => rua = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public int Capacidade { get => capacidade; set => capacidade = value; }
        public int Id_evento { get => id_evento; set => id_evento = value; }
        public DateTime Data_evento { get => data_evento; set => data_evento = value; }

        public string ResquestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(ResquestId);

        public byte[] Imagem { get => imagem; set => imagem = value; }
        public int Id_usuarioE { get => id_usuarioE; set => id_usuarioE = value; }
    }
}
