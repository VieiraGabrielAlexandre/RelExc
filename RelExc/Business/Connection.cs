using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

namespace CadMaterial.Business
{
    /*
     * 
     * Essa classe retorna um dataTable com a tabela selecionada;
     * 
     * Instruções:
     * 1 -> Gerar uma nova instancia dessa classe
     * 2 -> Utilizar alguma das funções como loadDataTable para retornar uma DataTable com um select
     * 3 -> insertTable ou updateTable recebem uma tabela e a query, ambos do tipo BOOL
     * 
     * 
     */
    class Connection
    {
        private String connString;
        private String Sql;

        public DataTable dataTable;

        public Connection() // Parametro opcional - permitir que selecione o tipo do SELECT
        {
            this.connString = "Driver={IBM DB2 ODBC DRIVER};hostname=BRSPVWDB1;" +
                "port=50000;database=PW_DATA;pwd=Password1;uid=userid;";
        }

        public bool updateTable(String dbTable, String sqlSet, String sqlWhere)
        {
            String updateClause = "UPDATE " + dbTable + " " + sqlSet + " " + sqlWhere;

                using (OdbcConnection connection = new OdbcConnection(this.connString))
                {
                    using (OdbcCommand cmd = new OdbcCommand(updateClause, connection))
                    {
                        try
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (OdbcException od)
                        {
                            MessageBox.Show("Erro! -> " + " Mensagem de erro: " + od.Message + " o programa será encerrado.");
                            return false;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
                return true;
        }

        public bool insertTable(String dbTable, String sqlValues)
        {
            String insertClause = "INSERT INTO " + dbTable + " " + sqlValues + ")";
            
            using (OdbcConnection connection = new OdbcConnection(this.connString))
            {
                using (OdbcCommand cmd = new OdbcCommand(insertClause, connection))
                {
                    try
                    {
                        connection.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result < 0)
                            return false;
                    }
                    catch (OdbcException od)
                    {
                        MessageBox.Show("Erro! -> " + " Mensagem de erro: " + od.Message + " o programa será encerrado.");
                        Application.Exit();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            } 
            return true;
        }

        public DataTable loadDataTable(String tableName, String inputMethod = "*", String whereClause = " ")
        {
         Sql = "SELECT " + inputMethod + "FROM " + tableName + whereClause + ";";

            using (OdbcConnection connection = new OdbcConnection(this.connString))
            {
                using (OdbcCommand odbcCommand = new OdbcCommand(Sql, connection))
                {
                    try
                    {
                        connection.Open();

                        using (OdbcDataReader dataReader = odbcCommand.ExecuteReader())
                        {
                            dataTable = new DataTable();
                            dataTable.Load(dataReader);
                            dataReader.Close();
                        }
                    }
                    catch (OdbcException od)
                    {
                        MessageBox.Show("Erro! -> " + " Mensagem de erro: " + od.Message + " o programa será encerrado.");
                        Application.Exit();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return dataTable;
                }
        }
       
    }
}
