using SAPB1.DTO.Territorio;
using SAPB1.IDAL.Territorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SAPB1.SqlServerDAL.Territorio
{
    public class TerritorioDAL : ITerritorio
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<TerritorioDTO> Listar()
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

                IList<TerritorioDTO> listTerritorios = new List<TerritorioDTO>();

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        TerritorioDTO territorioDTO = new TerritorioDTO();
                        territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                        territorioDTO.Descript = rdr["descript"].ToString();

                        listTerritorios.Add(territorioDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listTerritorios;
            }
            catch(SqlException er)
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
