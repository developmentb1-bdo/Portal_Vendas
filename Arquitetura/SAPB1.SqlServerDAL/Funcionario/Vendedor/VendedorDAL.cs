using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using SAPB1.IDAL.Funcionario.Vendedor;
using SAPB1.DTO.Funcionario.Vendedor;
using SAPB1.DTO.Funcionario.Vendedor.Comissao;
using System.Data;

namespace SAPB1.SqlServerDAL.Funcionario.Vendedor
{
    public class VendedorDAL : IVendedor
    {
        public IList<VendedorDTO> Listar(VendedorDTO vendedorDTO)
        {

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT ""SlpCode"", ""SlpName"", ""Locked"", ""Active"", ""Memo"", ""GroupCode"", ""Commission"" FROM OSLP ";

                if (vendedorDTO != null)
                {
                    if (!string.IsNullOrEmpty(vendedorDTO.Active) || !string.IsNullOrEmpty(vendedorDTO.Locked))
                    {
                        query += "WHERE ";

                        if (!string.IsNullOrEmpty(vendedorDTO.Active))
                        {
                            query += $@"""Active"" = '{vendedorDTO.Active}' ";

                            if (!string.IsNullOrEmpty(vendedorDTO.Locked))
                            {
                                query += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(vendedorDTO.Locked))
                        {
                            query += $@"""Locked"" = '{vendedorDTO.Locked}' ";
                        }
                    }
                    else
                    {
                        if (vendedorDTO.SlpCode > 0)
                        {
                            query += $@"WHERE ""SlpCode"" = '{vendedorDTO.SlpCode}' ";
                        }
                    }
                }

                query += $@"ORDER BY ""SlpName""";
                HanaConexao conexaoHana = new HanaConexao();

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
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT ");
                stb.Append("SlpCode, ");
                stb.Append("SlpName, ");
                stb.Append("Locked, ");
                stb.Append("Active, ");
                stb.Append("Memo, ");
                stb.Append("GroupCode, ");
                stb.Append("Commission ");
                stb.Append("FROM OSLP ");

                if (vendedorDTO != null)
                {
                    if (!string.IsNullOrEmpty(vendedorDTO.Active) || !string.IsNullOrEmpty(vendedorDTO.Locked))
                    {
                        stb.Append("WHERE ");

                        if (!string.IsNullOrEmpty(vendedorDTO.Active))
                        {
                            stb.Append("Active = @Active ");

                            cmd.Parameters.AddWithValue("@Active", vendedorDTO.Active);

                            if (!string.IsNullOrEmpty(vendedorDTO.Locked))
                            {
                                stb.Append("AND ");
                            }
                        }

                        if (!string.IsNullOrEmpty(vendedorDTO.Locked))
                        {
                            stb.Append("Locked = @Locked ");

                            cmd.Parameters.AddWithValue("@Locked", vendedorDTO.Locked);
                        }
                    }
                    else
                    {
                        if (vendedorDTO.SlpCode > 0)
                        {
                            stb.Append("WHERE SlpCode = @SlpCode ");
                            cmd.Parameters.AddWithValue("@SlpCode", vendedorDTO.SlpCode);
                        }
                    }
                }

                stb.Append("ORDER BY SlpName");

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

        private IList<VendedorDTO> PopularDadosHana(string query)
        {
            IList<VendedorDTO> listVendedores = new List<VendedorDTO>();
            HanaConexao conexaoHana = new HanaConexao();

            try
            {
                DataTable rdr = conexaoHana.ExecuteDataTable(query);

                if (rdr.Rows.Count > 0)
                {
                    foreach (DataRow dt in rdr.Rows)
                    {
                        VendedorDTO vendedorDTO = new VendedorDTO();
                        vendedorDTO.SlpCode = Convert.ToInt32(dt["SlpCode"].ToString());
                        vendedorDTO.SlpName = dt["SlpName"].ToString();
                        vendedorDTO.Locked = dt["Locked"].ToString();
                        vendedorDTO.Active = dt["Active"].ToString();

                        GrupoComissaoDTO grupoComissaoDTO = new GrupoComissaoDTO();
                        grupoComissaoDTO.GroupCode = Convert.ToInt32(dt["GroupCode"].ToString());
                        grupoComissaoDTO.Comission = Convert.ToDouble(dt["Commission"].ToString().Equals("") ? "0" : dt["Commission"].ToString());

                        vendedorDTO.GrupoComissao = grupoComissaoDTO;

                        listVendedores.Add(vendedorDTO);
                    }
                }
                return listVendedores;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            finally
            {
                conexaoHana.Dispose();
            }

        }

        private IList<VendedorDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<VendedorDTO> listVendedores = new List<VendedorDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    VendedorDTO vendedorDTO = new VendedorDTO();
                    vendedorDTO.SlpCode = Convert.ToInt32(rdr["SlpCode"].ToString());
                    vendedorDTO.SlpName = rdr["SlpName"].ToString();
                    vendedorDTO.Locked = rdr["Locked"].ToString();
                    vendedorDTO.Active = rdr["Active"].ToString();

                    GrupoComissaoDTO grupoComissaoDTO = new GrupoComissaoDTO();
                    grupoComissaoDTO.GroupCode = Convert.ToInt32(rdr["GroupCode"].ToString());
                    grupoComissaoDTO.Comission = Convert.ToDouble(rdr["Commission"].ToString().Equals("") ? "0" : rdr["Commission"].ToString());

                    vendedorDTO.GrupoComissao = grupoComissaoDTO;

                    listVendedores.Add(vendedorDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listVendedores;
        }
    }
}
