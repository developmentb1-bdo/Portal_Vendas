using SAPB1.DTO.Servico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ChamadoServicoDAL : IChamadoServico
    {

        public IList<ChamadoServicoDTO> ListarChamadoPorCustomer(string customer)
        {
            IList<ChamadoServicoDTO> listChamados = new List<ChamadoServicoDTO>();
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OSCL WHERE ""customer"" = '{customer}' ORDER BY ""callID"" DESC";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow rdr in dt.Rows)
                        {
                            ChamadoServicoDTO chamadoDTO = new ChamadoServicoDTO();
                            chamadoDTO.callID = (rdr["callID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["callID"]));
                            chamadoDTO.customer = rdr["customer"].ToString();
                            chamadoDTO.closeDate = (rdr["closeDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["closeDate"]));
                            chamadoDTO.createDate = (rdr["createDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["createDate"]));
                            chamadoDTO.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"]));
                            chamadoDTO.itemCode = rdr["itemCode"].ToString();
                            chamadoDTO.U_DataFalha = (rdr["U_DataFalha"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DataFalha"]));
                            chamadoDTO.U_Chassi = rdr["U_Chassi"].ToString();
                            chamadoDTO.U_Modelo = rdr["U_Modelo"].ToString();
                            chamadoDTO.U_KmFal = (rdr["U_KmFal"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamadoDTO.U_Placa = rdr["U_Placa"].ToString();
                            chamadoDTO.U_NumMoto = rdr["U_NumMoto"].ToString();
                            chamadoDTO.U_ModelMoto = rdr["U_ModelMoto"].ToString();
                            chamadoDTO.U_DescFal = rdr["U_DescFal"].ToString();
                            chamadoDTO.U_CausaFal = rdr["U_CausaFal"].ToString();
                            chamadoDTO.U_CorrecaoFal = rdr["U_CorrecaoFal"].ToString();
                            chamadoDTO.U_ObsGerais = rdr["U_ObsGerais"].ToString();
                            chamadoDTO.U_OrdemServ = rdr["U_OrdemServ"].ToString();
                            chamadoDTO.U_NomResp = rdr["U_NomResp"].ToString();
                            chamadoDTO.U_Funcao = rdr["U_Funcao"].ToString();
                            chamadoDTO.U_DtVenda = (rdr["U_DtVenda"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtVenda"]));
                            chamadoDTO.U_DtAbertFal = (rdr["U_DtAbertFal"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtAbertFal"]));
                            chamadoDTO.U_NomCli = rdr["U_NomCli"].ToString();
                            chamadoDTO.U_KmAt = (rdr["U_KmAt"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamadoDTO.AtcEntry = rdr["AtcEntry"].ToString();
                            chamadoDTO.U_Status = rdr["U_Status"].ToString();
                            chamadoDTO.U_TpGarant = rdr["U_TpGarant"].ToString();
                            chamadoDTO.U_SubTipoGarant = rdr["U_SubTipoGarant"].ToString();

                            listChamados.Add(chamadoDTO);
                        }
                    }

                }
                catch (SqlException er)
                {
                    throw new Exception(er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {

                SqlServerConexao conexao = new SqlServerConexao();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OSCL WHERE customer = @customer ORDER BY callID DESC");

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@customer", customer);

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();



                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ChamadoServicoDTO chamadoDTO = new ChamadoServicoDTO();
                            chamadoDTO.callID = (rdr["callID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["callID"]));
                            chamadoDTO.customer = rdr["customer"].ToString();
                            chamadoDTO.closeDate = (rdr["closeDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["closeDate"]));
                            chamadoDTO.createDate = (rdr["createDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["createDate"]));
                            chamadoDTO.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"]));
                            chamadoDTO.itemCode = rdr["itemCode"].ToString();

                            chamadoDTO.U_DataFalha = (rdr["U_DataFalha"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DataFalha"]));
                            chamadoDTO.U_Chassi = rdr["U_Chassi"].ToString();
                            chamadoDTO.U_Modelo = rdr["U_Modelo"].ToString();
                            chamadoDTO.U_KmFal = (rdr["U_KmFal"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamadoDTO.U_Placa = rdr["U_Placa"].ToString();
                            chamadoDTO.U_NumMoto = rdr["U_NumMoto"].ToString();
                            chamadoDTO.U_ModelMoto = rdr["U_ModelMoto"].ToString();
                            chamadoDTO.U_DescFal = rdr["U_DescFal"].ToString();
                            chamadoDTO.U_CausaFal = rdr["U_CausaFal"].ToString();
                            chamadoDTO.U_CorrecaoFal = rdr["U_CorrecaoFal"].ToString();
                            chamadoDTO.U_ObsGerais = rdr["U_ObsGerais"].ToString();
                            chamadoDTO.U_OrdemServ = rdr["U_OrdemServ"].ToString();
                            chamadoDTO.U_NomResp = rdr["U_NomResp"].ToString();
                            chamadoDTO.U_Funcao = rdr["U_Funcao"].ToString();
                            chamadoDTO.U_DtVenda = (rdr["U_DtVenda"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtVenda"]));
                            chamadoDTO.U_DtAbertFal = (rdr["U_DtAbertFal"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtAbertFal"]));
                            chamadoDTO.U_NomCli = rdr["U_NomCli"].ToString();
                            chamadoDTO.U_KmAt = (rdr["U_KmAt"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamadoDTO.AtcEntry = rdr["AtcEntry"].ToString();
                            chamadoDTO.U_Status = rdr["U_Status"].ToString();
                            chamadoDTO.U_TpGarant = rdr["U_TpGarant"].ToString();
                            chamadoDTO.U_SubTipoGarant = rdr["U_SubTipoGarant"].ToString();

                            listChamados.Add(chamadoDTO);
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

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
            return listChamados;
        }

        public ChamadoServicoDTO ListarChamadoPorIdPorCustomer(int callId, string customer)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            ChamadoServicoDTO chamado = new ChamadoServicoDTO();


            if (tipoBD == "Hana")
            {
                string query = $@"SELECT* FROM OSCL WHERE ""customer"" = '{customer}' AND ""callID"" = '{callId}'";
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow rdr in dt.Rows)
                        {
                            chamado.callID = (rdr["callID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["callID"]));
                            chamado.customer = rdr["customer"].ToString();
                            chamado.closeDate = (rdr["closeDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["closeDate"]));
                            chamado.createDate = (rdr["createDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["createDate"]));
                            chamado.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"]));
                            chamado.itemCode = rdr["itemCode"].ToString();

                            chamado.U_DataFalha = (rdr["U_DataFalha"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DataFalha"]));
                            chamado.U_Chassi = rdr["U_Chassi"].ToString();
                            chamado.U_Modelo = rdr["U_Modelo"].ToString();
                            chamado.U_KmFal = (rdr["U_KmFal"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.U_Placa = rdr["U_Placa"].ToString();
                            chamado.U_NumMoto = rdr["U_NumMoto"].ToString();
                            chamado.U_ModelMoto = rdr["U_ModelMoto"].ToString();
                            chamado.U_DescFal = rdr["U_DescFal"].ToString();
                            chamado.U_CausaFal = rdr["U_CausaFal"].ToString();
                            chamado.U_CorrecaoFal = rdr["U_CorrecaoFal"].ToString();
                            chamado.U_ObsGerais = rdr["U_ObsGerais"].ToString();
                            chamado.U_OrdemServ = rdr["U_OrdemServ"].ToString();
                            chamado.U_NomResp = rdr["U_NomResp"].ToString();
                            chamado.U_Funcao = rdr["U_Funcao"].ToString();
                            chamado.U_DtVenda = (rdr["U_DtVenda"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtVenda"]));
                            chamado.U_DtAbertFal = (rdr["U_DtAbertFal"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtAbertFal"]));
                            chamado.U_NomCli = rdr["U_NomCli"].ToString();
                            chamado.U_KmAt = (rdr["U_KmAt"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.AtcEntry = rdr["AtcEntry"].ToString();
                            chamado.U_Status = rdr["U_Status"].ToString();
                            chamado.U_TpGarant = rdr["U_TpGarant"].ToString();
                            chamado.U_SubTipoGarant = rdr["U_SubTipoGarant"].ToString();
                        }
                    }
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OSCL WHERE customer = @customer AND callID = @callID");

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@customer", customer);
                cmd.Parameters.AddWithValue("@callID", callId);

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            chamado.callID = (rdr["callID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["callID"]));
                            chamado.customer = rdr["customer"].ToString();
                            chamado.closeDate = (rdr["closeDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["closeDate"]));
                            chamado.createDate = (rdr["createDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["createDate"]));
                            chamado.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"]));
                            chamado.itemCode = rdr["itemCode"].ToString();

                            chamado.U_DataFalha = (rdr["U_DataFalha"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DataFalha"]));
                            chamado.U_Chassi = rdr["U_Chassi"].ToString();
                            chamado.U_Modelo = rdr["U_Modelo"].ToString();
                            chamado.U_KmFal = (rdr["U_KmFal"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.U_Placa = rdr["U_Placa"].ToString();
                            chamado.U_NumMoto = rdr["U_NumMoto"].ToString();
                            chamado.U_ModelMoto = rdr["U_ModelMoto"].ToString();
                            chamado.U_DescFal = rdr["U_DescFal"].ToString();
                            chamado.U_CausaFal = rdr["U_CausaFal"].ToString();
                            chamado.U_CorrecaoFal = rdr["U_CorrecaoFal"].ToString();
                            chamado.U_ObsGerais = rdr["U_ObsGerais"].ToString();
                            chamado.U_OrdemServ = rdr["U_OrdemServ"].ToString();
                            chamado.U_NomResp = rdr["U_NomResp"].ToString();
                            chamado.U_Funcao = rdr["U_Funcao"].ToString();
                            chamado.U_DtVenda = (rdr["U_DtVenda"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtVenda"]));
                            chamado.U_DtAbertFal = (rdr["U_DtAbertFal"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtAbertFal"]));
                            chamado.U_NomCli = rdr["U_NomCli"].ToString();
                            chamado.U_KmAt = (rdr["U_KmAt"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.AtcEntry = rdr["AtcEntry"].ToString();
                            chamado.U_Status = rdr["U_Status"].ToString();
                            chamado.U_TpGarant = rdr["U_TpGarant"].ToString();
                            chamado.U_SubTipoGarant = rdr["U_SubTipoGarant"].ToString();
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

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
            return chamado;
        }

        public IList<ChamadoServicoDTO> BuscarChamadoPorCustomer(string customer, ChamadoServicoDTO chamadoDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<ChamadoServicoDTO> listChamados = new List<ChamadoServicoDTO>();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OSCL WHERE ""customer"" = '{customer}' ";

                if ((chamadoDTO.createDate != DateTime.MinValue && chamadoDTO.closeDate != DateTime.MinValue) || chamadoDTO.callID != 0)
                {
                    query += "AND ";

                    if (chamadoDTO.callID > 0)
                    {
                        query += $@"""callID"" = '{chamadoDTO.callID}' ";

                        if (chamadoDTO.createDate != DateTime.MinValue && chamadoDTO.closeDate != DateTime.MinValue)
                            query += "AND ";
                    }

                    if (chamadoDTO.createDate != DateTime.MinValue && chamadoDTO.closeDate != DateTime.MinValue)
                    {
                        query += $@"""createDate"" BETWEEN '{chamadoDTO.createDate.ToString("yyyy-MM-dd") + " 00:00:00"}' AND '{chamadoDTO.closeDate.ToString("yyyy-MM-dd") + " 23:59:59"}' ";
                    }
                }

                query += $@"ORDER BY ""callID"" DESC";

                try
                {
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow rdr in dt.Rows)
                        {
                            ChamadoServicoDTO chamado = new ChamadoServicoDTO();
                            chamado.callID = (rdr["callID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["callID"]));
                            chamado.customer = rdr["customer"].ToString();
                            chamado.closeDate = (rdr["closeDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["closeDate"]));
                            chamado.createDate = (rdr["createDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["createDate"]));
                            chamado.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"]));
                            chamado.itemCode = rdr["itemCode"].ToString();
                            chamado.U_DataFalha = (rdr["U_DataFalha"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DataFalha"]));
                            chamado.U_Chassi = rdr["U_Chassi"].ToString();
                            chamado.U_Modelo = rdr["U_Modelo"].ToString();
                            chamado.U_KmFal = (rdr["U_KmFal"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.U_Placa = rdr["U_Placa"].ToString();
                            chamado.U_NumMoto = rdr["U_NumMoto"].ToString();
                            chamado.U_ModelMoto = rdr["U_ModelMoto"].ToString();
                            chamado.U_DescFal = rdr["U_DescFal"].ToString();
                            chamado.U_CausaFal = rdr["U_CausaFal"].ToString();
                            chamado.U_CorrecaoFal = rdr["U_CorrecaoFal"].ToString();
                            chamado.U_ObsGerais = rdr["U_ObsGerais"].ToString();
                            chamado.U_OrdemServ = rdr["U_OrdemServ"].ToString();
                            chamado.U_NomResp = rdr["U_NomResp"].ToString();
                            chamado.U_Funcao = rdr["U_Funcao"].ToString();
                            chamado.U_DtVenda = (rdr["U_DtVenda"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtVenda"]));
                            chamado.U_DtAbertFal = (rdr["U_DtAbertFal"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtAbertFal"]));
                            chamado.U_NomCli = rdr["U_NomCli"].ToString();
                            chamado.U_KmAt = (rdr["U_KmAt"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.AtcEntry = rdr["AtcEntry"].ToString();
                            chamado.U_Status = rdr["U_Status"].ToString();
                            chamado.U_TpGarant = rdr["U_TpGarant"].ToString();
                            chamado.U_SubTipoGarant = rdr["U_SubTipoGarant"].ToString();

                            listChamados.Add(chamado);
                        }
                    }
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
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

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OSCL WHERE customer = @customer ");

                cmd.Parameters.AddWithValue("@customer", customer);

                if ((chamadoDTO.createDate != DateTime.MinValue && chamadoDTO.closeDate != DateTime.MinValue) || chamadoDTO.callID != 0)
                {
                    stb.Append("AND ");

                    if (chamadoDTO.callID > 0)
                    {
                        stb.Append("callID = @CallId ");
                        cmd.Parameters.AddWithValue("@CallId", chamadoDTO.callID);

                        if (chamadoDTO.createDate != DateTime.MinValue && chamadoDTO.closeDate != DateTime.MinValue)
                            stb.Append("AND ");
                    }

                    if (chamadoDTO.createDate != DateTime.MinValue && chamadoDTO.closeDate != DateTime.MinValue)
                    {
                        stb.Append("(createDate BETWEEN @DataInicial AND @DataFinal) ");
                        cmd.Parameters.AddWithValue("@DataInicial", chamadoDTO.createDate.ToString("yyyy-MM-dd") + " 00:00:00");
                        cmd.Parameters.AddWithValue("@DataFinal", chamadoDTO.closeDate.ToString("yyyy-MM-dd") + " 23:59:59");
                    }
                }

                stb.Append("ORDER BY callID DESC ");

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ChamadoServicoDTO chamado = new ChamadoServicoDTO();
                            chamado.callID = (rdr["callID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["callID"]));
                            chamado.customer = rdr["customer"].ToString();
                            chamado.closeDate = (rdr["closeDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["closeDate"]));
                            chamado.createDate = (rdr["createDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["createDate"]));
                            chamado.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"]));
                            chamado.itemCode = rdr["itemCode"].ToString();

                            chamado.U_DataFalha = (rdr["U_DataFalha"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DataFalha"]));
                            chamado.U_Chassi = rdr["U_Chassi"].ToString();
                            chamado.U_Modelo = rdr["U_Modelo"].ToString();
                            chamado.U_KmFal = (rdr["U_KmFal"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.U_Placa = rdr["U_Placa"].ToString();
                            chamado.U_NumMoto = rdr["U_NumMoto"].ToString();
                            chamado.U_ModelMoto = rdr["U_ModelMoto"].ToString();
                            chamado.U_DescFal = rdr["U_DescFal"].ToString();
                            chamado.U_CausaFal = rdr["U_CausaFal"].ToString();
                            chamado.U_CorrecaoFal = rdr["U_CorrecaoFal"].ToString();
                            chamado.U_ObsGerais = rdr["U_ObsGerais"].ToString();
                            chamado.U_OrdemServ = rdr["U_OrdemServ"].ToString();
                            chamado.U_NomResp = rdr["U_NomResp"].ToString();
                            chamado.U_Funcao = rdr["U_Funcao"].ToString();
                            chamado.U_DtVenda = (rdr["U_DtVenda"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtVenda"]));
                            chamado.U_DtAbertFal = (rdr["U_DtAbertFal"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_DtAbertFal"]));
                            chamado.U_NomCli = rdr["U_NomCli"].ToString();
                            chamado.U_KmAt = (rdr["U_KmAt"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_KmFal"]));
                            chamado.AtcEntry = rdr["AtcEntry"].ToString();
                            chamado.U_Status = rdr["U_Status"].ToString();
                            chamado.U_TpGarant = rdr["U_TpGarant"].ToString();
                            chamado.U_SubTipoGarant = rdr["U_SubTipoGarant"].ToString();

                            listChamados.Add(chamado);
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

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
            return listChamados;
        }
    }
}
