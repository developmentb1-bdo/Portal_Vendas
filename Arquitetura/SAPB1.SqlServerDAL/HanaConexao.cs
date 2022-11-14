using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Sap.Data.Hana;

namespace SAPB1.SqlServerDAL
{
    internal class HanaConexao
    {      

        internal HanaConexao()
        {

        }

        private HanaConnection _conn = new HanaConnection(ConfigurationManager.ConnectionStrings["HanaConexao"].ToString());

        internal HanaConnection Conexao
        {
            get
            {
                return _conn;
            }
        }
        
        public object ExecuteScalar(string querySql)
        {
            try
            {
                Connection();
                var cmd = new HanaCommand(querySql, _conn);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        internal DataTable ExecuteDataTable(string querySql)
        {
            try
            {
                var dt = new DataTable("Table1");
                var da = new HanaDataAdapter(querySql, _conn);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void Connection()
        {
            try
            {
                if (Conexao.State == ConnectionState.Closed)
                    Conexao.Open();
                else
                    Conexao.Close();
            }
            catch (HanaException ex)
            {
                throw ex;
            }
        }

       
        public void Dispose()
        {
            try
            {
                if (Conexao == null)
                    return;

                if (Conexao.State == ConnectionState.Open)
                    Conexao.Close();

                Conexao.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
