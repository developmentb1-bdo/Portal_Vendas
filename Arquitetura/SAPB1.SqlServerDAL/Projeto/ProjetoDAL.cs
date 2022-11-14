using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Projeto;
using System.Data.SqlClient;
using SAPB1.IDAL.Projeto;

namespace SAPB1.SqlServerDAL.Projeto
{
    public class ProjetoDAL:IProjeto
    {
        string queryPadrao = "SELECT PrjCode, PrjName, Active, ValidTo FROM OPRJ ";

        public IList<ProjetoDTO> Listar(ProjetoDTO projetoDTO)
        {
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
    }
}
