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
    * Classe contendo os metodos cadastro, alterar, excluir, listare e pesquisar que fazem operações na tabela serviços do banco de dados  
    */
    public class CadastroServiço
    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con;
      //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void CadastrarServiços(string tipoServiço, double valor, Date data)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" INSERT INTO cadastroserviço( tiposerviço, valor, data)");
                mysql.Append(" VALUES ( @tipoServiço, @valor, @data)");

                cmd.Parameters.Add(new MySqlParameter("@tipoServiço", tipoServiço));
                cmd.Parameters.Add(new MySqlParameter("@valor", valor));
                cmd.Parameters.Add(new MySqlParameter("@data", data));


                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception ex)
            {

                throw new Exception ("Erro ao tentar cadastrar o serviço" + ex);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ExcluirServiços(int idServiço)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" DELETE FROM cadastroserviço");
                mysql.Append(" WHERE idserviço = @idserviço");

                cmd.Parameters.Add(new MySqlParameter("@idserviço", idServiço));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao excluir serviço" + ex);
            }

        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void AlterarServiço(int idServiço, string tipoServiço, double valor, Date data)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("UPDATE cadastroserviço");
                mysql.Append(" SET  tipoServiço = @tipoServiço, valor = @valor, data = @data");
                mysql.Append(" WHERE idserviço = @idserviço");

                cmd.Parameters.Add(new MySqlParameter("@idserviço", idServiço));
                cmd.Parameters.Add(new MySqlParameter("@tipoServiço", tipoServiço));
                cmd.Parameters.Add(new MySqlParameter("@valor", valor));
                cmd.Parameters.Add(new MySqlParameter("@data", data));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao alterar Registro da tabela Serviços" + ex);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable ListarServiço()
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT * FROM cadastroserviço ");
                mysql.Append(" ORDER BY  idserviço desc ;");

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;

                
            }
            catch (Exception ex)
            {

                throw new Exception("Erro refernete ao metodo retornar serviços"+ ex);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable PesquisarServiço(string serviço)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT * FROM cadastroserviço ");
                mysql.Append(" WHERE (tiposerviço = @tiposerviço)");
                mysql.Append(" ORDER BY  idserviço desc ;");

                cmd.Parameters.Add(new MySqlParameter("@tiposerviço", serviço));

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
    }
}
