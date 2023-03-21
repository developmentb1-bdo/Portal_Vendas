using SAPB1.DTO.Territorio;
using SAPB1.IDAL.Territorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SAPB1.SqlServerDAL.Territorio
{
    public class TerritorioDAL : ITerritorio
    {

        public IList<TerritorioDTO> Listar()
        {

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<TerritorioDTO> listTerritorios = new List<TerritorioDTO>();
            string query = $@"SELECT * FROM OTER";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            TerritorioDTO territorioDTO = new TerritorioDTO();
                            territorioDTO.TerritryId = Convert.ToInt32(dr["territryID"]);
                            territorioDTO.Descript = dr["descript"].ToString();

                            listTerritorios.Add(territorioDTO);
                        }
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
            }
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OTER");

                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            TerritorioDTO territorioDTO = new TerritorioDTO();
                            territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                            territorioDTO.Descript = rdr["descript"].ToString();

                            listTerritorios.Add(territorioDTO);
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

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
            return listTerritorios;

        }
    }
}
