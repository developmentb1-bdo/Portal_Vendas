using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SAPB1.DTO.Utilizacao;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.IDAL.Utilizacao;

namespace SAPB1.SqlServerDAL.Utilizacao
{
    public class UtilizacaoDAL:IUtilizacao
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<UtilizacaoDTO> Listar(UtilizacaoDTO utlizacaoDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("ID, ");
            stb.Append("Usage, ");
            stb.Append("Locked, ");
            stb.Append("UserSign, ");
            stb.Append("TaxOnly, ");
            stb.Append("PostTax, ");
            stb.Append("Descr, ");
            stb.Append("CFOPIIS, ");
            stb.Append("CFOPIOS, ");
            stb.Append("CFOPII, ");
            stb.Append("CFOPOIS, ");
            stb.Append("CFOPOOS, ");
            stb.Append("CFOPOE, ");
            stb.Append("ThirdParty, ");
            //stb.Append("U_ApropCred, ");
            stb.Append("FreeChrgBP ");
            //stb.Append("U_SomaPisCofins ");
            stb.Append("FROM OUSG ");

            if(utlizacaoDTO !=null)
            {
                if(!string.IsNullOrEmpty(utlizacaoDTO.Locked) || utlizacaoDTO.ID !=0)
                {
                    stb.Append("WHERE ");

                    if(!string.IsNullOrEmpty(utlizacaoDTO.Locked))
                    {
                        stb.Append("Locked = @Locked ");

                        cmd.Parameters.AddWithValue("@Locked", utlizacaoDTO.Locked);

                        if(utlizacaoDTO.ID !=0)
                        {
                            stb.Append("AND ");
                        }

                        if(utlizacaoDTO.ID !=0)
                        {
                            stb.Append("ID = @ID ");

                            cmd.Parameters.AddWithValue("@ID", utlizacaoDTO.ID);
                        }
                    }
                }
            }

            stb.Append("ORDER BY Usage");

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch(SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
                cmd.Dispose();
            }
        }

        private IList<UtilizacaoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<UtilizacaoDTO> listUtilizacao = new List<UtilizacaoDTO>();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    CfopDTO cfopDTO = new CfopDTO();

                    UtilizacaoDTO utilizacaoDTO = new UtilizacaoDTO();
                    utilizacaoDTO.ID = Convert.ToInt32(rdr["ID"].ToString());
                    utilizacaoDTO.Usage = rdr["Usage"].ToString();
                    utilizacaoDTO.Locked = rdr["Locked"].ToString();
                    utilizacaoDTO.TaxOnly = rdr["TaxOnly"].ToString();
                    utilizacaoDTO.PostTax = Convert.ToInt32(rdr["PostTax"].ToString());
                    utilizacaoDTO.Descr = rdr["Descr"].ToString();

                    cfopDTO.Code = rdr["CFOPIIS"].ToString();
                    utilizacaoDTO.CFOPIIS = cfopDTO;

                    cfopDTO.Code = rdr["CFOPIOS"].ToString();
                    utilizacaoDTO.CFOPIOS = cfopDTO;

                    cfopDTO.Code = rdr["CFOPII"].ToString();
                    utilizacaoDTO.CFOPII = cfopDTO;

                    cfopDTO.Code = rdr["CFOPOE"].ToString();
                    utilizacaoDTO.CFOPOE = cfopDTO;

                    cfopDTO.Code = rdr["CFOPOIS"].ToString();
                    utilizacaoDTO.CFOPOIS = cfopDTO;

                    cfopDTO.Code = rdr["CFOPOOS"].ToString();
                    utilizacaoDTO.CFOPOOS = cfopDTO;

                    utilizacaoDTO.ThirdParty = rdr["ThirdParty"].ToString();
                    //utilizacaoDTO.U_ApropCred = rdr["U_ApropCred"].ToString();
                    //utilizacaoDTO.U_SomaPisCofins = rdr["U_SomaPisCofins"].ToString();
                    utilizacaoDTO.FreeChrgBP = rdr["FreeChrgBP"].ToString();

                    listUtilizacao.Add(utilizacaoDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listUtilizacao;
        }
    }
}
