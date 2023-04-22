using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conection;
using MySql.Data.MySqlClient;

namespace Database
{
    /*
     * Classe responsavel pelas operações de salvar, alterar, excluir, listar, logim e pesquisar da tabela proprietario
     * 
     * observação nesta parte trata-se o proprietario e usuario do sistema como sendo o mesmo individuo
     * 
     */
    public class Proprietario

    {

        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();
        MySqlConnection con;
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //SalvarPropreietario responsavel por salvar o proprietario/usuario do sistema com base em um nomeProprietario que tambem e o nome do usaurio, logim que e a senha de entarda, e cpf
        public void salvarProprietario(string nomeProprieatrio, string logim, string cpf)
        {
            try
            { 

                MySqlConnection con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append(" INSERT INTO proprietario (nomeProprieatrio, logim, cpf)");
                mysql.Append(" VALUES  (@nomeProprieatrio, @logim, @cpf )");

                cmd.Parameters.Add(new MySqlParameter("@nomeProprieatrio", nomeProprieatrio));
                cmd.Parameters.Add(new MySqlParameter("@logim", logim));
                cmd.Parameters.Add(new MySqlParameter("@cpf", cpf));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex ) { 
                throw new Exception("Erro no metodo salvar da classe proprietario " +ex);
            }



        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Metodo que exclui o proprietario do sistema com base no idProprietario fornecido
        public void ExcluirProprietario(int idProprietario)
        {
            try
            {

                con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append("DELETE FROM proprietario ");
                mysql.Append("WHERE  (idProprietario = @idProprietario)");
                cmd.Parameters.Add(new MySqlParameter("@idProprietario", idProprietario));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Altera o proprietario onde o id for igual ao  IdProprietario
        public void AlterarProprietario(int IdProprietario, string nomeProprieatrio, string logim)
        {
            try
            {

                con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append("UPDATE proprietaio");
                mysql.Append(" SET nomeProprieatrio = @nomeProprieatrio, logim = @logim,");
                mysql.Append(" WHERE (idProprietario = @idProprietario,)");

                cmd.Parameters.Add(new MySqlParameter("@idProprietario", IdProprietario));
                cmd.Parameters.Add(new MySqlParameter("@nomeProprieatrio", nomeProprieatrio));
                cmd.Parameters.Add(new MySqlParameter("@logim", logim));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //LogimProprietario seleciona o usuário e retorna aquele que informamos o login e senha.
        public DataTable logimProprietario(String nomeProprieatrio, string senha) 
        {
            try
            {


                con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append("SELECT * FROM proprietario ");
                mysql.Append(" WHERE (nomeProprieatrio = @nomeProprieatrio AND logim = @logim)");

                cmd.Parameters.Add(new MySqlParameter("@nomeProprieatrio", nomeProprieatrio));
                cmd.Parameters.Add(new MySqlParameter("@logim", senha));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;



            }
            catch (Exception )
            {
                throw new Exception("Ocorreu um erro no método Logim. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //RetornaProprietario retorna uma lista de Proprieatrio/Usuariocdstrados no sistema
        public DataTable RetornarProprietario()
        {
            try
            {

                con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append("SELECT * FROM proprietario");

                
           
                
                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;


            }
            catch (Exception )
            {
                throw new Exception(" Ocorreu um erro no método Listar Proprietsrio. Caso o problema persista, entre em contato com o Administrador do Sistema." );
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //RetornaProprietario_id retorna o id do proprietario onde o nomeProprieatrio e igual ao nome fornecido pelo usuario
        public DataTable RetornarProprietario_id(string nomeProprieatrio)
        {
            try
            {

                con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append("SELECT * FROM proprietario");
                mysql.Append(" WHERE(nomeProprieatrio = @nomeProprieatrio)");

                cmd.Parameters.Add(new MySqlParameter("@nomeProprieatrio", nomeProprieatrio));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;


            }
            catch (Exception )
            {
                throw new Exception("Ocorreu um erro no método Listar Proprietsrio. Caso o problema persista, entre em contato com o Administrador do Sistema." );
            }
        }



    }
}
