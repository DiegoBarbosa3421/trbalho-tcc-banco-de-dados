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
     * Classe Execução serviços responsavel por ações salvar, excluir, alterar, Listar, pesquisar feitas na tabla de execução de srviços
     */
    public class ExecuçãoAtividades
    {
        MySqlCommand cmd = new MySqlCommand();
        DataTable listarTabela = new DataTable();
        StringBuilder mysql = new StringBuilder();

        MySqlConnection con ;

   //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void SalvarExServiços(string situação,  Date dataInicio, Date dataTermino,  int idServiço, int idColaborador, int idTalhão)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("INSERT INTO execuçãoatividades( situação ,dataInicio , dataTermino, CadastroServiço_idServiço, Colaborador_idColaborador, Talhão_idTalhão)");
                mysql.Append(" VALUES ( @situação, @dataInicio, @dataTermino, @CadastroServiço_idServiço, @Colaborador_idColaborador, @Talhão_idTalhão)");

                cmd.Parameters.Add(new MySqlParameter("@situação", situação)); 
                cmd.Parameters.Add(new MySqlParameter("@dataTermino", dataTermino));
                cmd.Parameters.Add(new MySqlParameter("@dataInicio", dataInicio));
                cmd.Parameters.Add(new MySqlParameter("@CadastroServiço_idServiço", idServiço));
                cmd.Parameters.Add(new MySqlParameter("@Colaborador_IdColaborador", idColaborador));
                cmd.Parameters.Add(new MySqlParameter("@Talhão_idTalhão", idTalhão));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception ex )
            {

                throw new Exception("Erro ao salvar registros na tabela Execução de serviço" + ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void DeletarExServiço(int idExecuçãoAtividades)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("DELETE FROM execuçãoatividades");
                mysql.Append(" WHERE (idExecuçãoAtividades = @idExecuçãoAtividades)");

                cmd.Parameters.Add(new MySqlParameter("@idExecuçãoAtividades", idExecuçãoAtividades));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch ( Exception ex )
            {

                throw new Exception("Erro ao excluir registro de Execuçõa de serviço" + ex);
            }

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void AlterarExServiço(int idExecuçãoAtividades, string situação, Date dataInicio, Date dataTermino, int idServiço, int idTalhão, int idColaborador)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("UPDATE ExecuçãoAtividades ");
                mysql.Append(" SET  situação = @situação, dataInicio = @dataInicio, dataTermino = @dataTermino, CadastroServiço_idServiço = @CadastroServiço_idServiço, ");
                mysql.Append(" Talhão_idTalhão = @Talhão_idTalhão, Colaborador_idColaborador = @Colaborador_idColaborador");
                mysql.Append(" WHERE (idExecuçãoAtividades = @idExecuçãoAtividades)");


                cmd.Parameters.Add(new MySqlParameter("@idExecuçãoAtividades", idExecuçãoAtividades));
                cmd.Parameters.Add(new MySqlParameter("@situação", situação));
                cmd.Parameters.Add(new MySqlParameter("@dataTermino", dataTermino));
                cmd.Parameters.Add(new MySqlParameter("@dataInicio", dataInicio));
                cmd.Parameters.Add(new MySqlParameter("@CadastroServiço_idServiço", idServiço));
                cmd.Parameters.Add(new MySqlParameter("@Colaborador_IdColaborador", idColaborador));
                cmd.Parameters.Add(new MySqlParameter("@Talhão_idTalhão", idTalhão));

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();


                con.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao executar alteração do registro de execução de serviços "+ ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable ListarEXServiços()
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("Select execuçãoAtividades.*, nomeColaborador as colaborador, nomeTalhão as talhão, tiposerviço as cadastroserviço ");
                mysql.Append(" from  ExecuçãoAtividades  JOIN colaborador  on idColaborador = Colaborador_idColaborador  join talhão  on idTalhão = Talhão_idTalhão ");
                mysql.Append(" join cadastroserviço  on CadastroServiço_idserviço = idserviço ");

                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;
                
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao listar atividades " + ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public DataTable PesquisarAtividades(int atividade)
        {
            try
            {
                con = new MySqlConnection(Conexão.stringConecçaõ());
                con.Open();

                mysql.Append("Select execuçãoAtividades.*, nomeColaborador as colaborador, nomeTalhão as talhão, tiposerviço as cadastroserviço ");
                mysql.Append(" from  ExecuçãoAtividades  JOIN colaborador  on idColaborador = Colaborador_idColaborador  join talhão  on idTalhão = Talhão_idTalhão ");
                mysql.Append(" join cadastroserviço  on CadastroServiço_idserviço = idserviço ");
                mysql.Append(" WHERE (CadastroServiço_idserviço =  @CadastroServiço_idserviço)");

                cmd.Parameters.Add(new MySqlParameter("@CadastroServiço_idserviço", atividade));



                cmd.CommandText = mysql.ToString();
                cmd.Connection = con;
                listarTabela.Load(cmd.ExecuteReader());

                con.Close();

                return listarTabela;

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao listar atividades " + ex);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
