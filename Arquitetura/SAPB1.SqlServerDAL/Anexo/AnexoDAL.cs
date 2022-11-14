using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Anexo;
using SAPB1.IDAL.Anexo;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Anexo
{
    public class AnexoDAL:IAnexo
    {
        public IList<AnexoDTO> ListarTodosAnexosPorAbsEntry(string absEntry)
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

                IList<AnexoDTO> listAnexos = new List<AnexoDTO>();

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        listAnexos.Add(new AnexoDTO()
                        {
                            AbsEntry = rdr["AbsEntry"].ToString(),
                            Line = rdr["Line"].ToString(),
                            NomeArquivo = rdr["FileName"].ToString(),
                            Date = (rdr["Date"].ToString().Equals("")?DateTime.MinValue:Convert.ToDateTime(rdr["Date"])),
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
