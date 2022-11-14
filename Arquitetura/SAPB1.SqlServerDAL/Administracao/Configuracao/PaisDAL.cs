using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;
using System.Data.SqlClient;
using SAPB1.IDAL.Administracao.Configuracao;

namespace SAPB1.SqlServerDAL.Administracao.Configuracao
{
    public class PaisDAL:IPais
    {
        public IList<PaisDTO> Listar()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM OCRY ORDER BY Name");

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

        private IList<PaisDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<PaisDTO> listPaises = new List<PaisDTO>();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    PaisDTO paisDTO = new PaisDTO();
                    paisDTO.Name = rdr["Name"].ToString();
                    paisDTO.CntCodNum = rdr["CntCodNum"].ToString();

                    listPaises.Add(paisDTO);
                }
            }

            rdr.Close();

            return listPaises;
        }

        public IList<PaisDTO> BuscarPorSigla(string sigla)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM OCRY WHERE Code = @Code");

            SqlServerConexao conexao = new SqlServerConexao();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Code", sigla);

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch(SqlException er)
            {
                throw new Exception(er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }
    }
}
