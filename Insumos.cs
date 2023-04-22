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
    * Classe responsavel por ações de salvar, alterar, excluir, listar e pesquisar da tabela insumos no banco de dados
    */
    public class Insumos
    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con ;
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
       // Salva novos insumos com base nos dados recebidos de , nomeInsumos, valorUnitario referente ao valor de cadada unidade, quantidadeComprada refernte a quantidade adiquirida, observação que pode ser 
       // o tipo de compra por exemplos sacos 50 kg, litros etc, e a data da compra
        public void SalvarInsumos(string nomeInsumos, double valorUnitario, double valorTotal, double quantidadeComprada, string observação, Date data)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append(" INSERT INTO insumos(nomeInsumos,valorUnitario, valortotal, quantidadeComprada, observação, dataAquisiçao)");
                mysql.Append(" VALUES (@nomeInsumos, @valorUnitario, @valortotal, @quantidadeComprada, @observação ,@dataAquisição)");

                cmd.Parameters.Add(new MySqlParameter("@nomeInsumos", nomeInsumos));
                cmd.Parameters.Add(new MySqlParameter("@valorUnitario", valorUnitario));
                cmd.Parameters.Add(new MySqlParameter("@valortotal", valorTotal));
                cmd.Parameters.Add(new MySqlParameter("@quantidadeComprada", quantidadeComprada));
                cmd.Parameters.Add(new MySqlParameter("@observação",observação));
                cmd.Parameters.Add(new MySqlParameter("@dataAquisição", data));
                


                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao salvar dados nas tabela Insumos" + ex);
                
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Excluir exclui os insumos com base em seu id  
        public void ExcluirInsumos(int idInsumos)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append("DELETE FROM insumos ");
                mysql.Append(" WHERE (idInsumos = @idInsumos)");

                cmd.Parameters.Add(new MySqlParameter("@idInsumos", idInsumos));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex )
            {

                throw new Exception("Erro ao deletar registro da tabela Insumos", ex);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Alterar altera os parametros de um  insumos ja cadastrado onde o id forigual ao idInsumos forneceido

        public void AlterarInsumos(int idInsumos, string nomeInsumos, double valorUnitario, double valorTotal, double quantidadeComprada, string observação, Date data)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();
                mysql.Append(" UPDATE insumos ");
                mysql.Append(" SET  nomeInsumos = @nomeInsumos, valorUnitario =" +
                    " @valorUnitario, valortotal = @valorTotal, quantidadeComprada = @quantidadeComprada," +
                    " observação = @observação, dataAquisiçao = @dataAquisiçao");
                mysql.Append(" WHERE (idInsumos = @idInsumos)");

                cmd.Parameters.Add(new MySqlParameter("@idInsumos", idInsumos));
                cmd.Parameters.Add(new MySqlParameter("@nomeInsumos", nomeInsumos));
                cmd.Parameters.Add(new MySqlParameter("@valorUnitario", valorUnitario));
                cmd.Parameters.Add(new MySqlParameter("@valortotal", valorTotal));
                cmd.Parameters.Add(new MySqlParameter("@quantidadeComprada", quantidadeComprada));
                cmd.Parameters.Add(new MySqlParameter("@observação", observação));
                cmd.Parameters.Add(new MySqlParameter("@dataAquisiçao", data));
              

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao atualizar tabela Insumos", ex);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Retornar retorna uma lsita de insumos ja cadastrados
        public DataTable RetornarInsumos()
        {
           
                try
                {
                    con = new MySqlConnection(Conexão.stringConecçaõ());
                    con.Open();

                    mysql.Append("SELECT * FROM insumos");
                   
                    cmd.CommandText = mysql.ToString();
                    cmd.Connection = con;
                    listarTabela.Load(cmd.ExecuteReader());

                    con.Close();

                    return listarTabela;
                }
                catch (Exception)
                {

                    throw new Exception("Erro ao retornar lista de insumos");
                }
            
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Pesquisar retorna uma lista de insumos especificos com base no nomeInsumo digitado no campo pesquisar pelo usuario
        public DataTable PesquisarInsumos(string nomeInsumos)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("SELECT * FROM insumos");
                mysql.Append(" WHERE (nomeInsumos = @nomeInsumos )");

                cmd.Parameters.Add(new MySqlParameter("@nomeInsumos", nomeInsumos));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                return listarTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
