using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Municipio;
using SAPB1.IDAL.Municipio;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Municipio
{
    public class MunicipioDAL : IMunicipio
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<MunicipioDTO> Listar(MunicipioDTO municipioDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OCNT WHERE ""State"" = '{municipioDTO.Estado.Code}'";
                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query, conexaoHana);

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
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OCNT ");
                stb.Append("WHERE State = @State");

                cmd.Parameters.AddWithValue("@State", municipioDTO.Estado.Code);

                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                try
                {
                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conexao.Desconectar();
                }
            }

        }

        private IList<MunicipioDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<MunicipioDTO> listMunicipio = new List<MunicipioDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    MunicipioDTO municipioDTO = new MunicipioDTO();
                    municipioDTO.AbsId = Convert.ToInt32(rdr["AbsId"].ToString());
                    municipioDTO.Code = Convert.ToInt32(rdr["Code"].ToString());
                    municipioDTO.Name = rdr["Name"].ToString();

                    listMunicipio.Add(municipioDTO);
                }
            }

            rdr.Close();
            rdr.Dispose();
            cmd.Dispose();

            return listMunicipio;
        }

        private IList<MunicipioDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<MunicipioDTO> listMunicipio = new List<MunicipioDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MunicipioDTO municipioDTO = new MunicipioDTO();
                    municipioDTO.AbsId = Convert.ToInt32(dr["AbsId"].ToString());
                    municipioDTO.Code = Convert.ToInt32(dr["Code"].ToString());
                    municipioDTO.Name = dr["Name"].ToString();

                    listMunicipio.Add(municipioDTO);
                }
            }

            return listMunicipio;
        }

        public IList<MunicipioDTO> RetornarCodigoMunicipioPorNome(string nome)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OCNT WHERE ""Name"" LIKE '%{nome}%'";

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query, conexaoHana);

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
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OCNT ");
                stb.Append("WHERE Name LIKE @Name");

                cmd.Parameters.AddWithValue("@Name", "%" + nome + "%");

                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                try
                {
                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conexao.Desconectar();
                }
            }

        }
    }
}
