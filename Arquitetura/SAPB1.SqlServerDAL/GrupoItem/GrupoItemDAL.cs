using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.GrupoItem;
using SAPB1.DTO.GrupoItem;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.GrupoItem
{
    public class GrupoItemDAL : IGrupoItem
    {
        SqlServerConexao _conexao;

        public GrupoItemDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public IList<GrupoItemDTO> ObterTodos()
        {
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
