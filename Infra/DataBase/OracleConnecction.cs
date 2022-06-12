
using Infra.Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

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
            #region ExemploGeral
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
            #endregion
        }

        public static List<BradescoBoleto> GetFINTitulo(Empresa empresa)
        {
            var listTitulos = new List<BradescoBoleto>();
            using (OracleConnection con = OracleDB.con)
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        if(empresa == null)
                        {
                            cmd.CommandText = "SELECT " +
                              "FAT.CGCCPF AS FAT_CGCCPF, REV.CNPJ AS REV_CNPJ, FI.CARTEIRA AS FI_CARTEIRA, " +
                              "FI.AGENCIA_FAVORECIDO AS FI_AGENCIA_FAVORECIDO, FI.NOSSONUMERO AS FI_NOSSONUMERO, " +
                              "FI.CLIENTE AS FI_CLIENTE, FI.DTA_EMISSAO AS FI_DTA_EMISSAO, FI.DTA_VENCIMENTO AS FI_DTA_VENCIMENTO, " +
                              "FI.VAL_TITULO AS FI_VAL_TITULO, FAT.NOME AS FAT_NOME, FAT.LOGRADOURO_COBRANCA AS FAT_LOGRADOURO_COBRANCA, " +
                              "FAT.NUMERO_COBRANCA AS FAT_NUMERO_COBRANCA, FAT.CEP_COBRANCA AS FAT_CEP_COBRANCA, FAT.BAIRRO_COBRANCA AS FAT_BAIRRO_COBRANCA, " +
                              "FAT.MUNICIPIO_COBRANCA AS FAT_MUNICIPIO_COBRANCA, FAT.UF_COBRANCA AS FAT_UF_COBRANCA, PJ.CGC AS PJ_CGC, PF.CPF AS PF_CPF " +
                                  "FROM FIN_TITULO FI " +
                                  "INNER JOIN FAT_CLIENTE FAT ON FAT.CLIENTE = FI.CLIENTE " +
                                  "INNER JOIN GER_REVENDA REV ON REV.REVENDA = FI.REVENDA " +
                                  "LEFT JOIN FAT_PESSOA_FISICA PF ON PF.CLIENTE = FI.CLIENTE " +
                                  "LEFT JOIN FAT_PESSOA_JURIDICA PJ ON PJ.CLIENTE = FI.CLIENTE " +
                                  "WHERE FI.EMPRESA = 1 AND FI.REVENDA = 1 AND BANCO = 104 " +
                                  "AND ROWNUM = 1";
                        }
                        else
                        {
                            cmd.CommandText = "SELECT " +
                                "FAT.CGCCPF AS FAT_CGCCPF, REV.CNPJ AS REV_CNPJ, FI.CARTEIRA AS FI_CARTEIRA, " +
                                "FI.AGENCIA_FAVORECIDO AS FI_AGENCIA_FAVORECIDO, FI.NOSSONUMERO AS FI_NOSSONUMERO, " +
                                "FI.CLIENTE AS FI_CLIENTE, FI.DTA_EMISSAO AS FI_DTA_EMISSAO, FI.DTA_VENCIMENTO AS FI_DTA_VENCIMENTO, " +
                                "FI.VAL_TITULO AS FI_VAL_TITULO, FAT.NOME AS FAT_NOME, FAT.LOGRADOURO_COBRANCA AS FAT_LOGRADOURO_COBRANCA, " +
                                "FAT.NUMERO_COBRANCA AS FAT_NUMERO_COBRANCA, FAT.CEP_COBRANCA AS FAT_CEP_COBRANCA, FAT.BAIRRO_COBRANCA AS FAT_BAIRRO_COBRANCA, " +
                                "FAT.MUNICIPIO_COBRANCA AS FAT_MUNICIPIO_COBRANCA, FAT.UF_COBRANCA AS FAT_UF_COBRANCA, PJ.CGC AS PJ_CGC, PF.CPF AS PF_CPF " +
                                    "FROM FIN_TITULO FI " +
                                    "INNER JOIN FAT_CLIENTE FAT ON FAT.CLIENTE = FI.CLIENTE " +
                                    "INNER JOIN GER_REVENDA REV ON REV.REVENDA = FI.REVENDA " +
                                    "LEFT JOIN FAT_PESSOA_FISICA PF ON PF.CLIENTE = FI.CLIENTE " +
                                    "LEFT JOIN FAT_PESSOA_JURIDICA PJ ON PJ.CLIENTE = FI.CLIENTE " +
                                    // "WHERE FI.EMPRESA = 1 AND FI.REVENDA = 1 AND BANCO = 104 " +
                                    // "AND ´ROWNUM = 1";

                                    // FILTROS
                                    "WHERE((FI.EMPRESA = '" + empresa.Nome + "'" + //CODIGO EMPRESA
                                    "AND coalesce(FI.REVENDA_COMPROMISSO, FI.REVENDA) = '" + empresa.Revenda + "'" + // CODIGO REVENDA
                                    "AND FI.BANCO = '" + empresa.Banco + "'" + ")) AND TIPO = 'CR' " +
                                    "AND ((STATUS = 'EM' " +
                                    "AND ((ENVIADO IS NULL OR ENVIADO = 0)) OR(STATUS = 'PT' " +
                                    "AND (ENVIADO = 1 OR ENVIADO = 3) AND INSTRUCAO_ENVIO = 2) )) " +
                                    "AND dta_emissao between TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','dd/mm/yyyy') " +
                                    "AND TO_DATE('" + DateTime.Now.AddDays(1).ToString("dd/MM/yyyy") + "','dd/mm/yyyy')" +
                                    "AND ((EMPRESA = '" + empresa.Nome + "'" + " and REVENDA = '" + empresa.Revenda + "'" + " and DEPARTAMENTO = '" + empresa.Departamento + "'" + ")) " +
                                    "AND FI.ORIGEM IN('" + empresa.Origem + "'" + ") " +
                                    "AND not exists(select NFE_SITUACAO from FAT_MOVIMENTO_CAPA where EMPRESA = FI.EMPRESA and REVENDA = FI.REVENDA and OPERACAO = FI.OPERACAO and STATUS <> 'C' and TIPO_NF = 'E' and NFE_SITUACAO <> 'A') ";
                        }
                    

                        OracleDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            try
                            {
                                var titulo = new BradescoBoleto();
                                titulo.nuCPFCNPJ = Convert.ToInt64(dataReader["FAT_CGCCPF"].ToString().Substring(0,9));
                                titulo.filialCPFCNPJ = Convert.ToInt64(dataReader["FAT_CGCCPF"].ToString().Substring(9, 3)); // **
                                titulo.ctrlCPFCNPJ = Convert.ToInt64(dataReader["REV_CNPJ"].ToString().Substring(13)); // 2 ultimos digitos
                                titulo.idProduto = Convert.ToInt64(dataReader["FI_CARTEIRA"] != DBNull.Value ? dataReader["FI_CARTEIRA"] : 0);
                                titulo.nuNegociacao = Convert.ToInt64(dataReader["FI_AGENCIA_FAVORECIDO"] != DBNull.Value ? dataReader["FI_AGENCIA_FAVORECIDO"] : 0);
                                titulo.nuTitulo = Convert.ToInt64(dataReader["FI_NOSSONUMERO"] != DBNull.Value ? dataReader["FI_NOSSONUMERO"] : 0);
                                titulo.nuCliente = dataReader["FI_CLIENTE"].ToString();
                                titulo.dtEmissaoTitulo = Convert.ToDateTime(dataReader["FI_DTA_EMISSAO"]).ToString("dd.MM.yyyy");
                                titulo.dtVencimentoTitulo = Convert.ToDateTime(dataReader["FI_DTA_VENCIMENTO"]).ToString("dd.MM.yyyy");
                                titulo.vlNominalTitulo = Convert.ToDecimal(dataReader["FI_VAL_TITULO"] != DBNull.Value ? dataReader["FI_VAL_TITULO"] : 0);
                                titulo.cdEspecieTitulo = 32; // BOLETO --> (BDP -- Boleto de Proposta) Docu bradesco)
                                titulo.nomePagador = dataReader["FAT_NOME"].ToString();
                                titulo.logradouroPagador = dataReader["FAT_LOGRADOURO_COBRANCA"].ToString().Length > 0 ? dataReader["FAT_LOGRADOURO_COBRANCA"].ToString() : "0";
                                titulo.nuLogradouroPagador = dataReader["FAT_NUMERO_COBRANCA"].ToString().Length > 0 ? dataReader["FAT_NUMERO_COBRANCA"].ToString() : "0";
                                titulo.cepPagador = Convert.ToInt64(dataReader["FAT_CEP_COBRANCA"] != DBNull.Value ? dataReader["FAT_CEP_COBRANCA"] : 0);
                                titulo.complementoCepPagador = 0;
                                titulo.bairroPagador = dataReader["FAT_BAIRRO_COBRANCA"].ToString().Length > 0 ? dataReader["FAT_BAIRRO_COBRANCA"].ToString() : "0";
                                titulo.municipioPagador = dataReader["FAT_MUNICIPIO_COBRANCA"].ToString().Length > 0 ? dataReader["FAT_MUNICIPIO_COBRANCA"].ToString() : "0";
                                titulo.ufPagador = dataReader["FAT_UF_COBRANCA"].ToString().Length > 0 ? dataReader["FAT_UF_COBRANCA"].ToString() : "0";
                                titulo.nuCpfcnpjPagador = Convert.ToInt64(dataReader["PJ_CGC"] ?? dataReader["PF_CPF"]);
                                titulo.cdIndCpfcnpjPagador = titulo.nuCpfcnpjPagador.ToString().Length == 14 ? 2 : 1;
                                listTitulos.Add(titulo);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Oracle Titulo Failed: " + ex.Message);
                            }
                        }
                        dataReader.Close();
                        dataReader.Dispose();

                        return listTitulos;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Oracle Failed: " + ex.Message);
                        return new List<BradescoBoleto>();
                    }
                }
            }
        }

        public static void UpdateTitulo(int retorno, long nossoNumero)
        {
            var listTitulos = new List<BradescoBoleto>();
            using (OracleConnection con = OracleDB.con)
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "UPDATE FIN_TITULO" +
                            "SET ENVIADO  = '" + retorno + "'," +
                            "WHERE NOSSONUMERO = '" + nossoNumero + "';";

                        OracleDataReader dataReader = cmd.ExecuteReader();
                        dataReader.Close();
                        dataReader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oracle Failed: " + ex.Message);
                    }
                }
            }
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
