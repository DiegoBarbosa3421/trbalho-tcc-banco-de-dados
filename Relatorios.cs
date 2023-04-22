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
    public class Relatorios
    {

        
        /*
         * Classe realtorio e uma classe responsavel apenas por retornar os campos necessarios para elaboração dos relatorios do ssitema
         * 
         */
        DataTable listarTabela;
        MySqlCommand cmd = new MySqlCommand();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con;

     // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable Relatorios_de_Serviços(DateTime data1, DateTime data2)
        {
            try
            {
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT execuçãoatividades. dataInicio, dataTermino, tiposerviço as serviços, valor as valorServiços ,nomeColaborador as colaborador, nomeTalhão as talhão ");
                mysql.Append(" FROM execuçãoatividades inner join cadastroserviço on idserviço = CadastroServiço_idserviço  inner join colaborador on idColaborador = Colaborador_idColaborador ");
                mysql.Append(" inner join talhão on Talhão_idTalhão = idTalhão ");
                mysql.Append(" WHERE execuçãoatividades.dataTermino between @data1 and @data2 ");


                cmd.Parameters.Add(new MySqlParameter("@data1", data1));
                cmd.Parameters.Add(new MySqlParameter("@data2", data2));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
     // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable Relatorios_de_Produção(DateTime data1, DateTime data2, string talhão, string colaborador, string propriedade)
        {
            try
            {
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();



                mysql.Append(" SELECT produção.idprodução, quantidade, dataInicio, dataTermino, custoUnitario, observação  ,  nomeColaborador as colaborador, nomeTalhão as talhão ,    nomePropriedade as propriedade");
                mysql.Append(" FROM produção inner join colaborador on idColaborador = Colaborador_idColaborador inner join talhão on idTalhão = Talhão_idTalhão inner join propriedade on idPropriedade = Propriedade_idPropriedade");

               // atributo de pesquisa de relatorio de produção responsavel por retornas a produção por data 
                if (talhão.Trim().Length == 0 && colaborador.Trim().Length == 0 && propriedade.Trim().Length == 0)
                {
                    mysql.Append(" Where produção.dataTermino between @data1 and @data2");
                    mysql.Append(" order by produção.dataTermino desc");

                }
                /*
                 * estes metodos não foram implementados devidamente ainda
                // atributo de pesquisa de relatorio de produção responsavel por retornas a produção por colaborador
                else if (talhão.Trim().Length == 0 && colaborador.Trim().Length != 0 && propriedade.Trim().Length == 0)
                {

                    mysql.Append("  Where produção.dataTermino between @data1 and @data2 and nomeColaborador = @nomeColaborador");
                    mysql.Append("  order by produção.dataTermino desc");

                    cmd.Parameters.Add(new MySqlParameter("@nomeColaborador", colaborador));

                }
                // atributo de pesquisa de relatorio de produção responsavel por retornas a produção por talhão ou repartição da propriedade 
                else if (talhão.Trim().Length != 0 && colaborador.Trim().Length == 0 && propriedade.Trim().Length == 0)
                {
                    mysql.Append(" Where produção.dataTermino between @data1 and @data2 and talhão = @talhão");
                    mysql.Append(" order by produção.dataTermino desc");

                    cmd.Parameters.Add(new MySqlParameter("@talhão", talhão));

                }
                // atributo de pesquisa de relatorio de produção responsavel por retornas a produção por propriedade caso exista mais de uma cadastrasda 
                else if (talhão.Trim().Length == 0 && colaborador.Trim().Length == 0 && propriedade.Trim().Length != 0)
                {
                    mysql.Append(" Where produção.dataTermino between @data1 and @data2 and propriedade = @propriedade");
                    mysql.Append(" order by produção.dataTermino desc");

                    cmd.Parameters.Add(new MySqlParameter("@propriedade", propriedade));

                }*/

                cmd.Parameters.Add(new MySqlParameter("@data1", data1));
                cmd.Parameters.Add(new MySqlParameter("@data2", data2));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex);
            }
        }
     //  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
        public DataTable Relatorios_de_Insumos(DateTime data1, DateTime data2)
        {

            try
            {
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT * FROM insumos");
                mysql.Append(" WHERE insumos.dataAquisiçao between @data1 and @data2 ");
                mysql.Append(" order by insumos.dataAquisiçao desc ");

                cmd.Parameters.Add(new MySqlParameter("@data1", data1));
                cmd.Parameters.Add(new MySqlParameter("@data2", data2));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


        }
     //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable Relatoriosa__Manutenções(DateTime data1, DateTime data2)
        {
            try
            {
                cmd = new MySqlCommand();
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

        
                  mysql.Append(" SELECT manutenção_equipamentos.tipoManutenção,valorManutenção,dataManutenção, nomeFerramenta as nomeFerramenta ");
                  mysql.Append(" FROM manutenção_equipamentos inner join ferramentas_e_equipamentos on idFerramentas = Ferramentas_e_Equipamentos_idFerramentas ");
                  mysql.Append(" WHERE manutenção_equipamentos.dataManutenção between @data1 and @data2 ");

                 cmd.Parameters.Add(new MySqlParameter("@data1", data1));
                 cmd.Parameters.Add(new MySqlParameter("@data2", data2));
              
                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();
                return listarTabela;
            }
            catch (Exception ex)
            {

                throw new Exception("ERR0" +ex);
            }
        }
     //   ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable Relatoriosa_Ferramentas(DateTime data1, DateTime data2)
        {
            try
            {
                cmd = new MySqlCommand();
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                 mysql.Append(" SELECT ferramentas_e_equipamentos.nomeFerramenta, datadeAquisição, valorFerramenta FROM ferramentas_e_equipamentos ");
                 mysql.Append(" WHERE ferramentas_e_equipamentos.datadeAquisição between @data1 and @data2");
                 mysql.Append(" ORDER BY nomeFerramenta ASC ");

                 cmd.Parameters.Add(new MySqlParameter("@data1", data1));
                 cmd.Parameters.Add(new MySqlParameter("@data2", data2));
                   
                cmd.CommandText = mysql.ToString();
                 cmd.Connection = con;
                 listarTabela.Load(cmd.ExecuteReader());
             
                con.Close();
                return listarTabela;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
     //  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable Relatorio__Faturamento(DateTime dataConsultaInicio, DateTime dataConsultaFim)
        {
            try
            {
                listarTabela = new DataTable();
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT faturamento.*, nomeTalhão as nomeTalhão, nomePropriedade as nomePropriedade");
                mysql.Append(" FROM faturamento  inner join talhão on idTalhão = Talhão_idTalhão inner join propriedade on idPropriedade = Propriedade_idPropriedade");
                mysql.Append(" WHERE dataFAturamento between @data1 and @data2 ");
                mysql.Append(" order by Talhão_idTalhão desc");
 
                 cmd.Parameters.Add(new MySqlParameter("@data1", dataConsultaInicio));
                cmd.Parameters.Add(new MySqlParameter("@data2", dataConsultaFim));

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
    }
}
