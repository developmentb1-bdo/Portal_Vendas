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
    public class TransacaoDAL
    {

        public IList<TransacaoDTO> Listar(int codigo)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<TransacaoDTO> listTransacao = new List<TransacaoDTO>();

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
                        foreach (DataRow dr in dt.Rows)
                        {
                            TransacaoDTO transacaoDTO = new TransacaoDTO();
                            //territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                            //territorioDTO.Descript = rdr["descript"].ToString();

                            listTransacao.Add(transacaoDTO);
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
                            TransacaoDTO transacaoDTO = new TransacaoDTO();
                            //territorioDTO.TerritryId = Convert.ToInt32(rdr["territryID"]);
                            //territorioDTO.Descript = rdr["descript"].ToString();

                            listTransacao.Add(transacaoDTO);
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
            return listTransacao;
        }
    }
}
