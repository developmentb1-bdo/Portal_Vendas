using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Servico
{
    public class OpcaoTipoGarantiaDAL : IOpcaoTipoGarantia
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<OpcaoTipoGarantiaDTO> ObterOpcoesTipoGaratiaPorGarantia(int codTpGarantia)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [@@RSD_OPTPGARANT] WHERE U_CodeTpGar = @CodTipoGarantia");

            SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);
            cmd.Parameters.AddWithValue("@CodTipoGarantia", codTpGarantia);

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<OpcaoTipoGarantiaDTO> listOpcao = new List<OpcaoTipoGarantiaDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listOpcao.Add(new OpcaoTipoGarantiaDTO()
                        {
                            Code = Convert.ToInt32(rdr["Code"]),
                            U_Ativo = rdr["U_Ativo"].ToString(),
                            U_CodeTpGar = Convert.ToInt32(rdr["U_CodeTpGar"]),
                            U_NomeOpTpGarant = rdr["U_NomeOpTpGarant"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listOpcao;
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
