using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SAPB1.DTO.CondicaoPagamento;
using SAPB1.IDAL.CondicaoPagamento;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.CondicaoPagamento
{
    public class CondicaoPagamentoDAL : ICondicaoPagamento
    {
        string queryPadrao = "SELECT cp.GroupNum, cp.PymntGroup, cp.UserSign FROM OCTG cp ";

        public IList<CondicaoPagamentoDTO> Listar(CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT cp.""GroupNum"", cp.""PymntGroup"", cp.""UserSign"" FROM OCTG AS cp ";

                if (condicaoPagamentoDTO.UserSign != 0 || condicaoPagamentoDTO.GroupNum != 0)
                    query += "WHERE ";

                if (condicaoPagamentoDTO.UserSign != 0)
                {
                    query += $@"cp.""UserSign"" = '{condicaoPagamentoDTO.UserSign}' ";
                }

                if (condicaoPagamentoDTO.GroupNum != 0 && condicaoPagamentoDTO.UserSign != 0)
                {
                    query += $@"AND cp.""GroupNum"" = '{condicaoPagamentoDTO.GroupNum}' ";
                }

                if (condicaoPagamentoDTO.GroupNum != 0 && condicaoPagamentoDTO.UserSign == 0)
                {
                    query += $@"cp.""GroupNum"" = '{condicaoPagamentoDTO.GroupNum}' ";
                }

                query += $@"ORDER BY cp.""PymntGroup""";

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query);

                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de dados: " + err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);

                SqlCommand cmd = new SqlCommand();

                //stb.Append("WHERE cp.DiscCode = 'Vendas' ");

                if (condicaoPagamentoDTO.UserSign != 0 || condicaoPagamentoDTO.GroupNum != 0)
                    stb.Append("WHERE ");

                if (condicaoPagamentoDTO.UserSign != 0)
                {
                    stb.Append("cp.UserSign = @UserSign ");

                    cmd.Parameters.AddWithValue("@UserSign", condicaoPagamentoDTO.UserSign);
                }

                if (condicaoPagamentoDTO.GroupNum != 0 && condicaoPagamentoDTO.UserSign != 0)
                {
                    stb.Append("AND cp.GroupNum = @GroupNum ");

                    cmd.Parameters.AddWithValue("@GroupNum", condicaoPagamentoDTO.GroupNum);
                }

                if (condicaoPagamentoDTO.GroupNum != 0 && condicaoPagamentoDTO.UserSign == 0)
                {
                    stb.Append("cp.GroupNum = @GroupNum ");

                    cmd.Parameters.AddWithValue("@GroupNum", condicaoPagamentoDTO.GroupNum);
                }

                stb.Append("ORDER BY cp.PymntGroup");

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no Banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                    cmd.Dispose();
                }
            }

        }

        private IList<CondicaoPagamentoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<CondicaoPagamentoDTO> listCondicaoPagamento = new List<CondicaoPagamentoDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    CondicaoPagamentoDTO condicaoPagamentoDTO = new CondicaoPagamentoDTO();
                    condicaoPagamentoDTO.GroupNum = Convert.ToInt32(rdr["GroupNum"].ToString());
                    condicaoPagamentoDTO.PymntGroup = rdr["PymntGroup"].ToString();
                    condicaoPagamentoDTO.UserSign = Convert.ToInt32(rdr["UserSign"].ToString());

                    listCondicaoPagamento.Add(condicaoPagamentoDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listCondicaoPagamento;
        }

        private IList<CondicaoPagamentoDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();

            IList<CondicaoPagamentoDTO> listCondicaoPagamento = new List<CondicaoPagamentoDTO>();

            DataTable dt = conexaoHana.ExecuteDataTable(query);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CondicaoPagamentoDTO condicaoPagamentoDTO = new CondicaoPagamentoDTO();
                    condicaoPagamentoDTO.GroupNum = Convert.ToInt32(dr["GroupNum"].ToString());
                    condicaoPagamentoDTO.PymntGroup = dr["PymntGroup"].ToString();
                    condicaoPagamentoDTO.UserSign = Convert.ToInt32(dr["UserSign"].ToString());

                    listCondicaoPagamento.Add(condicaoPagamentoDTO);
                }
            }

            return listCondicaoPagamento;
        }
    }
}
