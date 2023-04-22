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
   public class Propriedade
    {
        /*
         * Classe responavel por savar, escluir, alterar, listar e pesquisar da tabela propriedades no banco de dados.
         * 
         * observação "propriedade" que e usada neste programa se trata de um expaço de terrar que pertence a algum individuo
         */

        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con;
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Salvar propreidade salva novas propriedades com base no nomePropriedade refernete ao nome da propriedade, tipoCultivo referente ao tipo de cultivo caso seja por exxemplo uma 
        // tipo unico de café ele pode ser especificado aqui caso seja variado pode ser expecificado em cada talhão, data data de aquisição da propriedade, valorPropriedade corresponde ao
        // valor pago ao comprar a mesma ou o valor que a mesma tenha nomomento, hareaHectares corresponde a harea em hectares, por fim  proprietario_idProprietario que adiciona o id do
        // proprieatrio ao registro da propriedade para fis de controle.
        public void SalvarPropriedade( string nomePropriedade , string tipoCultivo, Date  data, double valorPropriedade,  double hareaHectares, int proprietario_idProprietario)
        {
            try
            {
               con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append("INSERT INTO propriedade ( tipocultivo, nomePropriedade, dataAquisição," +
                    " hareaHectares,  valorPropriedade, Proprietario_idProprietario )");
                mysql.Append("VALUES ( @tipocultivo, @nomePropriedade, @dataAquisição," +
                    " @hareaHectares,@valorPropriedade, @Proprietario_idProprietario )");

                cmd.Parameters.Add(new MySqlParameter("@tipocultivo", tipoCultivo));
                cmd.Parameters.Add(new MySqlParameter("@nomePropriedade", nomePropriedade));
                cmd.Parameters.Add(new MySqlParameter("@dataAquisição", data));
                cmd.Parameters.Add(new MySqlParameter("@hareaHectares", hareaHectares));
                cmd.Parameters.Add(new MySqlParameter("@valorPropriedade", valorPropriedade));
                cmd.Parameters.Add(new MySqlParameter("@Proprietario_idProprietario", proprietario_idProprietario));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception ("Erro ao tentar salvar os dados na tebela propriedade" + ex);
                
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ExcluirPropriedade exclui a proprieade com base no id selecionado pelo usuario
        public void EscluirPropriedade(int idPropriedade)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" DELETE FROM propriedade");
                mysql.Append(" WHERE (idPropriedade = @idPropriedade)");
                cmd.Parameters.Add(new MySqlParameter("@idPropriedade", idPropriedade));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro no metodo excluir da tabela Propriedade" + ex);
              
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Altera a propriedade onde o id da propriedade no banco de dados  e igual idPropriedade especificado pelo usuario
        public void AlterarPropriedade(int idPropriedade, string nomePropriedade, string tipoCultivo, Date data, double valorPropriedade, double haraHectares, int Proprietario_idProprietario)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" UPDATE propriedade");
                mysql.Append(" SET   tipoCultivo = @tipoCultivo,nomePropriedade =  @nomePropriedade, dataAquisição =  dataAquisição,");
                mysql.Append(" hareaHectares= @hareaHectares, valorPropriedade = @valorPropriedade, Proprietario_idProprietario = @Proprietario_idProprietario");
                mysql.Append(" WHERE (idPropriedade =   @idPropriedade)");

                cmd.Parameters.Add(new MySqlParameter("@idPropriedade", idPropriedade));
                cmd.Parameters.Add(new MySqlParameter("@tipoCultivo", tipoCultivo));
                cmd.Parameters.Add(new MySqlParameter("@nomePropriedade", nomePropriedade));
                cmd.Parameters.Add(new MySqlParameter("@dataAquisição", data));
                cmd.Parameters.Add(new MySqlParameter("@hareaHectares", haraHectares));
                cmd.Parameters.Add(new MySqlParameter("@valorPropriedade", valorPropriedade));
                cmd.Parameters.Add(new MySqlParameter("@Proprietario_idProprietario", Proprietario_idProprietario));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception )
            {

                throw new Exception("Erro ao tentar alter o registro da tabela Propriedade");
                
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Pesquisa a propriedade com base no nome expecificado pelo usuario
        public DataTable PesquisarPropriedade(string nome)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("SELECT propriedade.*, nomeProprieatrio as proprietario FROM propriedade INNER JOIN proprietario ");
                mysql.Append(" ON Proprietario_idproprietario = idproprietario ");
                mysql.Append(" WHERE nomePropriedade = @nomePropriedade ");

                cmd.Parameters.Add(new MySqlParameter("@nomePropriedade", nome));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;

            }
            catch (Exception)
            {

                throw new Exception("Erro ao pesquisar Propriedade");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Retorna uma lista com todas as propriedades cadastradas
        public DataTable ListarPropriedade()
            {
         
               try
                 {
                    con = new MySqlConnection(Conexão.stringConecçaõ());
                    con.Open();
 
                    mysql.Append("SELECT propriedade.*, nomeProprieatrio as proprietario FROM propriedade INNER JOIN proprietario ");
                    mysql.Append(" ON Proprietario_idproprietario = idproprietario ");
                    mysql.Append(" ORDER BY Proprietario_idproprietario ");

                   cmd.CommandText = mysql.ToString();
                   cmd.Connection = con;
                   listarTabela.Load(cmd.ExecuteReader());

                   con.Close();

                   return listarTabela;

                
            }
            catch (Exception  )
            {

                throw new Exception("Erro ao retornar Propriedade");
            }

            }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
   

