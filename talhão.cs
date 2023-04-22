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
    public class talhão

        /*
         * Classe responsavel pelas ações salvar, exclui, alterar, listar, pesquisar da tabela talhão
         * 
         * observção "Talhão" se refere a uma repartição de uma propriedade agricola nomalmente dividida por estradas
         */

    {

        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con;

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //SalvarTalhão salva no banco o talhão com base no nomeTalhão que e sua identificação por exemplo talhão 1 ou talhão A, hareaHectares corresponde a area em Hectares 
        // que o talhão abrange, o tipo de palanta caso seja uma propriedade onde se tem plantas variadas como panta de café conilon, catucai ente outras, dataPlantio corresponde a data em que foi
        // plantado o café e o propriedade_idpropriedade que coresponde ao id da propriedade usado para fins de controle

        public void SalvarTalhão(string nomeTalhão, double hareaHectare, string tipodePlanta, double espaçamento,int quantidadedePlantas, Date dataPlantio, int propriedade_idpropriedade)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" INSERT INTO talhão( nomeTalhão, hareaHectares, tipodePlanta, espaçamento, dataPlantio, quantidadedePlantas, Propriedade_idPropriedade) ");
                mysql.Append(" VALUES ( @nomeTalhão, @hareaHectares, @tipodePlanta, @espaçamento, @dataPlantio, @quantidadedePlantas, @Propriedade_idPropriedade) ");

                cmd.Parameters.Add(new MySqlParameter("@nomeTalhão", nomeTalhão));
                cmd.Parameters.Add(new MySqlParameter("@hareaHectares", hareaHectare));
                cmd.Parameters.Add(new MySqlParameter("@tipodePlanta", tipodePlanta));
                cmd.Parameters.Add(new MySqlParameter("@espaçamento", espaçamento));
                cmd.Parameters.Add(new MySqlParameter("@dataPlantio", dataPlantio));
                cmd.Parameters.Add(new MySqlParameter("@quantidadedePlantas", quantidadedePlantas));
                cmd.Parameters.Add(new MySqlParameter("@Propriedade_idPropriedade", propriedade_idpropriedade));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {

                throw new Exception("Erro ao tentar efetuar o salvamento do talhão  " );
                
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Exclui o talhão com base no id especificado
        public void ExcluirTalhão(int idTalhão )
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append(" DELETE FROM talhão ");
                mysql.Append(" WHERE (idTalhão = @idTalhão)");

                cmd.Parameters.Add(new MySqlParameter("@idTalhão", idTalhão));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {

                throw new Exception("Erro ao excluir talhão");

            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Altera o talhão com os novos dados onde o id informado pelo usuario e igual ao idTalhão no banco de daos 
        public void AlterarTalhão(int idTalhão, string nomeTalhão, double hareaHectare, string tipodePlanta, double espaçamento, Date dataPlantio, int quantidadedePlantas, int propriedade_idpropriedade)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append("UPDATE  talhão");
                mysql.Append(" SET  nomeTalhão = @nomeTalhão, hareaHectares = @hareaHectares,tipodePlanta = @tipodePlanta, espaçamento = @espaçamento, dataPlantio = @dataPlantio," +
                    "  quantidadedePlantas = @quantidadedePlantas, Propriedade_idPropriedade = @Propriedade_idPropriedade ");
                mysql.Append(" WHERE ( idTalhão = @idTalhão )");

                cmd.Parameters.Add(new MySqlParameter("@idTalhão", idTalhão));
                cmd.Parameters.Add(new MySqlParameter("@nomeTalhão", nomeTalhão));
                cmd.Parameters.Add(new MySqlParameter("@hareaHectares", hareaHectare));
                cmd.Parameters.Add(new MySqlParameter("@tipodePlanta", tipodePlanta));
                cmd.Parameters.Add(new MySqlParameter("@espaçamento", espaçamento));
                cmd.Parameters.Add(new MySqlParameter("@dataPlantio", dataPlantio));
                cmd.Parameters.Add(new MySqlParameter("@quantidadedePlantas", quantidadedePlantas));
                cmd.Parameters.Add(new MySqlParameter("@Propriedade_idPropriedade", propriedade_idpropriedade));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception )
            {

                throw new Exception("Erro no metodo alterar da tabela talhão ");
               
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Relaiza uma pesquisa por talhão e retona o talhão com o nome informado pelo usuario
        public DataTable PesquisarTalhão(string nomeTalhão)
        {
            try

            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" Select talhão.*, nomePropriedade as propriedade from talhão inner join propriedade");
                mysql.Append(" on Propriedade_idPropriedade = idPropriedade");
                mysql.Append(" WHERE (nomeTalhão = @nomeTalhão)");
                mysql.Append("  order by idTalhão desc");

                cmd.Parameters.Add(new MySqlParameter("@nomeTalhão", nomeTalhão));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                
                    return listarTabela;
                
            }
            catch (Exception )
            {

                throw new Exception ("Erro ao pesquisar talhão" );
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Retorna uma lista de talhões cadastrados
        public DataTable RetornarTalhão()
        {
            try

            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" Select talhão.*, nomePropriedade as propriedade from talhão inner join propriedade");
                mysql.Append(" on Propriedade_idPropriedade = idPropriedade");

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();
                return listarTabela;
            }
            catch (Exception)
            {

                throw new Exception("Erro ao retornar lista talhão");
            }
        }

       
    }
}
