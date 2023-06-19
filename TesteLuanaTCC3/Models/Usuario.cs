using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using sun.tools.tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyMaker3.Models
{
    public class Usuario
    {
        static MySqlConnection con = new MySqlConnection(
        "server=10.87.100.6;database=db_eventoRB;user id=aluno; password=Senai1234");

        private string nome, email, senha;
        private int id;
        private int id_evento;
        
        private DateTime data_nasc;


        public Usuario(int id, string nome = "", string email = "", string senha = "", DateTime data_nasc = default(DateTime))
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.data_nasc = data_nasc;
        }



        public Usuario(int id, int id_evento)
        {
            this.id = id;
            this.id_evento = id_evento;
        }
        private Usuario()
        {

        }


        internal static Usuario BuscarPessoa(int id)
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM usuario where id_usuario = @id_usuario", con);// Prepara o comando SQL para buscar os dados da tabela 'usuario' com base no ID fornecido
                qry.Parameters.AddWithValue("@id_usuario", id);// Adiciona o parâmetro ID ao comando SQL

                Usuario p = null;// Inicializa a variável de evento como nula

                MySqlDataReader leitor = qry.ExecuteReader();// Executa o comando SQL e obtém um leitor de dados

                if (leitor.Read())
                {
                    // Se houver dados retornados, cria um novo objeto Usuario com base nas informações obtidas do leitor de dados
                    p = new Usuario(
                            int.Parse(leitor["id_usuario"].ToString()),
                            leitor["nome"].ToString(),
                            leitor["email"].ToString(),
                            leitor["senha"].ToString(),
                            
                            DateTime.Parse(leitor["data_nasc"].ToString())
                           

                        );
                }
                con.Close();/// Fecha a conexão com o banco de dados
                return p;
            }
            catch (Exception e)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;// Retorna null em caso de erro
            }
        }

        internal string Editar()
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "UPDATE usuario SET nome = @nome,email = @email,senha = @senha, cep= @cep, data_nasc = @data_nasc WHERE id_usuario = @id_usuario", con);// Prepara o comando SQL para atualizar os dados do usuário na tabela 'usuario' com base no ID do usuário
                
                // Define os parâmetros do comando SQL com os valores dos atributos do objeto atual                                                                                                                                            // Define os parâmetros do comando SQL com os valores dos atributos do objeto atual
                qry.Parameters.AddWithValue("@id_usuario", this.id);
                qry.Parameters.AddWithValue("@nome", this.nome);
                qry.Parameters.AddWithValue("@email", this.email);
                qry.Parameters.AddWithValue("@senha", this.senha);
                
                qry.Parameters.AddWithValue("@data_nasc", this.data_nasc);

                qry.ExecuteNonQuery();// Executa o comando SQL para atualizar os dados na tabela
                con.Close();// Fecha a conexão com o banco de dados
                return "Editado com sucesso";// Retorna uma mensagem de sucesso
            }
            catch (Exception e)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();// Fecha a conexão com o banco de dados
                return e.Message;// Retorna a mensagem de erro caso ocorra uma exceção
            }
        }

        internal static List<Usuario> Listar()
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "SELECT * FROM usuario", con);// Prepara o comando SQL para buscar todos os dados da tabela 'usuario'

                List<Usuario> lista = new List<Usuario>();

                MySqlDataReader leitor = qry.ExecuteReader();// Executa o comando SQL e obtém um leitor de dados

                while (leitor.Read())
                {
                    // Enquanto houver dados retornados, cria um novo objeto Usuario com base nas informações obtidas do leitor de dados e adiciona-o à lista
                    lista.Add(new Usuario(
                        int.Parse(leitor["id_usuario"].ToString()),
                        leitor["nome"].ToString(),
                        leitor["email"].ToString(),
                        leitor["senha"].ToString(),
                     
                        DateTime.Parse(leitor["data_nasc"].ToString())
                       


                        ));
                }
                con.Close();// Fecha a conexão com o banco de dados
                return lista;

            }
            catch (Exception e)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;// Retorna null em caso de erro
            }

        }

        internal string Excluir()
        {
            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "DELETE FROM usuario WHERE id_usuario = @id_usuario", con);// Prepara o comando SQL para excluir o usuário da tabela 'usuario' com base no ID do usuário
                qry.Parameters.AddWithValue("@id_usuario", this.id);// Define o parâmetro do comando SQL com o valor do ID do usuário

                qry.ExecuteNonQuery();// Executa o comando SQL para excluir o usuário da tabela
                con.Close();// Fecha a conexão com o banco de dados
                return "Excluido com sucesso";
            }
            catch (Exception e)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return e.Message;// Retorna a mensagem de erro caso ocorra uma exceção
            }
        }

        internal string Salvar()
        {

            try
            {
                con.Open();// Abre a conexão com o banco de dados
                MySqlCommand qry = new MySqlCommand(
                    "INSERT INTO usuario ( nome, email, senha, data_nasc ) VALUES(@nome, @email, @senha, @data_nasc)", con);// Prepara o comando SQL para inserir um novo usuário na tabela 'usuario'

                // Define os parâmetros do comando SQL com os valores dos atributos do objeto atual
                qry.Parameters.AddWithValue("@nome", this.nome);
                qry.Parameters.AddWithValue("@email", this.email);
                qry.Parameters.AddWithValue("@senha", this.senha);
                qry.Parameters.AddWithValue("@data_nasc", this.data_nasc);

                qry.ExecuteNonQuery();// Executa o comando SQL para inserir o novo usuário na tabela
                con.Close();// Fecha a conexão com o banco de dados
                return "Inserido com sucesso";
            }
            catch (Exception e)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return e.Message;// Retorna a mensagem de erro caso ocorra uma exceção
            }
        }
        internal static Usuario Logar(string email, string senha)
        {
            try
            {

                con.Open();// Abre a conexão com o banco de dados
                Usuario usuario = null;

                MySqlCommand query = new MySqlCommand("select * from usuario where email=@email and senha=@senha;", con);// Prepara o comando SQL para buscar um usuário na tabela 'usuario' com base no email e senha fornecidos
                query.Parameters.AddWithValue("@email", email);// Define os parâmetros do comando SQL com os valores do email e senha fornecidos
                query.Parameters.AddWithValue("@senha", senha);
                //query.Parameters.AddWithValue("@senha", senha);
                MySqlDataReader reader = query.ExecuteReader();// Executa o comando SQL e obtém um leitor de dados

                if (reader.Read())
                {
                    // Se o leitor de dados tiver registros, cria um novo objeto Usuario com base nas informações obtidas do leitor de dados
                    usuario = new Usuario(
                         int.Parse(reader["id_usuario"].ToString()),
                        reader["nome"].ToString(),
                        reader["email"].ToString(),
                        reader["senha"].ToString(),
                       
                        DateTime.Parse(reader["data_nasc"].ToString())
                       
                    );
                    con.Close();// Fecha a conexão com o banco de dados
                    return usuario;
                }
                usuario.Senha = reader.GetString("senha");// Se o leitor de dados não tiver registros, significa que o email ou senha estão incorretos

                if (!usuario.Senha.Equals(senha))
                {
                    con.Close();
                    return null;
                }
               
                con.Close();
                return usuario;
            }
            catch (Exception ex)
            {
                // Verifica se a conexão está aberta e a fecha
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close(); 
                return null;// Retorna null em caso de erro
            }

        }
        public Boolean likee()
        {
            try
            {
                con.Open();

                // Verificar se o like já existe
                MySqlCommand checkQuery = new MySqlCommand("SELECT FK_USUARIO_id_usuario FROM likee WHERE FK_USUARIO_id_usuario = @id_usuario AND FK_EVENTO_id_evento = @id_evento", con);
                checkQuery.Parameters.AddWithValue("@id_usuario", this.id);
                checkQuery.Parameters.AddWithValue("@id_evento", this.id_evento); // Substitua pelo valor correto do ID do evento
                object result = checkQuery.ExecuteScalar();

                if (result != null) // Se o like já existe
                {
                    // Deletar o like existente
                    MySqlCommand deleteQuery = new MySqlCommand("DELETE FROM likee WHERE FK_USUARIO_id_usuario = @FK_USUARIO_id_usuario", con);
                    deleteQuery.Parameters.AddWithValue("@FK_USUARIO_id_usuario", this.id);
                    deleteQuery.ExecuteNonQuery();
                    return true;
                }
               

                    // Inserir o novo like
                    MySqlCommand insertQuery = new MySqlCommand("INSERT INTO likee (FK_USUARIO_id_usuario, FK_EVENTO_id_evento) VALUES (@id_usuario, @id_evento)", con);
                    insertQuery.Parameters.AddWithValue("@id_usuario", this.id);
                    insertQuery.Parameters.AddWithValue("@id_evento", this.id_evento); // Substitua pelo valor correto do ID do evento
                    insertQuery.ExecuteNonQuery();

                    return true;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public Boolean Favorito()
        {
            try
            {
                con.Open();

                // Verificar se o like já existe
                MySqlCommand checkQuery = new MySqlCommand("SELECT FK_USUARIO_id_usuario FROM favorito WHERE FK_USUARIO_id_usuario = @id_usuario AND FK_EVENTO_id_evento = @id_evento", con);
                checkQuery.Parameters.AddWithValue("@id_usuario", this.id);
                checkQuery.Parameters.AddWithValue("@id_evento", this.id_evento); // Substitua pelo valor correto do ID do evento
                object result = checkQuery.ExecuteScalar();

                if (result != null) // Se o like já existe
                {
                    // Deletar o like existente
                    MySqlCommand deleteQuery = new MySqlCommand("DELETE FROM favorito WHERE FK_USUARIO_id_usuario = @FK_USUARIO_id_usuario", con);
                    deleteQuery.Parameters.AddWithValue("@FK_USUARIO_id_usuario", this.id);
                    deleteQuery.ExecuteNonQuery();
                    return true;
                }


                // Inserir o novo like
                MySqlCommand insertQuery = new MySqlCommand("INSERT INTO favorito (FK_USUARIO_id_usuario, FK_EVENTO_id_evento) VALUES (@id_usuario, @id_evento)", con);
                insertQuery.Parameters.AddWithValue("@id_usuario", this.id);
                insertQuery.Parameters.AddWithValue("@id_evento", this.id_evento); // Substitua pelo valor correto do ID do evento
                insertQuery.ExecuteNonQuery();

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        


        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
       
        public DateTime Data_nasc { get => data_nasc; set => data_nasc = value; }
        public int Id_evento { get => id_evento; set => id_evento = value; }
    }
}
