using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;
using System.Data.SqlClient;
using SAPB1.IDAL.Administracao.Configuracao;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Administracao.Configuracao
{
    public class PaisDAL : IPais
    {
        public IList<PaisDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            StringBuilder stb = new StringBuilder();
            stb.Append($@"SELECT * FROM OCRY ORDER BY ""Name""");

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(stb.ToString());
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
        }

        private IList<PaisDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<PaisDTO> listPaises = new List<PaisDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
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

        private IList<PaisDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();
            DataTable dt = conexaoHana.ExecuteDataTable(query);
            IList<PaisDTO> listPaises = new List<PaisDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PaisDTO paisDTO = new PaisDTO();
                    paisDTO.Name = dr["Name"].ToString();
                    paisDTO.CntCodNum = dr["CntCodNum"].ToString();

                    listPaises.Add(paisDTO);
                }
            }

            return listPaises;
        }

        public IList<PaisDTO> BuscarPorSigla(string sigla)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string query = $@"SELECT * FROM OCRY WHERE ""Code"" = '{sigla}'";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query);

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
                catch (SqlException er)
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
}
