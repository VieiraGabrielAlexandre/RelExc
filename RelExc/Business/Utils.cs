using System;
using System.Data;
/*
 * FUNÇÕES UTEIS PARA A CONEXÃO E OBTENÇÃO DOS VALORES NO DATATABLE
 * 
 */
namespace CadMaterial.Business
{
    class Utils
    {
        /*
         * Retorna um datatable de acordo com a tabela, where e colunas
         */
        public DataTable carregarTabela(String tableDb, string columnsDb, string whereDb)
        {
            Connection con;
            con = new Connection();

            return con.loadDataTable(tableDb, columnsDb, whereDb); // Conecta e retorna o dataTable para a instancia atual
        }

        /*
         * Retorna um novo datatable com o where selecionado
         */
		
		public DataTable filtrarValores(DataTable dbTable, String whereClause)
		{
            DataTable dbFiltrado = null;

            var result = dbTable.Select(whereClause);

            if (result.Length > 0) // Copiar o resultado, caso exista ocorrencias
                dbFiltrado = result.CopyToDataTable();

            return dbFiltrado;
        }

        /*
         * Seleciona as colunas e retorna para um novo datatable
         * exemplo: selectColumn(coCheckListSp, new [] {"CHECKLIST_ID"}
         */
        public DataTable selectColumn(DataTable dbTable, String[] selectedColumns)
        {
            DataTable dt = new DataView(dbTable).ToTable(false, selectedColumns);

            return dt;
        }

        /* 
         * Filtra uma tabela de acordo com o primaryKey
         * exemplo: selecionarLinhaDB(coCheckListSp, CHK_LINE_SEQUENCE, 1, INSTRUCTION
         * Retorna um STRING da coluna INSTRUCTION de CHK_LINE_SEQUENCE onde for 1 
         */
        public String selecionarLinhaDB(DataTable dbTable, String primaryKey, int primaryKeyIndex,
            String resultColumn)
        {
            DataTable chkLineSequence = selectColumn(dbTable, new[] { resultColumn });

            DataRow[] dr = dbTable.Select(primaryKey + " = '" + primaryKeyIndex + "'");

            return dr[0][resultColumn].ToString();
        }

        // Retorna uma linha do datatable, e não direto do DB como na função anterior
        public String selecionarLinhaDT(DataTable dbTable, int row, String columnName)
        {
            return dbTable.Rows[row][columnName].ToString().Trim();
        }
    }
}
