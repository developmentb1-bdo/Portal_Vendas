using SAPB1.DTO.Servico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SAPB1.SqlServerDAL.Servico
{
    public class TransacaoDAL
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<TransacaoDTO> Listar(int codigo)
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

                IList<TransacaoDTO> listTransacao = new List<TransacaoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        TransacaoDTO transacaoDTO = new TransacaoDTO();
                        //territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                        //territorioDTO.Descript = rdr["descript"].ToString();

                        listTransacao.Add(transacaoDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listTransacao;
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
