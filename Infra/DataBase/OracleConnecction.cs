
using Oracle.ManagedDataAccess.Client;
using System;

namespace Infra.DataBase
{
    public class OracleDB
    {
        //Exemplo
        //private string conString = "DATA SOURCE=10.199.15.4/orcl;USER ID=ANF_USER;PASSWORD=ANFUSER;VALIDATE CONNECTION=TRUE;STATEMENT CACHE SIZE=150;";
        private string conString = "DATA SOURCE=10.100.1.205:1525/APOLLO_TESTE;USER ID=Bradesco;PASSWORD=il2022#;VALIDATE CONNECTION=TRUE;STATEMENT CACHE SIZE=150;";

        public OracleDB()
        {
            using (OracleConnection con = new OracleConnection(this.conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Oracle Failed: " + ex.Message);

                    }
                }
            }
        }
    }
}
