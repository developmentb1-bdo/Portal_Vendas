using SAPB1.DTO.Servico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ContratoDAL
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ContratoDTO> Listar()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM OTER");

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ContratoDTO> listContrato = new List<ContratoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ContratoDTO transacaoDTO = new ContratoDTO();
                        //territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                        //territorioDTO.Descript = rdr["descript"].ToString();

                        listContrato.Add(transacaoDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listContrato;
            }
            catch (SqlException er)
            {
                throw new Exception(er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }
    }
}
