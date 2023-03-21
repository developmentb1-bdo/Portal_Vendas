/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.SqlServerDAL.ParceiroNegocio
{
    /// <summary>
    /// Tabela OCRG do SAP B1.
    /// </summary>
    public sealed class GrupoDAL : IGrupo
    {


        public IList<GrupoDTO> Listar(GroupType groupType)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<GrupoDTO> listGrupoDTO = new List<GrupoDTO>();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""GroupCode"", ""GroupName"", ""GroupType"", ""Locked"", ""DataSource"", ""UserSign"", ""PriceList"", ""DiscRel"" FROM OCRG ";
                if (groupType == GroupType.Client)
                    query += $@"WHERE ""GroupType"" = 'C'";

                if (groupType == GroupType.Supplier)
                    query += $@"WHERE ""GroupType"" = 'S'";

                if (groupType == GroupType.Lead)
                    query += $@"WHERE ""GroupType"" = 'L'";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    foreach (DataRow dr in dt.Rows)
                    {
                        GrupoDTO grupoDTO = new GrupoDTO();
                        grupoDTO = ObterGrupoHanaDTO(dr);

                        listGrupoDTO.Add(grupoDTO);
                    }
                }
                catch (Exception erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                string tSQLBase = "SELECT GroupCode, GroupName, GroupType, Locked, DataSource, UserSign, PriceList, DiscRel FROM OCRG ";

                SqlServerConexao conexao = new SqlServerConexao();
                try
                {
                    StringBuilder tSQL = new StringBuilder();
                    tSQL.Append(tSQLBase);

                    if (groupType == GroupType.Client)
                        tSQL.Append("WHERE GroupType = 'C'");

                    if (groupType == GroupType.Supplier)
                        tSQL.Append("WHERE GroupType = 'S'");

                    if (groupType == GroupType.Lead)
                        tSQL.Append("WHERE GroupType = 'L'");

                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                    SqlDataReader dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        GrupoDTO grupoDTO = new GrupoDTO();
                        grupoDTO = ObterGrupoDTO(dataReader);

                        listGrupoDTO.Add(grupoDTO);
                    }
                    dataReader.Close();
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
            return listGrupoDTO;

        }

        private GrupoDTO ObterGrupoDTO(SqlDataReader dataReader)
        {
            GrupoDTO grupoDTO = new GrupoDTO();

            if (dataReader.HasRows)
            {
                grupoDTO.GroupCode = Convert.ToInt32(dataReader["GroupCode"]);
                grupoDTO.GroupName = Convert.ToString(dataReader["GroupName"]);
                grupoDTO.GroupType = Convert.ToChar(dataReader["GroupType"]);
                grupoDTO.Locked = Convert.ToChar(dataReader["Locked"]);
                grupoDTO.DataSource = Convert.ToChar(dataReader["DataSource"]);
                grupoDTO.UserSign = Convert.ToInt32(dataReader["UserSign"]);
                grupoDTO.PriceList = ((!dataReader["PriceList"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["PriceList"]) : 0);
                grupoDTO.DiscRel = Convert.ToChar(dataReader["DiscRel"]);
            }
            return grupoDTO;
        }

        private GrupoDTO ObterGrupoHanaDTO(DataRow dr)
        {
            GrupoDTO grupoDTO = new GrupoDTO();

            grupoDTO.GroupCode = Convert.ToInt32(dr["GroupCode"]);
            grupoDTO.GroupName = Convert.ToString(dr["GroupName"]);
            grupoDTO.GroupType = Convert.ToChar(dr["GroupType"]);
            grupoDTO.Locked = Convert.ToChar(dr["Locked"]);
            grupoDTO.DataSource = Convert.ToChar(dr["DataSource"]);
            grupoDTO.UserSign = Convert.ToInt32(dr["UserSign"]);
            grupoDTO.PriceList = ((!dr["PriceList"].Equals(DBNull.Value)) ? Convert.ToInt32(dr["PriceList"]) : 0);
            grupoDTO.DiscRel = Convert.ToChar(dr["DiscRel"]);

            return grupoDTO;
        }
    }
}