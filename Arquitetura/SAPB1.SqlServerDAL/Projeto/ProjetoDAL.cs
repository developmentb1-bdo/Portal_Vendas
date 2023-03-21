using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Projeto;
using System.Data.SqlClient;
using SAPB1.IDAL.Projeto;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Projeto
{
    public class ProjetoDAL : IProjeto
    {

        public IList<ProjetoDTO> Listar(ProjetoDTO projetoDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT ""PrjCode"", ""PrjName"", ""Active"", ""ValidTo"" FROM OPRJ WHERE ""Active"" = '{projetoDTO.Active}'";
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();

                    return PopularDadosHana(query, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                string queryPadrao = "SELECT PrjCode, PrjName, Active, ValidTo FROM OPRJ ";
                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);
                stb.Append("WHERE Active = @Active");

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@Active", projetoDTO.Active);

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                    cmd.Dispose();
                }
            }

        }

        private IList<ProjetoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<ProjetoDTO> listProjetos = new List<ProjetoDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ProjetoDTO projetoDTO = new ProjetoDTO();
                    projetoDTO.PrjCode = rdr["PrjCode"].ToString();
                    projetoDTO.PrjName = rdr["PrjName"].ToString();

                    listProjetos.Add(projetoDTO);
                }
            }

            return listProjetos;
        }

        private IList<ProjetoDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<ProjetoDTO> listProjetos = new List<ProjetoDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjetoDTO projetoDTO = new ProjetoDTO();
                    projetoDTO.PrjCode = dr["PrjCode"].ToString();
                    projetoDTO.PrjName = dr["PrjName"].ToString();

                    listProjetos.Add(projetoDTO);
                }
            }

            return listProjetos;
        }
    }
}
