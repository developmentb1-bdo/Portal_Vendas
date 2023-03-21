using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Anexo;
using SAPB1.IDAL.Anexo;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Anexo
{
    public class AnexoDAL : IAnexo
    {
        public IList<AnexoDTO> ListarTodosAnexosPorAbsEntry(string absEntry)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<AnexoDTO> listAnexos = new List<AnexoDTO>();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM ATC1 WHERE ""AbsEntry"" = '{absEntry}' ORDER BY ""Line"" ASC";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listAnexos.Add(new AnexoDTO()
                            {
                                AbsEntry = dr["AbsEntry"].ToString(),
                                Line = dr["Line"].ToString(),
                                NomeArquivo = dr["FileName"].ToString(),
                                Date = (dr["Date"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(dr["Date"])),
                                Extensao = dr["FileExt"].ToString()
                            });
                        }
                    }

                    return listAnexos;
                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de dados: " + err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM ATC1 WHERE AbsEntry = @AbsEntry ORDER BY Line ASC");

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);
                    comando.Parameters.AddWithValue("@AbsEntry", absEntry);

                    SqlDataReader rdr = comando.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            listAnexos.Add(new AnexoDTO()
                            {
                                AbsEntry = rdr["AbsEntry"].ToString(),
                                Line = rdr["Line"].ToString(),
                                NomeArquivo = rdr["FileName"].ToString(),
                                Date = (rdr["Date"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["Date"])),
                                Extensao = rdr["FileExt"].ToString()
                            });
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

                    return listAnexos;
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
        }
    }
}
