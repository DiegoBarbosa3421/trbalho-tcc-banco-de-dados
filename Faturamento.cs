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
       * Classe responsavel pelos metodos salvar, alterar, excluir, Listar, da tabela faturamento 
       */
    public class Faturamento
    { 
        MySqlCommand cmd;
        DataTable listarTabela;
        StringBuilder mysql;

        MySqlConnection con;
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Salvar_Faturamento(double varorTotalArrecadado, double valorUnitarioArrecadado, DateTime dataFAturamento, double quantidadeparaFaturamento,string tipoDeCafé, int idTalhão)
        {
            try
            {
                cmd = new MySqlCommand();
                mysql = new StringBuilder();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" INSERT INTO faturamento(valorUnitarioArrecadado , valorTotalArrecadado, dataFAturamento, quantidadeFaturamento, tipoDeCafé,  Talhão_idTalhão)");
                mysql.Append(" values(@valorUnitarioArrecadado, @valorTotalArrecadado, @dataFAturamento, @quantidadeFaturamento, @tipoDeCafé, @Talhão_idTalhão)");

                cmd.Parameters.Add(new MySqlParameter("@valorTotalArrecadado", varorTotalArrecadado));
                cmd.Parameters.Add(new MySqlParameter("@valorUnitarioArrecadado", valorUnitarioArrecadado));
                cmd.Parameters.Add(new MySqlParameter("@dataFAturamento", dataFAturamento));
                cmd.Parameters.Add(new MySqlParameter("@quantidadeFaturamento", quantidadeparaFaturamento));
                cmd.Parameters.Add(new MySqlParameter("@tipoDeCafé", tipoDeCafé));
                cmd.Parameters.Add(new MySqlParameter("@Talhão_idTalhão", idTalhão));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao tentar salvar os dados na tabela de faturamento" + ex);
            }

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Excluir_Faturamento(int idFaturamento)
        {
            try
            {
                cmd = new MySqlCommand();
                mysql = new StringBuilder();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" delete from faturamento");
                mysql.Append(" where (idFaturamento = @idFaturamento) ");


                cmd.Parameters.Add(new MySqlParameter("@idFaturamento", idFaturamento));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception)
            {

                throw new Exception("Erro ao tentar excluir registro");
            }

        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Alterar_Faturamento(int idFaturamento, double varorTotalArrecadado, double valorUnitarioArrecadado, DateTime dataFAturamento, double quantidadeparaFaturamento, string tipoDeCafé, int idTalhão)
        {
            try
            {
                cmd = new MySqlCommand();
                mysql = new StringBuilder();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("UPDATE faturamento");
                mysql.Append(" SET valorUnitarioArrecadado = @valorUnitarioArrecadado , valorTotalArrecadado = @valorTotalArrecadado, dataFAturamento = @dataFAturamento, quantidadeFaturamento = @quantidadeFaturamento, tipoDeCafé = @tipoDeCafé, Talhão_idTalhão = @Talhão_idTalhão ");
                mysql.Append(" WHERE (idFaturamento = @idFaturamento)");

                cmd.Parameters.Add(new MySqlParameter("@idFaturamento", idFaturamento));
                cmd.Parameters.Add(new MySqlParameter("@valorTotalArrecadado", varorTotalArrecadado));
                cmd.Parameters.Add(new MySqlParameter("@valorUnitarioArrecadado", valorUnitarioArrecadado));
                cmd.Parameters.Add(new MySqlParameter("@dataFAturamento", dataFAturamento));
                cmd.Parameters.Add(new MySqlParameter("@quantidadeFaturamento", quantidadeparaFaturamento));
                cmd.Parameters.Add(new MySqlParameter("@tipoDeCafé", tipoDeCafé));
                cmd.Parameters.Add(new MySqlParameter("@Talhão_idTalhão", idTalhão));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao atualizar registro databela Finaceiro" + ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable RetornarFaturamento()
        {
            try
            {
                cmd = new MySqlCommand();
                mysql = new StringBuilder();
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("  SELECT faturamento.idFaturamento, valorUnitarioArrecadado, valorTotalArrecadado, tipoDeCafé, dataFAturamento, quantidadeFaturamento , nomeTalhão as nomeTalhão");
                mysql.Append("  FROM faturamento inner join talhão on idTalhão = Talhão_idTalhão");
        
                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception )
            {

                throw new Exception("Erro ao retornar dados da tabela Faturamento");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      //Este metodo retorna a quantidade que foi produzida com base no talhão, ano , e observação que contem uma ddefinição de "Descascado ou Despolpado " que são o café ja em estado de comercialização
        public DataTable Retornar_Quatidade_Produzida(string talhão, string observação, DateTime ano) {

            try
            {
                cmd = new MySqlCommand();
                mysql = new StringBuilder();
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" select  SUM(quantidade) as totalProduzido, YEAR(dataTermino) as ano");
                mysql.Append("  FROM talhão INNER JOIN produção ON talhão.idTalhão = produção.Talhão_idTalhão");
                mysql.Append("  WHERE observação = @observação AND YEAR(dataTermino) = @ano AND nomeTalhão = @nomeTalhão ");
                mysql.Append("   GROUP BY talhão.idTalhão, YEAR(dataTermino)");

                cmd.Parameters.Add(new MySqlParameter("@observação", observação));
                cmd.Parameters.Add(new MySqlParameter("@ano", ano.Year));
                cmd.Parameters.Add(new MySqlParameter("@nomeTalhão", talhão));
 
                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {


                throw new Exception(""  + ex);
            }

      
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      // Metodo responsavel com base na divisão especifica da propriedade retornar a quantidade ja faturada caso este ja tenha cadastrado algum rgistro sobre esta divisão especifica especificado 
        public DataTable Retornar_Quantidade_já_faturada(int idTalhão, string tipoCafe)
        {
            try
            {
                cmd = new MySqlCommand();
                mysql = new StringBuilder();
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("SELECT SUM(quantidadeFaturamento) as quantidadeTotalFaturado, YEAR(dataFAturamento) as ano ");
                mysql.Append(" FROM faturamento inner join talhão on idTalhão = Talhão_idTalhão");
                mysql.Append(" where Talhão_idTalhão = @idTalhão AND tipoDeCafé = @tipoCafe");
                mysql.Append(" GROUP BY faturamento.Talhão_idTalhão, YEAR(dataFAturamento) ");

                cmd.Parameters.Add(new MySqlParameter("@idTalhão", idTalhão));
                cmd.Parameters.Add(new MySqlParameter("@tipoCafe", tipoCafe));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception )
            {

                throw new Exception(" Retornar_Quantidade_já_faturada");
            }
        }

     }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

}
