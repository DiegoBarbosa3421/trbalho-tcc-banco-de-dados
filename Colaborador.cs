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
    /*
     * Classe responsavel pelas opreções de salvar , alterar, excluir, listar e pesquisar na tabela colaborador do banco de dados.
     */
    public class Colaborador
    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con ;
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void SalvarColaborador(string nomeColaborador, string contato, string endereço, string situação)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" INSERT INTO colaborador( nomeColaborador, contato, endereço, situação )");
                mysql.Append(" VALUES( @nomeColaborador, @contato, @endereço, @situação)");

                cmd.Parameters.Add(new MySqlParameter("@nomeColaborador", nomeColaborador));
                cmd.Parameters.Add(new MySqlParameter("@contato", contato));
                cmd.Parameters.Add(new MySqlParameter("@endereço", endereço));
                cmd.Parameters.Add(new MySqlParameter("@situação", situação));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
           
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao tentar salvar Colaborador " + ex);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ExcluirColaborador(int idColaborador)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" DELETE FROM colaborador");
                mysql.Append(" WHERE (idColaborador = @idColaborador)");

                cmd.Parameters.Add(new MySqlParameter("@idColaborador", idColaborador));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao escluir colaborador" +  ex);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void AlteraColaborador(int idColaborador, string nomeColaborador, string contato, string endereço, string situação)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" UPDATE colaborador ");
                mysql.Append(" SET  nomeColaborador = @nomeColaborador,contato = @contato,  endereço = @endereço, situação = @situação ");
                mysql.Append("WHERE idColaborador = @idColaborador");

                cmd.Parameters.Add(new MySqlParameter("@idColaborador", idColaborador));
                cmd.Parameters.Add(new MySqlParameter("@nomeColaborador", nomeColaborador));
                cmd.Parameters.Add(new MySqlParameter("@contato", contato));
                cmd.Parameters.Add(new MySqlParameter("@endereço", endereço));
                cmd.Parameters.Add(new MySqlParameter("@situação", situação));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao atualizar colaborador" + ex);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable RetornarColaborador()
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT * FROM colaborador ");
            

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception)
            {

                throw;
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable PesquisarColaborador(string nome)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT * FROM colaborador ");
                mysql.Append(" WHERE nomeColaborador = @nomeColaborador");

                cmd.Parameters.Add(new MySqlParameter("@nomeColaborador", nome));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao procurar Colaborador" + ex);
            }
        }


    }
}

