using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Municipio;
using SAPB1.IDAL.Municipio;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Municipio
{
    public class MunicipioDAL:IMunicipio
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<MunicipioDTO> Listar(MunicipioDTO municipioDTO)
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

        public IList<MunicipioDTO> RetornarCodigoMunicipioPorNome(string nome)
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
