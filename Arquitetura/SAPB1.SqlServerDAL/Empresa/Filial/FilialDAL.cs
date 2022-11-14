using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SAPB1.IDAL.Empresa.Filial;
using SAPB1.DTO.Empresa.Filial;

namespace SAPB1.SqlServerDAL.Empresa.Filial
{
    public class FilialDAL : IFilial
    {
        public IList<FilialDTO> Listar(FilialDTO filialDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("BPLId, ");
            stb.Append("BPLName, ");
            stb.Append("BPLFrName, ");
            stb.Append("TaxIdNum, ");
            stb.Append("TaxIdNum2, ");
            stb.Append("TaxIdNum3, ");
            stb.Append("MainBPL, ");
            stb.Append("Disabled ");
            //stb.Append("U_MatrizGrupo, ");
            //stb.Append("U_Matriz ");
            stb.Append("FROM OBPL ");

            if (filialDTO != null)
            {
                if (filialDTO.BPLId != 0)
                {
                    stb.Append("WHERE ");

                    if (filialDTO.BPLId != 0 || !string.IsNullOrEmpty(filialDTO.Disabled))
                    {
                        stb.Append("BPLId = @BPLId ");

                        cmd.Parameters.AddWithValue("@BPLId", filialDTO.BPLId);

                        if(!string.IsNullOrEmpty(filialDTO.Disabled))
                        {
                            stb.Append("AND ");
                        }
                    }

                    if(!string.IsNullOrEmpty(filialDTO.Disabled))
                    {
                        stb.Append("Disabled = @Disabled ");

                        cmd.Parameters.AddWithValue("@Disabled", filialDTO.Disabled);
                    }
                }
            }

            stb.Append("ORDER BY BPLName");

            SqlServerConexao conexao = new SqlServerConexao();

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch(SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
                cmd.Dispose();
            }
        }

        private IList<FilialDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<FilialDTO> listFiliais = new List<FilialDTO>();

            if(rdr.HasRows)
            {
                while(rdr.Read())
                {
                    FilialDTO filialDTO = new FilialDTO();
                    filialDTO.BPLId = Convert.ToInt32(rdr["BPLId"].ToString());
                    filialDTO.BPLName = rdr["BPLName"].ToString();
                    filialDTO.BPLFrName = rdr["BPLFrName"].ToString();
                    filialDTO.TaxIdNum = rdr["TaxIdNum"].ToString();
                    filialDTO.TaxIdNum2 = rdr["TaxIdNum2"].ToString();
                    filialDTO.TaxIdNum3 = rdr["TaxIdNum3"].ToString();
                    //filialDTO.U_Matriz = rdr["U_Matriz"].ToString();
                    //filialDTO.U_MatrizGrupo = rdr["U_MatrizGrupo"].ToString();
                    filialDTO.Disabled = rdr["Disabled"].ToString();
                    filialDTO.MainBPL = rdr["MainBPL"].ToString();

                    listFiliais.Add(filialDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listFiliais;
        }
    }
}
