using Conection;
using Microsoft.OData.Edm;
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
     * Classse responsavel pelas ações de salvar, alterar , excluir, listar, pesquisar da tabela ferramentas
     */
    public class Ferramentas

    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con ;
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void SalvarFerramentas(string nomeFerramenta, double valorFerramenta, Date dataAquisição)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append("INSERT INTO ferramentas_e_equipamentos (nomeFerramenta, valorFerramenta, datadeAquisição) ");
                mysql.Append("VALUES (@nomeFerramenta, @valorFerramenta, @datadeAquisição)");

                cmd.Parameters.Add(new MySqlParameter("@nomeFerramenta", nomeFerramenta));
                cmd.Parameters.Add(new MySqlParameter("@valorFerramenta", valorFerramenta));
                cmd.Parameters.Add(new MySqlParameter("@datadeAquisição", dataAquisição));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao salvar dados na tabela Ferramentas", ex);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ExcluirFerramenta(int idferramentas)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("DELETE  from ferramentas_e_equipamentos");
                mysql.Append(" WHERE (idFerramentas = @idFerramentas) ");

                cmd.Parameters.Add(new MySqlParameter("@idFerramentas", idferramentas));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao tentar excluir a ferramenta do banco" + ex);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void AlterarFerramenta(int idferamentas, string nomeFerramenta , double valorFerramenta, Date datadeAquisição)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" UPDATE ferramentas_e_equipamentos");
                mysql.Append(" SET  nomeFerramenta = @nomeFerramenta, valorFerramenta = @valorFerramenta, datadeAquisição = @datadeAquisição");
                mysql.Append(" WHERE (idFerramentas = @idFerramentas)");

              
                cmd.Parameters.Add(new MySqlParameter("@nomeFerramenta", nomeFerramenta));
                cmd.Parameters.Add(new MySqlParameter("@valorFerramenta", valorFerramenta));
                cmd.Parameters.Add(new MySqlParameter("@datadeAquisição", datadeAquisição));
                cmd.Parameters.Add(new MySqlParameter("@idFerramentas", idferamentas));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex )
            {

                throw new Exception("Erro ao atualizar registros da tabela Ferramntas" +  ex);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable ListarFeramnta()
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("SELECT * FROM ferramentas_e_equipamentos");
               
                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();
                return listarTabela;
            }
            catch (Exception ex)
            {

                throw new Exception ("Erro ao retornar sobre as ferramentas" + ex);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable PesquisarFerramentas(string nomeFerramenta)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("SELECT * FROM ferramentas_e_equipamentos");
                mysql.Append(" WHERE nomeFerramenta = @nomeFerramenta ");

                cmd.Parameters.Add(new MySqlParameter("@nomeFerramenta", nomeFerramenta));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();
                return listarTabela;
          
            }
            catch (Exception)
            {

                throw new  Exception("Erro do metodo pesquisar Ferramnetas");
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
       // Metodo usado para rettornar o id de acordo com a feeramenta expecificada
        public int Retornarid (string nomeFerramenta)
        {
            try
            {

                int id;
                con = new MySqlConnection(Conexão.stringConecçaõ());

                con.Open();

                mysql.Append("SELECT * FROM ferramentas_e_equipamentos");
                mysql.Append(" WHERE nomeFerramenta = @nomeFerramenta ");

                cmd.Parameters.Add(new MySqlParameter("@nomeFerramenta", nomeFerramenta));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                id = Convert.ToInt32(listarTabela.Rows[0]["idFerramentas"].ToString());


                con.Close();

                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
