using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Item;
using SAPB1.IDAL.Item;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Item
{
    public class ItemDAL : IItem
    {

        public IList<ItemDTO> Listar(ItemDTO itemDTO)
        {

            HanaConexao conexaoHana = new HanaConexao();
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                string query = $@"SELECT i.""ItemCode"", i.""ItemName"", i.""DfltWH"", COALESCE(t1.""WhsName"", '') AS WhsName FROM OITM i LEFT JOIN OWHS t1 ON i.""DfltWH"" = t1.""WhsCode"" WHERE ";
                query += $@"i.""SellItem"" = '{itemDTO.SellItem}' ";

                if (!string.IsNullOrEmpty(itemDTO.validFor))
                {
                    query += $@"AND i.""validFor"" = '{itemDTO.validFor}' ";
                }

                query += $@"ORDER BY i.""ItemName""";
                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query, conexaoHana);

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
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();

                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT i.ItemCode, i.ItemName, i.DfltWH, COALESCE(t1.WhsName,'') AS 'WhsName' FROM OITM i LEFT JOIN OWHS t1 ON i.DfltWH = t1.WhsCode WHERE ");
                stb.Append("i.SellItem = @SellItem ");

                if (!string.IsNullOrEmpty(itemDTO.validFor))
                {
                    stb.Append("AND i.validFor = @ValidFor ");
                    cmd.Parameters.AddWithValue("@ValidFor", itemDTO.validFor);
                }

                stb.Append("ORDER BY i.ItemName");

                cmd.Parameters.AddWithValue("@SellItem", itemDTO.SellItem);

                try
                {
                    cmd.Connection = conexao.Conexao;
                    cmd.CommandText = stb.ToString();

                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
        }

        private IList<ItemDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<ItemDTO> listItem = new List<ItemDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.ItemName = rdr["ItemName"].ToString();
                    itemDTO.DfltWH = rdr["DfltWH"].ToString();
                    itemDTO.WareHouseName = rdr["WhsName"].ToString();

                    listItem.Add(itemDTO);
                }
            }

            rdr.Close();

            return listItem;
        }

        private IList<ItemDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<ItemDTO> listItem = new List<ItemDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemCode = dr["ItemCode"].ToString();
                    itemDTO.ItemName = dr["ItemName"].ToString();
                    itemDTO.DfltWH = dr["DfltWH"].ToString();
                    itemDTO.WareHouseName = dr["WhsName"].ToString();

                    listItem.Add(itemDTO);
                }
            }

            return listItem;
        }

        public IList<ItemDTO> BuscarInfoItem(ItemDTO itemDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<ItemDTO> listItem = new List<ItemDTO>();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string queryComprimento = $@"SELECT COALESCE(CAST(CAST(T0.""SLength1"" AS decimal(19,0)) AS varchar),'0') AS ""Comprimento"" FROM OITM T0 WHERE T0.""ItemCode"" = '{itemDTO.ItemCode}'";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(queryComprimento);
                    if (dt.Rows.Count > 0)

                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            itemDTO.Comprimento = Convert.ToDouble(dr["Comprimento"]);
                        }
                    }
                    else
                    {
                        itemDTO.Comprimento = 0;
                    }

                }
                catch (Exception err)
                {
                    throw new Exception(err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }

                if (itemDTO.Comprimento > 0)
                {
                    string queryLote = $@"SELECT DISTINCT
                                     T0.""IntrSerial"" AS Lote
                                    ,ROUND((((T0.""Quantity"" - T0.""IsCommited"")/((CAST((REPLACE(T0.""SuppSerial"",',','.')) AS FLOAT))))*1000),0) AS ""Comprimento(mm)""
                                    ,(T0.""Quantity"" - T0.""IsCommited"")  AS ""Peso Unitário""
                                    ,COUNT(*) AS ""Peças""
                                    ,SUM(T0.""Quantity"" - T0.""IsCommited"") AS ""Peso Total""
                                    FROM OIBT T0
                                    INNER JOIN OITM T1
                                    ON T1.""ItemCode"" = T0.""ItemCode""
                                    INNER JOIN OITB T2
                                    ON T2.""ItmsGrpCod"" = T1.""ItmsGrpCod""
                                    WHERE T0.""Quantity"" > 0
                                    AND (T0.""Quantity"" - T0.""IsCommited"" <> 0)
                                    AND T0.""ItemCode"" = '{itemDTO.ItemCode}'
                                    AND ROUND((((T0.""Quantity"" - T0.""IsCommited"")/((CAST((REPLACE(T0.""SuppSerial"",',','.')) AS FLOAT))))*1000),0) >= 
                                    (CAST({itemDTO.Comprimento.ToString()} AS FLOAT))
                                    GROUP BY  T0.""ItemCode"", T0.""ItemName"", T0.""IntrSerial"", T0.""Quantity"", T0.""SuppSerial"",T0.""IsCommited""
                                    ORDER BY (ROUND((((T0.""Quantity"" - T0.""IsCommited"")/((CAST((REPLACE(T0.""SuppSerial"",',','.')) AS FLOAT))))*1000),0)) ASC";

                    try
                    {
                        DataTable dt = conexaoHana.ExecuteDataTable(queryLote);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                itemDTO.Lote = dr["Lote"].ToString();
                            }
                        }
                        else
                        {
                            itemDTO.Lote = "";
                        }

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

                if (!string.IsNullOrEmpty(itemDTO.Lote))
                {

                    string queryNorma = $@"SELECT ""U_Norma"" AS ""Norma"" FROM [@ESSSBO_CERTIFICADO] WHERE ""U_RI"" = '{itemDTO.Lote}'";

                    try
                    {
                        conexaoHana.Connection();

                        DataTable dt = conexaoHana.ExecuteDataTable(queryNorma);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                itemDTO.Norma = dr["Norma"].ToString();
                            }
                        }
                        else
                        {
                            itemDTO.Norma = "";
                        }

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

                string queryDescricaoAuxiliar = $@"SELECT COALESCE(T0.""U_ComprFixo"",'0') as ""DescricaoAuxiliar"" FROM OITM T0 WHERE T0.""ItemCode"" = '{itemDTO.ItemCode}'";
                try
                {
                    DataTable dt = conexaoHana.ExecuteDataTable(queryDescricaoAuxiliar);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            itemDTO.DescricaoAuxiliar = Convert.ToDouble(dr["DescricaoAuxiliar"].ToString());
                        }
                    }
                    else
                    {
                        itemDTO.DescricaoAuxiliar = 0;
                    }

                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }

                listItem.Add(itemDTO);
            }

            else
            {
                SqlServerConexao conexao = new SqlServerConexao();

                SqlCommand cmd = new SqlCommand();

                string queryComprimento = $@"SELECT COALESCE(CAST(CAST(T0.SLength1 AS decimal(19,0)) AS varchar),'0') AS 'Comprimento' FROM OITM T0 WHERE T0.ItemCode = '{itemDTO.ItemCode}'";

                try
                {
                    cmd.Connection = conexao.Conexao;
                    cmd.CommandText = queryComprimento;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemDTO.Comprimento = Convert.ToDouble(rdr["Comprimento"]);
                        }
                    }
                    else
                    {
                        itemDTO.Comprimento = 0;
                    }


                    rdr.Close();
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }

                if (itemDTO.Comprimento > 0)
                {
                    string queryLote = $@"SELECT DISTINCT
                                     T0.IntrSerial AS 'Lote'
                                    ,ROUND((((T0.Quantity - T0.IsCommited)/((CAST((REPLACE(T0.SuppSerial,',','.')) AS FLOAT))))*1000),0) AS 'Comprimento(mm)'
                                    ,(T0.Quantity - T0.IsCommited)  AS 'Peso Unitário'
                                    ,COUNT(*) AS 'Peças'
                                    ,SUM(T0.Quantity - T0.IsCommited) AS 'Peso Total'
                                    FROM OIBT T0
                                    INNER JOIN OITM T1
                                    ON T1.ItemCode = T0.ItemCode
                                    INNER JOIN OITB T2
                                    ON T2.ItmsGrpCod = T1.ItmsGrpCod
                                    WHERE T0.Quantity > 0
                                    AND (T0.Quantity - T0.IsCommited <> 0)
                                    AND T0.ItemCode = '{itemDTO.ItemCode}'
                                    AND ROUND((((T0.Quantity - T0.IsCommited)/((CAST((REPLACE(T0.SuppSerial,',','.')) AS FLOAT))))*1000),0) >= 
                                    (CONVERT (float,{itemDTO.Comprimento.ToString()}))
                                    GROUP BY  T0.ItemCode,  T0.ItemName, T0.IntrSerial, T0.Quantity,T0.SuppSerial,T0.IsCommited
                                    ORDER BY (ROUND((((T0.Quantity - T0.IsCommited)/((CAST((REPLACE(T0.SuppSerial,',','.')) AS FLOAT))))*1000),0)) ASC";

                    try
                    {
                        cmd.Connection = conexao.Conexao;
                        cmd.CommandText = queryLote;

                        conexao.Conectar();

                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                itemDTO.Lote = rdr["Lote"].ToString();
                            }
                        }
                        else
                        {
                            itemDTO.Lote = "";
                        }


                        rdr.Close();
                    }
                    catch (Exception er)
                    {
                        throw new Exception("Erro no banco de dados: " + er.Message);
                    }
                    finally
                    {
                        conexao.Desconectar();
                    }


                    if (!string.IsNullOrEmpty(itemDTO.Lote))
                    {

                        string queryNorma = $@"SELECT U_Norma AS 'Norma' FROM[@ESSSBO_CERTIFICADO] WHERE U_RI = '{itemDTO.Lote}'";

                        try
                        {
                            cmd.Connection = conexao.Conexao;
                            cmd.CommandText = queryNorma;

                            conexao.Conectar();

                            SqlDataReader rdr = cmd.ExecuteReader();

                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    itemDTO.Norma = rdr["Norma"].ToString();
                                }
                            }
                            else
                            {
                                itemDTO.Norma = "";
                            }

                            rdr.Close();
                        }
                        catch (Exception er)
                        {
                            throw new Exception("Erro no banco de dados: " + er.Message);
                        }
                        finally
                        {
                            conexao.Desconectar();
                        }
                    }
                }

                string queryDescricaoAuxiliar = $@"SELECT COALESCE(T0.[U_ComprFixo],'0') as 'DescricaoAuxiliar' FROM OITM T0 WHERE T0.[ItemCode] = '{itemDTO.ItemCode}'";
                try
                {
                    cmd.Connection = conexao.Conexao;
                    cmd.CommandText = queryDescricaoAuxiliar;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            itemDTO.DescricaoAuxiliar = Convert.ToDouble(rdr["DescricaoAuxiliar"].ToString());
                        }
                    }
                    else
                    {
                        itemDTO.DescricaoAuxiliar = 0;
                    }

                    rdr.Close();
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }

                listItem.Add(itemDTO);
            }

            return listItem;
        }


        public IList<ItemDTO> BuscarInfoQtd(ItemDTO itemDTO)
        {
            IList<ItemDTO> listItem = new List<ItemDTO>();

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                if (itemDTO.Pecas > 0 && itemDTO.Comprimento > 0)
                {
                    string QtdMetros = $@"SELECT CAST({itemDTO.Pecas.ToString()} AS FLOAT) * CAST({itemDTO.Comprimento.ToString()} AS FLOAT)/1000 FROM DUMMY";

                    try
                    {
                        conexaoHana.Connection();

                        DataTable dt = conexaoHana.ExecuteDataTable(QtdMetros);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                itemDTO.QtdMetro = Convert.ToDouble(dr[0]);
                            }
                        }
                        else
                        {
                            itemDTO.QtdMetro = 0;
                        }
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
                    itemDTO.QtdMetro = 0;
                }

                if (itemDTO.QtdMetro > 0 && !string.IsNullOrEmpty(itemDTO.Lote) && !string.IsNullOrEmpty(itemDTO.ItemCode))
                {
                    string Peso = $@"SELECT DISTINCT IFNULL(MAX(IFNULL(CAST(REPLACE(T0.""SuppSerial"", ',', '.') AS DECIMAL(19,9)), 1) * CAST(REPLACE('0,001', ',', '.') AS FLOAT)), 0)
                                 FROM OIBT T0
                                 WHERE
                                 T0.""ItemCode"" = 'CT00086'
                                 AND T0.""IntrSerial"" = '1'";

                    try
                    {
                        conexaoHana.Connection();

                        DataTable dt = conexaoHana.ExecuteDataTable(Peso);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                itemDTO.Peso = Convert.ToDouble(dr[0]);
                            }
                        }
                        else
                        {
                            itemDTO.Peso = 0;
                        }

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
                    itemDTO.Peso = 0;
                }

                listItem.Add(itemDTO);
            }
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();

                SqlCommand cmd = new SqlCommand();


                if (itemDTO.Pecas > 0 && itemDTO.Comprimento > 0)
                {
                    string QtdMetros = $@"SELECT CONVERT(float,{itemDTO.Pecas.ToString()}) * CONVERT (float,{itemDTO.Comprimento.ToString()})/1000";

                    try
                    {
                        cmd.Connection = conexao.Conexao;
                        cmd.CommandText = QtdMetros;

                        conexao.Conectar();

                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                itemDTO.QtdMetro = Convert.ToDouble(rdr[0]);
                            }
                        }
                        else
                        {
                            itemDTO.QtdMetro = 0;
                        }


                        rdr.Close();
                    }
                    catch (Exception er)
                    {
                        throw new Exception("Erro no banco de dados: " + er.Message);
                    }
                    finally
                    {
                        conexao.Desconectar();
                    }
                }
                else
                {
                    itemDTO.QtdMetro = 0;
                }


                if (itemDTO.QtdMetro > 0 && !string.IsNullOrEmpty(itemDTO.Lote) && !string.IsNullOrEmpty(itemDTO.ItemCode))
                {
                    string Peso = $@"SELECT DISTINCT MAX(ISNULL(CAST(REPLACE(T0.[SuppSerial], ',', '.') AS DECIMAL(19,9)), 1) * CONVERT(float,{itemDTO.QtdMetro}))
                                 FROM OIBT T0 
                                 WHERE 
                                 T0.ItemCode = '{itemDTO.ItemCode}'
                                 AND T0.IntrSerial = '{itemDTO.Lote}'";

                    try
                    {
                        cmd.Connection = conexao.Conexao;
                        cmd.CommandText = Peso;

                        conexao.Conectar();

                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                itemDTO.Peso = Convert.ToDouble(rdr[0]);
                            }
                        }
                        else
                        {
                            itemDTO.Peso = 0;
                        }


                        rdr.Close();
                    }
                    catch (Exception er)
                    {
                        throw new Exception("Erro no banco de dados: " + er.Message);
                    }
                    finally
                    {
                        conexao.Desconectar();
                    }
                }
                else
                {
                    itemDTO.Peso = 0;
                }

                listItem.Add(itemDTO);

            }
            return listItem;
        }

        public IList<ItemDTO> BuscarItemPorId(ItemDTO itemDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT i.""ItemCode"", i.""ItemName"", i.""DfltWH"", COALESCE(t1.""WhsName"",'') AS ""WhsName"" FROM OITM i LEFT JOIN OWHS t1 ON i.""DfltWH"" = t1.""WhsCode"" WHERE i.""SellItem"" = '{itemDTO.SellItem}' AND i.""ItemCode"" = '{itemDTO.ItemCode}' ORDER BY i.""ItemName""";

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();

                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT i.ItemCode, i.ItemName, i.DfltWH, COALESCE(t1.WhsName,'') AS 'WhsName' FROM OITM i LEFT JOIN OWHS t1 ON i.DfltWH = t1.WhsCode WHERE ");
                stb.Append("i.SellItem = @SellItem AND i.ItemCode = @ItemCode ");

                stb.Append("ORDER BY i.ItemName");

                cmd.Parameters.AddWithValue("@SellItem", itemDTO.SellItem);
                cmd.Parameters.AddWithValue("@ItemCode", itemDTO.ItemCode);

                try
                {
                    cmd.Connection = conexao.Conexao;
                    cmd.CommandText = stb.ToString();

                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }

        }

        public IList<ItemDTO> ListarPorCategoria(ItemDTO itemDTO, List<string> listCategorias)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT i.""ItemCode"", i.""ItemName"", i.""DfltWH"", COALESCE(t1.""WhsName"",'') AS ""WhsName"" FROM OITM i LEFT JOIN OWHS t1 ON i.""DfltWH"" = t1.""WhsCode"" WHERE ""U_ItemSalesSite"" = '01' ";

                if (listCategorias.Count > 0)
                {
                    query += "AND (";
                    for (int i = 0; i < listCategorias.Count; i++)
                    {
                        query += $@"""ItmsGrpCod"" = '{listCategorias[i]}'" + i.ToString() + " ";

                        if (i < (listCategorias.Count - 1))
                            query += "OR ";
                    }

                    query += ") ";
                }

                if (!string.IsNullOrEmpty(itemDTO.validFor))
                {
                    query += $@"AND i.""validFor"" = '{itemDTO.validFor}'";
                }

                query += $@"ORDER BY i.""ItemName""";


                try
                {
                    conexaoHana.Connection();

                    return PopularDadosHana(query, conexaoHana);
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
                SqlServerConexao conexao = new SqlServerConexao();

                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT i.ItemCode, i.ItemName, i.DfltWH, COALESCE(t1.WhsName,'') AS 'WhsName' FROM OITM i LEFT JOIN OWHS t1 ON i.DfltWH = t1.WhsCode WHERE U_ItemSalesSite = '01' ");
                //stb.Append("i.SellItem = @SellItem ");



                if (listCategorias.Count > 0)
                {
                    stb.Append("AND (");
                    for (int i = 0; i < listCategorias.Count; i++)
                    {
                        stb.Append("ItmsGrpCod = @Grupo" + i.ToString() + " ");
                        cmd.Parameters.AddWithValue("@Grupo" + i.ToString(), listCategorias[i]);

                        if (i < (listCategorias.Count - 1))
                            stb.Append("OR ");
                    }

                    stb.Append(") ");
                }

                if (!string.IsNullOrEmpty(itemDTO.validFor))
                {
                    stb.Append("AND i.validFor = @ValidFor ");
                    cmd.Parameters.AddWithValue("@ValidFor", itemDTO.validFor);
                }

                stb.Append("ORDER BY i.ItemName");

                //cmd.Parameters.AddWithValue("@SellItem", itemDTO.SellItem);

                try
                {
                    cmd.Connection = conexao.Conexao;
                    cmd.CommandText = stb.ToString();

                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
        }
    }
}
