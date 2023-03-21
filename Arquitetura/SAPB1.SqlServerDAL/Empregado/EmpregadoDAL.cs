using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Empregado;
using SAPB1.IDAL.Empregado;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Empregado
{
    public class EmpregadoDAL : IEmpregado
    {
        string queryPadrao = $@"SELECT ""empID"", ""Active"", ""lastName"", ""firstName"", ""position"" FROM OHEM ";

        SqlCommand cmd = new SqlCommand();

        public IList<EmpregadoDTO> Listar(EmpregadoDTO empregadoDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                queryPadrao += $@"WHERE ""Active"" = '{empregadoDTO.Active}' ";

                if (empregadoDTO.Posicao != null)
                {
                    if (empregadoDTO.Posicao.PosId != 0)
                    {
                        queryPadrao += $@"AND ""position"" = '{empregadoDTO.Posicao.PosId}'";
                    }
                }

                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();

                    return PopularDadosHana(queryPadrao);
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
                stb.Append(queryPadrao);
                stb.Append("WHERE Active = @Active ");
                cmd.Parameters.AddWithValue("@Active", empregadoDTO.Active);

                if (empregadoDTO.Posicao != null)
                {
                    if (empregadoDTO.Posicao.PosId != 0)
                    {
                        stb.Append("AND position = @Position ");
                        cmd.Parameters.AddWithValue("@Position", empregadoDTO.Posicao.PosId);
                    }
                }

                SqlServerConexao conexao = new SqlServerConexao();

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

        private IList<EmpregadoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<EmpregadoDTO> listEmpregados = new List<EmpregadoDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    EmpregadoDTO empregadoDTO = new EmpregadoDTO();
                    empregadoDTO.EmpID = Convert.ToInt32(rdr["empID"]);
                    empregadoDTO.LastName = rdr["firstName"].ToString() + " " + rdr["lastName"].ToString();

                    listEmpregados.Add(empregadoDTO);
                }
            }

            return listEmpregados;
        }

        private IList<EmpregadoDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();

            IList<EmpregadoDTO> listEmpregados = new List<EmpregadoDTO>();

            DataTable dt = conexaoHana.ExecuteDataTable(query);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmpregadoDTO empregadoDTO = new EmpregadoDTO();
                    empregadoDTO.EmpID = Convert.ToInt32(dr["empID"]);
                    empregadoDTO.LastName = dr["firstName"].ToString() + " " + dr["lastName"].ToString();

                    listEmpregados.Add(empregadoDTO);
                }
            }

            return listEmpregados;
        }
    }
}
