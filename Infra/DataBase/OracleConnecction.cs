
using Oracle.ManagedDataAccess.Client;
using System;

namespace Infra.DataBase
{

    public class OracleDB
    {
        //Exemplo
        //private string conString = "DATA SOURCE=10.199.15.4/orcl;USER ID=ANF_USER;PASSWORD=ANFUSER;VALIDATE CONNECTION=TRUE;STATEMENT CACHE SIZE=150;";

        //SERVOPA DB
        //private string conString = "DATA SOURCE=10.100.1.205:1525/APOLLO_TESTE;USER ID=Bradesco;PASSWORD=BradescoAbril2022#;VALIDATE CONNECTION=TRUE;STATEMENT CACHE SIZE=150;";
        private string conString = "DATA SOURCE=10.100.1.205:1525/Apollo_TESTE;USER ID=Bradesco;PASSWORD=BradescoAbril2022#;VALIDATE CONNECTION=TRUE;STATEMENT CACHE SIZE=150;";
        //private string conString = "DATA SOURCE=10.100.1.205/APOLLO_TESTE;USER ID=Bradesco;PASSWORD=BradescoAbril2022#;VALIDATE CONNECTION=TRUE;STATEMENT CACHE SIZE=150;";
        public static OracleConnection con;

        public OracleDB()
        {
            OracleDB.con = new OracleConnection(this.conString);
            //using (OracleConnection con = new OracleConnection(this.conString))
            //{
            //    using (OracleCommand cmd = con.CreateCommand())
            //    {
            //        try
            //        {
            //            con.Open();
            //            cmd.BindByName = true;
            //        }
            //        catch(Exception ex)
            //        {
            //            Console.WriteLine("Oracle Failed: " + ex.Message);

            //        }
            //    }
            //}
        }

        public static string GetFINTitulo()
        {
            using (OracleConnection con = OracleDB.con)
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT* FROM FIN_TITULO FIS " +
                            "WHERE((FI.EMPRESA = 1 " +
                            "AND coalesce(FI.REVENDA_COMPROMISSO, FI.REVENDA) = 1 " +
                            "AND FI.BANCO = 104)) AND TIPO = 'CR' " +
                            "AND ((STATUS = 'EM' " +
                            "AND ((ENVIADO IS NULL OR ENVIADO = 0)) OR(STATUS = 'PT' " +
                            "AND (ENVIADO = 1 OR ENVIADO = 3) AND INSTRUCAO_ENVIO = 2) )) " +
                            "AND dta_emissao between TO_DATE('17/02/2022','dd/mm/yyyy') " +
                            "AND TO_DATE('18/02/2022','dd/mm/yyyy')" +
                            "AND ((EMPRESA = 1 and REVENDA = 1 and DEPARTAMENTO = 15)) " +
                            "AND FI.ORIGEM IN(1104) " +
                            "AND not exists(select NFE_SITUACAO from FAT_MOVIMENTO_CAPA where EMPRESA = FI.EMPRESA and REVENDA = FI.REVENDA and OPERACAO = FI.OPERACAO and STATUS <> 'C' and TIPO_NF = 'E' and NFE_SITUACAO <> 'A') ";

                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string empresa = reader["EMPRESA"].ToString();
                            string revenda = reader["REVENDA"].ToString();
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Oracle Failed: " + ex.Message);

                    }
                }
            }

            #region TESTE ORACLE
            //OracleCommand command = new OracleCommand();
            //command.CommandText = "SELECT * FROM FIN_TITULO FI" +
            //        "WHERE((" +
            //            "--FI.EMPRESA = X and coalesce(FI.REVENDA_COMPROMISSO, FI.REVENDA) = X and" +
            //              "FI.BANCO = 104)) AND TIPO = 'CR'" +
            //              "AND((STATUS = 'EM'" +
            //              "AND((ENVIADO IS NULL OR ENVIADO = 0)) OR(STATUS = 'PT'" +
            //             "AND(ENVIADO = 1 OR ENVIADO = 3) AND INSTRUCAO_ENVIO = 2) ))" +
            //                "--and dta_emissao between TO_DATE('xx/xx/xxxx', 'dd/mm/yyyy')" +
            //             "--and TO_DATE('xx/xx/xxxx','dd/mm/yyyy')" +
            //                "--and((EMPRESA = X and REVENDA = X and DEPARTAMENTO = X))" +
            //                "and FI.ORIGEM IN( 1104 )" +
            //                "and not exists(select NFE_SITUACAO from FAT_MOVIMENTO_CAPA" +
            //              "where EMPRESA = FI.EMPRESA and REVENDA = FI.REVENDA and OPERACAO = FI.OPERACAO and STATUS <> 'C' and TIPO_NF = 'E' and NFE_SITUACAO <> 'A');" +
            //                      "View criada" +
            //              "SYS @APOLLO SQL > select count(1) from gruposervopa.coaf_boleto;" +
            //               "COUNT(1)";

            ////command.Parameters.Add(":username", OracleDbType.NVarchar2).Value = username;
            //command.Connection = OracleDB.con;
            //OracleDB.con.Open();

            //while (reader.Read())
            //{
            //    string password = reader["password"].ToString();
            //    string customerId = reader["customerId"].ToString();
            //    string securityQuestion = reader["securityQuestion"].ToString();
            //    string securityAnswer = reader["securityAnswer"].ToString();
            //    string email = reader["email"].ToString();
            //    // user = new User(username, password, customerId, securityQuestion, securityAnswer, email);
            //}
            #endregion

            return string.Empty;
        }
    }
}
