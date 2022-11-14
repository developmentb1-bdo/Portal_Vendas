using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;
using System.Data.SqlClient;
using SAPB1.IDAL.Administracao.Configuracao;

namespace SAPB1.SqlServerDAL.Administracao.Configuracao
{
    public class IdiomaDAL:IIdioma
    {
        public IList<IdiomaDTO> Listar()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append($@"SELECT * from OLNG ORDER BY ""Name""");

            SqlServerConexao conexao = new SqlServerConexao();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
                cmd.Dispose();
            }
        }

        private IList<IdiomaDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<IdiomaDTO> listIdioma = new List<IdiomaDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    IdiomaDTO idiomaDTO = new IdiomaDTO();
                    idiomaDTO.Name = rdr["Name"].ToString();
                    idiomaDTO.Code = Convert.ToInt32(rdr["Code"].ToString());

                    listIdioma.Add(idiomaDTO);
                }
            }

            rdr.Close();

            return listIdioma;
        }
    }
}
