using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.OrdemProducao;
using SAPB1.DTO.OrdemProducao;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.OrdemProducao
{
    public class OrdemProducaoDAL : IOrdemProducao
    {
        public IList<OrdemProducaoDTO> ObterOrdemProducaoAbertas()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT DocEntry, ItemCode, PlannedQty, DueDate FROM OWOR (NOLOCK) WHERE Status <> 'L'");

            SqlCommand cmd = new SqlCommand();

            SqlServerConexao conexao = new SqlServerConexao();

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                IList<OrdemProducaoDTO> listaOrdensProducao = new List<OrdemProducaoDTO>();

                SqlDataReader rdr = cmd.ExecuteReader();

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        OrdemProducaoDTO ordemProducao = new OrdemProducaoDTO();
                        ordemProducao.ItemCode = rdr["ItemCode"].ToString();
                        ordemProducao.PlannedQty = (rdr["PlannedQty"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["PlannedQty"]));
                        ordemProducao.DueDate = (rdr["DueDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["DueDate"]));

                        listaOrdensProducao.Add(ordemProducao);
                    }
                }

                rdr.Close();
                rdr.Dispose();
                cmd.Dispose();

                return listaOrdensProducao;
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
}
