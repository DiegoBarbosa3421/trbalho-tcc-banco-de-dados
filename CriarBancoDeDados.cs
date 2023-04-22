using Conection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
   public class CriarBancoDeDados
    {
        MySqlConnection con;
        StringBuilder mysql = new StringBuilder();
        MySqlCommand cmd ;
        public DataTable VerificarBanco(string nomeBancoDados)
        {
            DataTable listaTabela = new DataTable();
            try
            {
                cmd = new MySqlCommand();
                con = new MySqlConnection(" server = localhost;  user id = root; password = root; port = 3306; ");
                con.Open();

                mysql.Append(" SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nomeBancoDados ");
                
                cmd.Parameters.Add(new MySqlParameter("@nomeBancoDados", nomeBancoDados));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listaTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listaTabela;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro no método VerificarBanco. Caso o problema persista, entre em contato com o Administrador do Sistema."+ ex);
            }
           
        }




        public void CriarBancodeDados(string scripBancoDeDados)
        {

            con = new MySqlConnection("server = localhost; user id = root; password = root; port = 3306; ");
            try
            {
                // Abre a conexão com o servidor MySQL
                con.Open();

                // Cria um novo comando MySqlCommand e define sua propriedade Connection
                cmd = new MySqlCommand();
                
                // Lê o conteúdo do arquivo do script SQL e atribui ao comando MySqlCommand
                string script = System.IO.File.ReadAllText(scripBancoDeDados);
                cmd.CommandText = script;
                cmd.Connection = con;
                // Executa o comando MySqlCommand para criar o banco de dados
                cmd.ExecuteNonQuery();

                // Fecha a conexão com o servidor MySQL
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar banco de dados: " + ex.Message);
            }
        }
    }
}
