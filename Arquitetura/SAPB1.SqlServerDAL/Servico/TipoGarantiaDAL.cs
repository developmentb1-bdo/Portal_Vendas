using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.IDAL.Servico;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Servico
{
    public class TipoGarantiaDAL : ITipoGarantia
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<TipoGarantiaDTO> ObterTipoGarantiaAtivas()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [@@RSD_TPGARANT] WHERE U_Ativo = 'S'");

            SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<TipoGarantiaDTO> listTipoGarantia = new List<TipoGarantiaDTO>();

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        listTipoGarantia.Add(new TipoGarantiaDTO()
                        {
                            Code = Convert.ToInt32(rdr["Code"]),
                            U_Ativo = rdr["U_Ativo"].ToString(),
                            U_NomeTpGarant = rdr["U_NomeTpGarant"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listTipoGarantia;
            }
            catch(Exception er)
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
