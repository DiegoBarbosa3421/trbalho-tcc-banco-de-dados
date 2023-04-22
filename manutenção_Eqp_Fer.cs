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
     * Classe responsavel pelos metos salvar, alterar, listar, pesquisar da tabela manutençoes do bnaco de dados
     */
    public class manutenção_Eqp_Fer
    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con;
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //SalvarManutenção salva a manutenção com base nos campos tipoManutenção, valorManutenção, dataManutenção, idFerramentas
        public void SalvarManutenção(string tipoManutenção, double valorManutenção, DateTime dataManutenção,int idFerramentas)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" INSERT INTO  manutenção_equipamentos (tipoManutenção, valorManutenção, dataManutenção, Ferramentas_e_Equipamentos_idFerramentas)");
                mysql.Append(" VALUES (@tipoManutenção, @valorManutenção, @dataManutenção, @Ferramentas_e_Equipamentos_idFerramentas ) ");

                cmd.Parameters.Add(new MySqlParameter("@tipoManutenção", tipoManutenção));
                cmd.Parameters.Add(new MySqlParameter("@valorManutenção", valorManutenção));
                cmd.Parameters.Add(new MySqlParameter("@dataManutenção", dataManutenção));
                cmd.Parameters.Add(new MySqlParameter("@Ferramentas_e_Equipamentos_idFerramentas", idFerramentas));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi Possivel efetuar o cadastro dos dados" + ex);
            }

        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //AlterarManutenção altera a manutenção onde o campo id for igual ao idManutenção 
        public void AlterarManutenção(string tipoManutenção, double valorManutenção, DateTime dataManutenção, int idFerramentas, int idManutenção)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" UPDATE  manutenção_equipamentos SET tipoManutenção = @tipoManutenção , valorManutenção = @valorManutenção,");
                mysql.Append(" dataManutenção = @dataManutenção , Ferramentas_e_Equipamentos_idFerramentas = @Ferramentas_e_Equipamentos_idFerramentas");
                mysql.Append(" WHERE (idManutenção =  @idManutenção)");

                cmd.Parameters.Add(new MySqlParameter("@idManutenção", idManutenção));
                cmd.Parameters.Add(new MySqlParameter("@tipoManutenção", tipoManutenção));
                cmd.Parameters.Add(new MySqlParameter("@valorManutenção", valorManutenção));
                cmd.Parameters.Add(new MySqlParameter("@dataManutenção", dataManutenção));
                cmd.Parameters.Add(new MySqlParameter("@Ferramentas_e_Equipamentos_idFerramentas", idFerramentas));


                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao atualizar registro de manutenção"+ ex);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ExcluirManutenção exclui a manutenção com base no id fornecido que corresponda a manutenção 
        public void ExcluirManutenção(int idManutenção)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("delete from manutenção_equipamentos");
                mysql.Append("  where idManutenção = @idManutenção");

                cmd.Parameters.Add(new MySqlParameter("@idManutenção", idManutenção));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao deletar registro de manutenção" + ex);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //ListarManutenção retorna uma lista de todas as manutençoões cadastradas pelo usuario
        public DataTable listarManutenção()
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT manutenção_equipamentos.* ,  nomeFerramenta as nomeFerramenta From manutenção_equipamentos ");
                mysql.Append(" INNER  JOIN ferramentas_e_equipamentos on idFerramentas = Ferramentas_e_Equipamentos_idFerramentas ");

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());
                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao retornar lista de Manutenções" + ex);
            }

        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // PesquisarManutenção retorna a manutenção com base no nome da ferramenta fornecida pelo usuario
        public DataTable PesquisarManutenção(string nomeFerramenta)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append(" SELECT manutenção_equipamentos.* ,  nomeFerramenta as nomeFerramenta From manutenção_equipamentos ");
                mysql.Append(" INNER  JOIN ferramentas_e_equipamentos on idFerramentas = Ferramentas_e_Equipamentos_idFerramentas ");
                mysql.Append(" WHERE nomeFerramenta = @nomeFerramenta ");

                cmd.Parameters.Add(new MySqlParameter("@nomeFerramenta", nomeFerramenta));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());
                con.Close();

                return listarTabela;
            }
            catch (Exception ex)
            {
                throw new Exception ("Erro ao retornar pesquisa de manutenções "+ ex);
            }
        }
    }
}
