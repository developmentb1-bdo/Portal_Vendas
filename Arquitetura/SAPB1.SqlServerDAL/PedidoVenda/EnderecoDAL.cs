using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoVenda;
using SAPB1.DTO.PedidoVenda;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.PedidoVenda
{
    public class EnderecoDAL:IEndereco
    {
        public EnderecoDTO RetonarEndereco(PedidoVendaDTO pedidoVendaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT ""DocEntry"", ""AddrTypeS"", ""ZipCodeS"", ""StreetS"", ""StreetNoS"", ""BuildingS"", ""BlockS"", ""CityS"", ""StateS"", ""CountyS"", ""CountryS"", ""Address2S"", ""Address3S"", ""GlbLocNumS"", ""AddrTypeB"", ""ZipCodeB"", ""StreetB"", ""StreetNoB"", ""BuildingB"", ""BlockB"", ""CityB"", ""StateB"", ""CountyB"", ""CountryB"", ""Address2B"", ""Address3B"", ""GlbLocNumB"", ""State"", ""County"", ""Incoterms"", ""Vehicle"", ""VidState"", ""NfRef"", ""Carrier"", ""QoP"", ""PackDesc"", ""Brand"", ""NoSU"" FROM RDR12 WHERE ""DocEntry"" = '{pedidoVendaDTO.DocEntry}'";
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();

                    return PopularDadosHana(query, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                string queryPadrao = "SELECT DocEntry, AddrTypeS, ZipCodeS, StreetS, StreetNoS, BuildingS, BlockS, CityS, StateS, CountyS, CountryS, Address2S, Address3S, GlbLocNumS, AddrTypeB, ZipCodeB, StreetB, StreetNoB, BuildingB, BlockB, CityB, StateB, CountyB, CountryB, Address2B, Address3B, GlbLocNumB, State, County, Incoterms, Vehicle, VidState, NfRef, Carrier, QoP, PackDesc, Brand, NoSu FROM RDR12 ";

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);
                stb.Append("WHERE DocEntry = @DocEntry");

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@DocEntry", pedidoVendaDTO.DocNum);

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

        private EnderecoDTO PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            EnderecoDTO enderecoDTO = new EnderecoDTO();

            if (rdr.HasRows)
            {
                while(rdr.Read())
                {
                    enderecoDTO.Address2S = rdr["Address2S"].ToString();
                    enderecoDTO.Address3S = rdr["Address3S"].ToString();
                    enderecoDTO.AddrTypeS = rdr["AddrTypeS"].ToString();
                    enderecoDTO.BlockS = rdr["BlockS"].ToString();
                    enderecoDTO.BuildingS = rdr["BuildingS"].ToString();
                    enderecoDTO.CityS = rdr["CityS"].ToString();
                    enderecoDTO.CountryS = rdr["CountryS"].ToString();
                    enderecoDTO.CountyS = rdr["CountyS"].ToString();
                    enderecoDTO.GlbLocNumS = rdr["GlbLocNumS"].ToString();
                    enderecoDTO.StateS = rdr["StateS"].ToString();
                    enderecoDTO.StreetNoS = rdr["StreetNoS"].ToString();
                    enderecoDTO.StreetS = rdr["StreetS"].ToString();
                    enderecoDTO.ZipCodeS = rdr["ZipCodeS"].ToString();

                    enderecoDTO.Address2B = rdr["Address2S"].ToString();
                    enderecoDTO.Address3B = rdr["Address3S"].ToString();
                    enderecoDTO.AddrTypeB = rdr["AddrTypeS"].ToString();
                    enderecoDTO.BlockB = rdr["BlockS"].ToString();
                    enderecoDTO.BuildingB = rdr["BuildingS"].ToString();
                    enderecoDTO.CityB = rdr["CityS"].ToString();
                    enderecoDTO.CountryB = rdr["CountryS"].ToString();
                    enderecoDTO.CountyB = rdr["CountyS"].ToString();
                    enderecoDTO.GlbLocNumB = rdr["GlbLocNumS"].ToString();
                    enderecoDTO.StateB = rdr["StateS"].ToString();
                    enderecoDTO.StreetNoB = rdr["StreetNoS"].ToString();
                    enderecoDTO.StreetB = rdr["StreetS"].ToString();
                    enderecoDTO.ZipCodeB = rdr["ZipCodeS"].ToString();

                    enderecoDTO.State = rdr["State"].ToString();
                    enderecoDTO.County = rdr["County"].ToString();
                    enderecoDTO.Incoterms = rdr["Incoterms"].ToString();
                    enderecoDTO.Vehicle = rdr["Vehicle"].ToString();
                    enderecoDTO.VidState = rdr["VidState"].ToString();
                    enderecoDTO.NfRef = rdr["NfRef"].ToString();
                    enderecoDTO.Carrier = rdr["Carrier"].ToString();
                    enderecoDTO.QoP = rdr["QoP"].ToString();
                    enderecoDTO.PackDesc = rdr["PackDesc"].ToString();
                    enderecoDTO.Brand = rdr["Brand"].ToString();
                    enderecoDTO.NoSu = rdr["NoSu"].ToString();
                }
            }

            return enderecoDTO;
        }

        private EnderecoDTO PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            EnderecoDTO enderecoDTO = new EnderecoDTO();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rdr in dt.Rows)
                {
                    enderecoDTO.Address2S = rdr["Address2S"].ToString();
                    enderecoDTO.Address3S = rdr["Address3S"].ToString();
                    enderecoDTO.AddrTypeS = rdr["AddrTypeS"].ToString();
                    enderecoDTO.BlockS = rdr["BlockS"].ToString();
                    enderecoDTO.BuildingS = rdr["BuildingS"].ToString();
                    enderecoDTO.CityS = rdr["CityS"].ToString();
                    enderecoDTO.CountryS = rdr["CountryS"].ToString();
                    enderecoDTO.CountyS = rdr["CountyS"].ToString();
                    enderecoDTO.GlbLocNumS = rdr["GlbLocNumS"].ToString();
                    enderecoDTO.StateS = rdr["StateS"].ToString();
                    enderecoDTO.StreetNoS = rdr["StreetNoS"].ToString();
                    enderecoDTO.StreetS = rdr["StreetS"].ToString();
                    enderecoDTO.ZipCodeS = rdr["ZipCodeS"].ToString();
                    enderecoDTO.Address2B = rdr["Address2S"].ToString();
                    enderecoDTO.Address3B = rdr["Address3S"].ToString();
                    enderecoDTO.AddrTypeB = rdr["AddrTypeS"].ToString();
                    enderecoDTO.BlockB = rdr["BlockS"].ToString();
                    enderecoDTO.BuildingB = rdr["BuildingS"].ToString();
                    enderecoDTO.CityB = rdr["CityS"].ToString();
                    enderecoDTO.CountryB = rdr["CountryS"].ToString();
                    enderecoDTO.CountyB = rdr["CountyS"].ToString();
                    enderecoDTO.GlbLocNumB = rdr["GlbLocNumS"].ToString();
                    enderecoDTO.StateB = rdr["StateS"].ToString();
                    enderecoDTO.StreetNoB = rdr["StreetNoS"].ToString();
                    enderecoDTO.StreetB = rdr["StreetS"].ToString();
                    enderecoDTO.ZipCodeB = rdr["ZipCodeS"].ToString();
                    enderecoDTO.State = rdr["State"].ToString();
                    enderecoDTO.County = rdr["County"].ToString();
                    enderecoDTO.Incoterms = rdr["Incoterms"].ToString();
                    enderecoDTO.Vehicle = rdr["Vehicle"].ToString();
                    enderecoDTO.VidState = rdr["VidState"].ToString();
                    enderecoDTO.NfRef = rdr["NfRef"].ToString();
                    enderecoDTO.Carrier = rdr["Carrier"].ToString();
                    enderecoDTO.QoP = rdr["QoP"].ToString();
                    enderecoDTO.PackDesc = rdr["PackDesc"].ToString();
                    enderecoDTO.Brand = rdr["Brand"].ToString();
                    enderecoDTO.NoSu = rdr["NoSu"].ToString();
                }
            }

            return enderecoDTO;
        }
    }
}
