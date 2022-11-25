using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.GrupoItem;
using SAPB1.DTO.GrupoItem;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.GrupoItem
{
    public class GrupoItemDAL : IGrupoItem
    {

        public IList<GrupoItemDTO> ObterTodos()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                string query = $@"SELECT * FROM OITB WHERE ""Locked"" = 'N' ORDER BY ""ItmsGrpNam""";

                try
                {
                    conexaoHana.Connection();

                    IList<GrupoItemDTO> listGrupo = new List<GrupoItemDTO>();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listGrupo.Add(new GrupoItemDTO()
                            {
                                ItmsGrpCod = Convert.ToInt32(dr["ItmsGrpCod"].ToString()),
                                ItmsGrpNam = dr["ItmsGrpNam"].ToString()
                            });
                        }
                    }

                    return listGrupo;
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
                SqlServerConexao _conexao = new SqlServerConexao();
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OITB WHERE Locked = 'N' ORDER BY ItmsGrpNam");

                SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);

                try
                {
                    _conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    IList<GrupoItemDTO> listGrupo = new List<GrupoItemDTO>();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            listGrupo.Add(new GrupoItemDTO()
                            {
                                ItmsGrpCod = Convert.ToInt32(rdr["ItmsGrpCod"].ToString()),
                                ItmsGrpNam = rdr["ItmsGrpNam"].ToString()
                            });
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

                    return listGrupo;
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    _conexao.Desconectar();
                }
            }
        }
    }
}
