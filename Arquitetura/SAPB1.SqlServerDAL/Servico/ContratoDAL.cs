using SAPB1.DTO.Servico;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ContratoDAL
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ContratoDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<ContratoDTO> listContrato = new List<ContratoDTO>();


            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OTER";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow rdr in dt.Rows)
                        {
                            ContratoDTO transacaoDTO = new ContratoDTO();
                            //territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                            //territorioDTO.Descript = rdr["descript"].ToString();

                            listContrato.Add(transacaoDTO);
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
                            ContratoDTO transacaoDTO = new ContratoDTO();
                            //territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                            //territorioDTO.Descript = rdr["descript"].ToString();

                            listContrato.Add(transacaoDTO);
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
            return listContrato;
        }

    }
}
