using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Representante;
using SAPB1.DTO.Representante;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Representante
{
    public class RepresentanteDAL:IRepresentante
    {
        public IList<RepresentanteDTO> Listar()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM OAGP WHERE Locked IS NULL ");
            stb.Append("ORDER BY AgentName");

            SqlServerConexao conexao = new SqlServerConexao();

            SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);

            try
            {
                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch(SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        private IList<RepresentanteDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<RepresentanteDTO> listRepresentante = new List<RepresentanteDTO>();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    RepresentanteDTO representanteDTO = new RepresentanteDTO();
                    representanteDTO.AgentCode = Convert.ToInt32(rdr["AgentCode"]);
                    representanteDTO.AgentName = rdr["AgentName"].ToString();

                    listRepresentante.Add(representanteDTO);
                }
            }

            rdr.Close();

            return listRepresentante;
        }
    }
}
