using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.IDAL.Utilizacao.Cfop;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Utilizacao.Cfop
{
    public class CfopDAL:ICfop
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<CfopDTO> Listar(CfopDTO cfopDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT Code, CONCAT(Code, '-', Descrip) AS 'Descrip' FROM OCFP ");
            stb.Append("WHERE Locked = 'N' AND ");

            if(cfopDTO.TipoCfop == CfopType.Venda)
            {
                stb.Append("CAST(Code AS INT) >= 5000 ");
            }
            else
            {
                stb.Append("CAST(Code AS INT) < 5000 ");
            }

            stb.Append("ORDER BY Code");

            cmd.Connection = conexao.Conexao;
            cmd.CommandText = stb.ToString();

            try
            {
                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch(Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        private IList<CfopDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<CfopDTO> listCfop = new List<CfopDTO>();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    CfopDTO cfopDTO = new CfopDTO();
                    cfopDTO.Code = rdr["Code"].ToString();
                    cfopDTO.Descrip = rdr["Descrip"].ToString();

                    listCfop.Add(cfopDTO);
                }
            }

            rdr.Close();

            return listCfop;
        }
    }
}
