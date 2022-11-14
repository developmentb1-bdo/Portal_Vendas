using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ModeloVeiculoDAL : IModeloVeiculo
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ModeloVeiculoDTO> ListarTodosModelos()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT DISTINCT Modelo FROM [@@RSD_MODVEI]");

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ModeloVeiculoDTO> listModelos = new List<ModeloVeiculoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listModelos.Add(new ModeloVeiculoDTO()
                        {
                            Modelo = rdr["Modelo"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listModelos;
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

        public IList<ModeloVeiculoDTO> ListarAnoModeloPorModelo(string modelo)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT DISTINCT AnoModelo FROM [@@RSD_MODVEI] WHERE Modelo = @Modelo");

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Modelo", modelo);

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ModeloVeiculoDTO> listModelos = new List<ModeloVeiculoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listModelos.Add(new ModeloVeiculoDTO()
                        {
                            AnoModelo = rdr["AnoModelo"].ToString(),
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listModelos;
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

        public IList<ModeloVeiculoDTO> ListarEntreEixosPorAnoModelo(string modelo, string anoModelo)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT DISTINCT EntreEixos FROM [@@RSD_MODVEI] WHERE AnoModelo = @AnoModelo AND Modelo = @Modelo");

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@AnoModelo", anoModelo);
            cmd.Parameters.AddWithValue("@Modelo", modelo);

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ModeloVeiculoDTO> listModelos = new List<ModeloVeiculoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listModelos.Add(new ModeloVeiculoDTO()
                        {
                            EntreEixos = rdr["EntreEixos"].ToString(),
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listModelos;
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
