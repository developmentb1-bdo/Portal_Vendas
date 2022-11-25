using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.FormasPagamento;
using SAPB1.IDAL.FormasPagamento;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.FormasPagamento
{
    public class FormaPagamentoDAL : IFormaPagamento
    {
        SqlServerConexao conexao = new SqlServerConexao();

        string queryPadrao = "SELECT PayMethCod, Descript, Active, Type FROM OPYM ";

        public IList<FormaPagamentoDTO> Listar(FormaPagamentoDTO formaPagamentoDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                if (formaPagamentoDTO != null)
                {
                    if (!string.IsNullOrEmpty(formaPagamentoDTO.Active) || !string.IsNullOrEmpty(formaPagamentoDTO.Type))
                    {
                        queryPadrao += "WHERE ";

                        if (!string.IsNullOrEmpty(formaPagamentoDTO.Active))
                        {
                            queryPadrao += $@"""Active"" = '{formaPagamentoDTO.Active}' ";

                            if (!string.IsNullOrEmpty(formaPagamentoDTO.Type))
                            {
                                queryPadrao += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(formaPagamentoDTO.Type))
                        {
                            queryPadrao += $@"""Type"" = '{formaPagamentoDTO.Type}' ";
                        }
                    }
                }
                queryPadrao += $@"ORDER BY ""PayMethCod""";

                try
                {
                    conexaoHana.Connection();

                    return PopularDadosHana(queryPadrao);
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
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);

                if (formaPagamentoDTO != null)
                {
                    if (!string.IsNullOrEmpty(formaPagamentoDTO.Active) || !string.IsNullOrEmpty(formaPagamentoDTO.Type))
                    {
                        stb.Append("WHERE ");

                        if (!string.IsNullOrEmpty(formaPagamentoDTO.Active))
                        {
                            stb.Append("Active = @Active ");

                            cmd.Parameters.AddWithValue("@Active", formaPagamentoDTO.Active);

                            if (!string.IsNullOrEmpty(formaPagamentoDTO.Type))
                            {
                                stb.Append("AND ");
                            }
                        }

                        if (!string.IsNullOrEmpty(formaPagamentoDTO.Type))
                        {
                            stb.Append("Type = @Type ");

                            cmd.Parameters.AddWithValue("@Type", formaPagamentoDTO.Type);
                        }
                    }
                }

                stb.Append("ORDER BY PayMethCod");

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

        private IList<FormaPagamentoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<FormaPagamentoDTO> listFormasPagamento = new List<FormaPagamentoDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    FormaPagamentoDTO formaPagamentoDTO = new FormaPagamentoDTO();
                    formaPagamentoDTO.PayMethCod = rdr["PayMethCod"].ToString();
                    formaPagamentoDTO.Descript = rdr["Descript"].ToString();
                    formaPagamentoDTO.Active = rdr["Active"].ToString();
                    formaPagamentoDTO.Type = rdr["Type"].ToString();

                    listFormasPagamento.Add(formaPagamentoDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listFormasPagamento;
        }

        private IList<FormaPagamentoDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();

            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<FormaPagamentoDTO> listFormasPagamento = new List<FormaPagamentoDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    FormaPagamentoDTO formaPagamentoDTO = new FormaPagamentoDTO();
                    formaPagamentoDTO.PayMethCod = dr["PayMethCod"].ToString();
                    formaPagamentoDTO.Descript = dr["Descript"].ToString();
                    formaPagamentoDTO.Active = dr["Active"].ToString();
                    formaPagamentoDTO.Type = dr["Type"].ToString();

                    listFormasPagamento.Add(formaPagamentoDTO);
                }
            }

            return listFormasPagamento;
        }
    }
}
