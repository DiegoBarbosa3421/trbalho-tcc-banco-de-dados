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
   
    public class Produção
/*
 * Classe responsavel pelas ações salvar, excluir, alterar, lsitar, pesquisar da tabela produção no banco de dados.
 */
    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela;
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con;
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   //Metodo SalvarProdução salva no banco a produção com bsase nos campos quantidade que corresponde a quantidade prosuzida
   //, dataInicio que é a data em que se começou as atividades de produção, dataTermino que a data onde se finaliza a produção
   //, custoUnitario o que cada unidade produzida custou para produzir, observação o tipo de produção podendo ser "maduro , seco, despolpado, descascado"
   //, talhão_idTalhão a repartição da propriedade ao qual a produção esta vinculada, idColaborador o colaborador responsavel por aquela produção
        public void salvarProdução(double quantidade, Date  datainicio, Date dataTermino, double custoUnitario, string observação, int Talhão_idTalhão, int idColaborador )
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("INSERT INTO produção (quantidade, datainicio, dataTermino, custoUnitario, observação, Talhão_idTalhão, Colaborador_idColaborador )");
                mysql.Append("VALUES (@quantidade, @datainicio, @dataTermino, @custoUnitario, @observação, @Talhão_idTalhão, @Colaborador_idColaborador) ");

                cmd.Parameters.Add(new MySqlParameter("@quantidade", quantidade));
                cmd.Parameters.Add(new MySqlParameter("@datainicio", datainicio));
                cmd.Parameters.Add(new MySqlParameter("@dataTermino", dataTermino));
                cmd.Parameters.Add(new MySqlParameter("@custoUnitario", custoUnitario));
                cmd.Parameters.Add(new MySqlParameter("@observação", observação));
                cmd.Parameters.Add(new MySqlParameter("@Talhão_idTalhão", Talhão_idTalhão));
                cmd.Parameters.Add(new MySqlParameter("@Colaborador_idColaborador", idColaborador));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao tentar salvar registros databela Produção" + ex);
            }

            
        }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   //ExcluirProdução deleta do banco de dados a produção que coresponda ao id especificado pelo usuario
        public void EscluiProdução(int idProdução)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" DELETE FROM produção");
                mysql.Append(" WHERE (idprodução = @idprodução)");

                cmd.Parameters.Add(new MySqlParameter("@idprodução", idProdução));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception  ex )
            {

                throw new Exception ("erro ao tentar deletar o registro da tabela Produção" + ex);
            }
        }
 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 //AlteraProdução altera a produção onde o idProdução e igual ao id fornecido pelo usuario
        public void AlterarProdução(int idProdução,double quantidade, Date datainicio, Date datafim, double custoUnitario, string observação, int Talhão_idTalhão, int idColabordor)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" UPDATE Produção");
                mysql.Append(" SET  quantidade = @quantidade, datainicio = @datainicio, dataTermino = @dataTermino, custoUnitario = @custoUnitario,  Colaborador_idColaborador = @Colaborador_idColaborador");
                mysql.Append(" WHERE (idProdução = @idPodução) ");

                cmd.Parameters.Add(new MySqlParameter("@idPodução", idProdução));
                cmd.Parameters.Add(new MySqlParameter("@quantidade", quantidade));
                cmd.Parameters.Add(new MySqlParameter("@datainicio", datainicio));
                cmd.Parameters.Add(new MySqlParameter("@dataTermino", datafim));
                cmd.Parameters.Add(new MySqlParameter("@custoUnitario", custoUnitario));
                cmd.Parameters.Add(new MySqlParameter("@observação", observação));
                cmd.Parameters.Add(new MySqlParameter("@Talhão_idTalhão", Talhão_idTalhão));
                cmd.Parameters.Add(new MySqlParameter("@Colaborador_idColaborador", idColabordor));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao alterar registro" + ex);
            }
        }
 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Metodo retornar do banco de dados uma lista de informação de produção
        public DataTable ListarProdução()
        {
            try
            {
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("  SELECT produção. idprodução, quantidade, dataInicio, dataTermino, observação, custoUnitario, (quantidade* custoUnitario) as custoTotal , nomeTalhão as nomeTalhão, nomeColaborador as nomeColaborador");
                mysql.Append("  FROM  produção inner join talhão on idTalhão = Talhão_idTalhão inner join colaborador on idColaborador = Colaborador_idColaborador");


                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao retornar a Lista de produção" + ex );
            }
        }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // metodo para pesquisar informações no banco de dados na tabela de Produção.
        public DataTable PesquisarProdução(string tipodePesquisa, string PesquisaProdução, DateTime data1, DateTime data2)
        {
            try
            {
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("  SELECT produção. idprodução, quantidade, dataInicio, dataTermino, observação, custoUnitario, (quantidade* custoUnitario) as custoTotal , nomeTalhão as nomeTalhão, nomeColaborador as nomeColaborador");
                mysql.Append("  FROM  produção inner join talhão on idTalhão = Talhão_idTalhão inner join colaborador on idColaborador = Colaborador_idColaborador");

                switch (tipodePesquisa)
                {
                    // Isntruções para pesquisar caso o tipo de pesquisa selecionado pelo usuario sejá Talhão.
                    case "Talhão":
                        mysql.Append(" WHERE (nomeTalhão = @nomeTalhão)");
                        cmd.Parameters.Add(new MySqlParameter("@nomeTalhão", PesquisaProdução));

                        break;

                    // Istuções de pesquisa caso o tipo escolhido sejá pelo nome do colaborador envolvido
                    case "Colaborador":
                        mysql.Append(" WHERE (nomeColaborador = @nomeColaborador)");
                        cmd.Parameters.Add(new MySqlParameter("@nomeColaborador", PesquisaProdução));
                        break;

                   // Istruçoes de pesquisa caso o usuario selecione entervalos de data para pesquisar
                    case "Data":
                        mysql.Append(" WHERE produção.dataTermino between @data1 and @data2 ");
                        cmd.Parameters.Add(new MySqlParameter("@data1", data1));
                        cmd.Parameters.Add(new MySqlParameter("@data2", data2));
                        break;

                    default:
                        break;
                }

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao pesquisar a Lista de produção" + ex);
            }
        }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
    
}
