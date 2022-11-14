using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Estoque;
using SAPB1.DTO.Estoque;
using SAPB1.DTO.Item;
using SAPB1.DTO.Deposito;
using System.Data;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Estoque
{
    public class EstoqueDAL2: IEstoqueConsulta
    {
        SqlServerConexao conexao = new SqlServerConexao();

        /*public IList<EstoqueDTO> Listar_old(EstoqueDTO estoqueDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT TOP 500 ");
            stb.Append("e.OnOrder, ");
            stb.Append("e.OnHand, ");
            stb.Append("e.IsCommited, ");
            stb.Append("i.ItemName, ");
            stb.Append("e.ItemCode, ");
            stb.Append("e.WhsCode, ");
            stb.Append("d.WhsName ");
            stb.Append("FROM OITW e ");
            stb.Append("INNER JOIN OITM i ON i.ItemCode = e.ItemCode ");
            stb.Append("INNER JOIN OWHS d ON d.WhsCode = e.WhsCode ");
            stb.Append("WHERE 1 = 1 ");

            if (estoqueDTO != null)
            {
                if (estoqueDTO.Deposito != null)
                {
                    //stb.Append("AND d.WhsName LIKE @WhsName ");
                    //cmd.Parameters.AddWithValue("@WhsName", ("%" + estoqueDTO.Deposito.WhsName + "%"));
                    if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsCode))
                    {
                        stb.Append("AND e.WhsCode = @WhsCode ");
                        cmd.Parameters.AddWithValue("@WhsCode", estoqueDTO.Deposito.WhsCode);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsName))
                        {
                            stb.Append("AND d.WhsName LIKE @WhsName ");
                            cmd.Parameters.AddWithValue("@WhsName", ("%" + estoqueDTO.Deposito.WhsName + "%"));
                        }

                    }
                    
                }

                if (estoqueDTO.Item != null)
                {
                    stb.Append("AND ");

                    if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemCode))
                    {
                        estoqueDTO.Item.ItemCode = "%" + estoqueDTO.Item.ItemCode + "%";

                        stb.Append("i.ItemCode LIKE @ItemCode ");
                        cmd.Parameters.AddWithValue("@ItemCode", estoqueDTO.Item.ItemCode);

                        if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
                        {
                            stb.Append("AND ");
                        }
                    }

                    if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
                    {
                        estoqueDTO.Item.ItemName = "%" + estoqueDTO.Item.ItemName + "%";

                        stb.Append("i.ItemName LIKE @ItemName ");
                        cmd.Parameters.AddWithValue("@ItemName", "%" + estoqueDTO.Item.ItemName + "%");
                    }
                }
            }
            
            stb.Append("ORDER BY i.ItemCode DESC");

            cmd.CommandText = stb.ToString();
            cmd.Connection = conexao.Conexao;
           
            try
            {
                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }*/

        public IList<EstoqueConsulta> Listar(EstoqueDTO estoqueDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("EXEC [ConsultaPortal] @ItemCode,@WhsCode,@ItemName ");

            if (estoqueDTO != null)
            {
                if (estoqueDTO.Deposito != null)
                {
                    if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsCode))
                    {
                        //stb.Append("AND e.WhsCode = @WhsCode ");
                        cmd.Parameters.AddWithValue("@WhsCode", estoqueDTO.Deposito.WhsCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WhsCode", DBNull.Value);
                    }
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsName))
                    //    {
                    //        //stb.Append("AND d.WhsName LIKE @WhsName ");
                    //        cmd.Parameters.AddWithValue("@WhsName", ("%" + estoqueDTO.Deposito.WhsName + "%"));
                    //    }

                    //}

                }
                else
                {
                    cmd.Parameters.AddWithValue("@WhsCode", DBNull.Value);
                }

                if (estoqueDTO.Item != null)
                {
                    //stb.Append("AND ");

                    if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemCode))
                    {
                        estoqueDTO.Item.ItemCode = "%" + estoqueDTO.Item.ItemCode + "%";

                        //stb.Append("i.ItemCode LIKE @ItemCode ");
                        cmd.Parameters.AddWithValue("@ItemCode", estoqueDTO.Item.ItemCode);

                        //if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
                        //{
                        //    stb.Append("AND ");
                        //}
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ItemCode", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
                    {
                        estoqueDTO.Item.ItemName = "%" + estoqueDTO.Item.ItemName + "%";

                        //stb.Append("i.ItemName LIKE @ItemName ");
                        cmd.Parameters.AddWithValue("@ItemName", "%" + estoqueDTO.Item.ItemName + "%");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ItemName", DBNull.Value);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ItemCode", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ItemName", DBNull.Value);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@ItemCode", DBNull.Value);
                cmd.Parameters.AddWithValue("@ItemName", DBNull.Value);
                cmd.Parameters.AddWithValue("@WhsCode", DBNull.Value);
            }

            //stb.Append("ORDER BY i.ItemCode DESC");

            cmd.CommandText = stb.ToString();
            cmd.Connection = conexao.Conexao;

            try
            {
                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }

        private IList<EstoqueDTO> PopularDados_old(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<EstoqueDTO> listEstoque = new List<EstoqueDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    DepositoDTO depositoDTO = new DepositoDTO();
                    depositoDTO.WhsCode = rdr["WhsCode"].ToString();
                    depositoDTO.WhsName = rdr["WhsName"].ToString();

                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.ItemName = rdr["ItemName"].ToString();

                    EstoqueDTO estoqueDTO = new EstoqueDTO();
                    estoqueDTO.Deposito = depositoDTO;
                    estoqueDTO.Item = itemDTO;
                    estoqueDTO.OnHand = Convert.ToDouble(rdr["OnHand"].ToString());
                    estoqueDTO.OnOrder = Convert.ToDouble(rdr["OnOrder"].ToString());
                    estoqueDTO.IsCommited = Convert.ToDouble(rdr["IsCommited"].ToString());

                    listEstoque.Add(estoqueDTO);
                }
            }

            rdr.Close();
            rdr.Dispose();
            cmd.Dispose();

            return listEstoque;
        }

        private IList<EstoqueConsulta> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<EstoqueConsulta> listEstoque = new List<EstoqueConsulta>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {

                    EstoqueConsulta estoqueDTO = new EstoqueConsulta();

                    estoqueDTO.ItemCode = rdr["Cód. Item"].ToString();
                    estoqueDTO.WhsCode = rdr["Depósito"].ToString();
                    estoqueDTO.ItemName = rdr["Nome do Item"].ToString();
                    estoqueDTO.Comprimento = Convert.ToInt32(rdr["Comprimento(mm)"].ToString());
                    estoqueDTO.TotalPecas = Convert.ToInt32(rdr["Total Peças"].ToString());
                    estoqueDTO.EstoqueDisponivel = Convert.ToDouble(rdr["Estoque Disponivel"].ToString());
                    estoqueDTO.EstoqueReservado = Convert.ToDouble(rdr["Estoque Reservado"].ToString());
                    estoqueDTO.PesoUnitario = Convert.ToDouble(rdr["Peso Unitário"].ToString());
                    estoqueDTO.PrecoMinimo = rdr["Preço Mínimo"].ToString();
                    estoqueDTO.PrecoMaximo = rdr["Preço Máximo"].ToString();
                    estoqueDTO.Lote = rdr["Lote"].ToString();
                    estoqueDTO.GrupoItem = rdr["Grupo Item"].ToString();
                    estoqueDTO.EntradaPrevista = Convert.ToDouble(rdr["Entrada Prevista(kg)"].ToString());

                    listEstoque.Add(estoqueDTO);
                }
            }

            rdr.Close();
            rdr.Dispose();
            cmd.Dispose();

            return listEstoque;
        }

        public double RetornarTotalValorEstoque()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(((OnHand - IsCommited) + OnOrder) * AvgPrice) AS 'TOTAL' FROM OITW");

            try
            {
                conexao.Conectar();

                SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);

                return Convert.ToDouble(comando.ExecuteScalar());
            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public IList<EstoqueDTO> ListarEstoquePorProduto(string itemCode)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("e.OnOrder, ");
            stb.Append("e.OnHand, ");
            stb.Append("e.IsCommited, ");
            stb.Append("i.ItemName, ");
            stb.Append("e.ItemCode, ");
            stb.Append("e.WhsCode, ");
            stb.Append("d.WhsName, ");
            stb.Append("d.BPLid ");
            stb.Append("FROM OITW e ");
            stb.Append("INNER JOIN OITM i ON i.ItemCode = e.ItemCode AND e.ItemCode = @ItemCode ");
            stb.Append("INNER JOIN OWHS d ON d.WhsCode = e.WhsCode ");

            SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);
            comando.Parameters.AddWithValue("@ItemCode", itemCode);

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = comando.ExecuteReader();

                IList<EstoqueDTO> listEstoque = new List<EstoqueDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        DepositoDTO depositoDTO = new DepositoDTO();
                        depositoDTO.WhsCode = rdr["WhsCode"].ToString();
                        depositoDTO.WhsName = rdr["WhsName"].ToString();

                        ItemDTO itemDTO = new ItemDTO();
                        itemDTO.ItemCode = rdr["ItemCode"].ToString();
                        itemDTO.ItemName = rdr["ItemName"].ToString();

                        EstoqueDTO estoqueDTO = new EstoqueDTO();
                        estoqueDTO.Deposito = depositoDTO;
                        estoqueDTO.Item = itemDTO;
                        estoqueDTO.OnHand = Convert.ToDouble(rdr["OnHand"].ToString());
                        estoqueDTO.OnOrder = Convert.ToDouble(rdr["OnOrder"].ToString());
                        estoqueDTO.IsCommited = Convert.ToDouble(rdr["IsCommited"].ToString());
                        estoqueDTO.BPLid = rdr["BPLid"].ToString();

                        listEstoque.Add(estoqueDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listEstoque;
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                comando.Dispose();
                conexao.Desconectar();
            }
        }
    }

    public class EstoqueDAL:IEstoque
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<EstoqueDTO> Listar(EstoqueDTO estoqueDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT TOP 500 ");
            stb.Append("e.OnOrder, ");
            stb.Append("e.OnHand, ");
            stb.Append("e.IsCommited, ");
            stb.Append("i.ItemName, ");
            stb.Append("e.ItemCode, ");
            stb.Append("e.WhsCode, ");
            stb.Append("d.WhsName ");
            stb.Append("FROM OITW e ");
            stb.Append("INNER JOIN OITM i ON i.ItemCode = e.ItemCode ");
            stb.Append("INNER JOIN OWHS d ON d.WhsCode = e.WhsCode ");
            stb.Append("WHERE 1 = 1 ");

            if (estoqueDTO != null)
            {
                if (estoqueDTO.Deposito != null)
                {
                    //stb.Append("AND d.WhsName LIKE @WhsName ");
                    //cmd.Parameters.AddWithValue("@WhsName", ("%" + estoqueDTO.Deposito.WhsName + "%"));
                    if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsCode))
                    {
                        stb.Append("AND e.WhsCode = @WhsCode ");
                        cmd.Parameters.AddWithValue("@WhsCode", estoqueDTO.Deposito.WhsCode);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsName))
                        {
                            stb.Append("AND d.WhsName LIKE @WhsName ");
                            cmd.Parameters.AddWithValue("@WhsName", ("%" + estoqueDTO.Deposito.WhsName + "%"));
                        }

                    }
                    
                }

                if (estoqueDTO.Item != null)
                {
                    stb.Append("AND ");

                    if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemCode))
                    {
                        estoqueDTO.Item.ItemCode = "%" + estoqueDTO.Item.ItemCode + "%";

                        stb.Append("i.ItemCode LIKE @ItemCode ");
                        cmd.Parameters.AddWithValue("@ItemCode", estoqueDTO.Item.ItemCode);

                        if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
                        {
                            stb.Append("AND ");
                        }
                    }

                    if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
                    {
                        estoqueDTO.Item.ItemName = "%" + estoqueDTO.Item.ItemName + "%";

                        stb.Append("i.ItemName LIKE @ItemName ");
                        cmd.Parameters.AddWithValue("@ItemName", "%" + estoqueDTO.Item.ItemName + "%");
                    }
                }
            }
            
            stb.Append("ORDER BY i.ItemCode DESC");

            cmd.CommandText = stb.ToString();
            cmd.Connection = conexao.Conexao;
           
            try
            {
                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }

        //public IList<EstoqueConsulta> Listar(EstoqueDTO estoqueDTO)
        //{
        //    SqlCommand cmd = new SqlCommand();

        //    StringBuilder stb = new StringBuilder();
        //    stb.Append("EXEC [ConsultaPortal] @ItemCode,@WhsCode,@ItemName ");

        //    if (estoqueDTO != null)
        //    {
        //        if (estoqueDTO.Deposito != null)
        //        {
        //            if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsCode))
        //            {
        //                //stb.Append("AND e.WhsCode = @WhsCode ");
        //                cmd.Parameters.AddWithValue("@WhsCode", estoqueDTO.Deposito.WhsCode);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@WhsCode", DBNull.Value);
        //            }
        //            //else
        //            //{
        //            //    if (!string.IsNullOrEmpty(estoqueDTO.Deposito.WhsName))
        //            //    {
        //            //        //stb.Append("AND d.WhsName LIKE @WhsName ");
        //            //        cmd.Parameters.AddWithValue("@WhsName", ("%" + estoqueDTO.Deposito.WhsName + "%"));
        //            //    }

        //            //}

        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@WhsCode", DBNull.Value);
        //        }

        //        if (estoqueDTO.Item != null)
        //        {
        //            //stb.Append("AND ");

        //            if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemCode))
        //            {
        //                estoqueDTO.Item.ItemCode = "%" + estoqueDTO.Item.ItemCode + "%";

        //                //stb.Append("i.ItemCode LIKE @ItemCode ");
        //                cmd.Parameters.AddWithValue("@ItemCode", estoqueDTO.Item.ItemCode);

        //                //if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
        //                //{
        //                //    stb.Append("AND ");
        //                //}
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@ItemCode", DBNull.Value);
        //            }

        //            if (!string.IsNullOrEmpty(estoqueDTO.Item.ItemName))
        //            {
        //                estoqueDTO.Item.ItemName = "%" + estoqueDTO.Item.ItemName + "%";

        //                //stb.Append("i.ItemName LIKE @ItemName ");
        //                cmd.Parameters.AddWithValue("@ItemName", "%" + estoqueDTO.Item.ItemName + "%");
        //            }
        //            {
        //                cmd.Parameters.AddWithValue("@ItemName", DBNull.Value);
        //            }
        //        }
        //        else
        //        {
        //            cmd.Parameters.AddWithValue("@ItemCode", DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ItemName", DBNull.Value);
        //        }
        //    }
        //    else
        //    {
        //        cmd.Parameters.AddWithValue("@ItemCode", DBNull.Value);
        //        cmd.Parameters.AddWithValue("@ItemName", DBNull.Value);
        //        cmd.Parameters.AddWithValue("@WhsCode", DBNull.Value);
        //    }

        //    //stb.Append("ORDER BY i.ItemCode DESC");

        //    cmd.CommandText = stb.ToString();
        //    cmd.Connection = conexao.Conexao;

        //    try
        //    {
        //        conexao.Conectar();

        //        return PopularDados(ref cmd);
        //    }
        //    catch (SqlException er)
        //    {
        //        throw new Exception("Erro no banco de dados: " + er.Message);
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        conexao.Desconectar();
        //    }
        //}

        private IList<EstoqueDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<EstoqueDTO> listEstoque = new List<EstoqueDTO>();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    DepositoDTO depositoDTO = new DepositoDTO();
                    depositoDTO.WhsCode = rdr["WhsCode"].ToString();
                    depositoDTO.WhsName = rdr["WhsName"].ToString();

                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.ItemName = rdr["ItemName"].ToString();

                    EstoqueDTO estoqueDTO = new EstoqueDTO();
                    estoqueDTO.Deposito = depositoDTO;
                    estoqueDTO.Item = itemDTO;
                    estoqueDTO.OnHand = Convert.ToDouble(rdr["OnHand"].ToString());
                    estoqueDTO.OnOrder = Convert.ToDouble(rdr["OnOrder"].ToString());
                    estoqueDTO.IsCommited = Convert.ToDouble(rdr["IsCommited"].ToString());

                    listEstoque.Add(estoqueDTO);
                }
            }

            rdr.Close();
            rdr.Dispose();
            cmd.Dispose();

            return listEstoque;
        }

        //private IList<EstoqueConsulta> PopularDados(ref SqlCommand cmd)
        //{
        //    SqlDataReader rdr = cmd.ExecuteReader();

        //    IList<EstoqueConsulta> listEstoque = new List<EstoqueConsulta>();

        //    if (rdr.HasRows)
        //    {
        //        while (rdr.Read())
        //        {

        //            EstoqueConsulta estoqueDTO = new EstoqueConsulta();

        //            estoqueDTO.ItemCode = rdr["Cód. Item"].ToString();
        //            estoqueDTO.WhsCode = rdr["Depósito"].ToString();
        //            estoqueDTO.ItemName = rdr["Nome do Item"].ToString();
        //            estoqueDTO.Comprimento =  Convert.ToInt32(rdr["Comprimento(mm)"].ToString());
        //            estoqueDTO.TotalPecas = Convert.ToInt32(rdr["Total Peças"].ToString());
        //            estoqueDTO.EstoqueDisponivel = Convert.ToDouble(rdr["Estoque Disponivel"].ToString());
        //            estoqueDTO.EstoqueReservado = Convert.ToDouble(rdr["Estoque Reservado"].ToString());
        //            estoqueDTO.PesoUnitario = Convert.ToDouble(rdr["Peso Unitário"].ToString());
        //            estoqueDTO.PrecoMinimo = Convert.ToDouble(rdr["Preço Mínimo"].ToString());
        //            estoqueDTO.PrecoMaximo = Convert.ToDouble(rdr["Preço Máximo"].ToString());
        //            estoqueDTO.Lote = rdr["Lote"].ToString();
        //            estoqueDTO.GrupoItem = rdr["Grupo Item"].ToString();
        //            estoqueDTO.EntradaPrevista = Convert.ToDouble(rdr["Entrada Prevista(kg)"].ToString());

        //            listEstoque.Add(estoqueDTO);
        //        }
        //    }

        //    rdr.Close();
        //    rdr.Dispose();
        //    cmd.Dispose();

        //    return listEstoque;
        //}

        public double RetornarTotalValorEstoque()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(((OnHand - IsCommited) + OnOrder) * AvgPrice) AS 'TOTAL' FROM OITW");

            try
            {
                conexao.Conectar();

                SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);
                
                return Convert.ToDouble(comando.ExecuteScalar());
            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public IList<EstoqueDTO> ListarEstoquePorProduto(string itemCode)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("e.OnOrder, ");
            stb.Append("e.OnHand, ");
            stb.Append("e.IsCommited, ");
            stb.Append("i.ItemName, ");
            stb.Append("e.ItemCode, ");
            stb.Append("e.WhsCode, ");
            stb.Append("d.WhsName, ");
            stb.Append("d.BPLid ");
            stb.Append("FROM OITW e ");
            stb.Append("INNER JOIN OITM i ON i.ItemCode = e.ItemCode AND e.ItemCode = @ItemCode ");
            stb.Append("INNER JOIN OWHS d ON d.WhsCode = e.WhsCode ");

            SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);
            comando.Parameters.AddWithValue("@ItemCode", itemCode);

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = comando.ExecuteReader();

                IList<EstoqueDTO> listEstoque = new List<EstoqueDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        DepositoDTO depositoDTO = new DepositoDTO();
                        depositoDTO.WhsCode = rdr["WhsCode"].ToString();
                        depositoDTO.WhsName = rdr["WhsName"].ToString();

                        ItemDTO itemDTO = new ItemDTO();
                        itemDTO.ItemCode = rdr["ItemCode"].ToString();
                        itemDTO.ItemName = rdr["ItemName"].ToString();

                        EstoqueDTO estoqueDTO = new EstoqueDTO();
                        estoqueDTO.Deposito = depositoDTO;
                        estoqueDTO.Item = itemDTO;
                        estoqueDTO.OnHand = Convert.ToDouble(rdr["OnHand"].ToString());
                        estoqueDTO.OnOrder = Convert.ToDouble(rdr["OnOrder"].ToString());
                        estoqueDTO.IsCommited = Convert.ToDouble(rdr["IsCommited"].ToString());
                        estoqueDTO.BPLid = rdr["BPLid"].ToString();

                        listEstoque.Add(estoqueDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listEstoque;
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                comando.Dispose();
                conexao.Desconectar();
            }
        }
    }
}
