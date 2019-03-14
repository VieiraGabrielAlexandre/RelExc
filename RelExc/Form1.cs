using CadMaterial.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelExc
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        Connection conexao = new Connection();
        DataTable dtTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dtTable = conexao.loadDataTable("BAX_EXCEPTION_LOG ", "*", " ORDER BY BATCH_ID, ENTRY_TIMESTAMP ASC");
            dtGrid.DataSource = dtTable;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Utils pesquisar = new Utils();
            dtTable = pesquisar.filtrarValores(dtTable, " BATCH_ID = '" + txtLote.Text + "'");
            dtGrid.DataSource = dtTable;
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtLote_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            var lines = new List<string>();
            string[] columnNames = dtTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
            var header = string.Join(",", columnNames); lines.Add(header); var valueLines = dtTable.AsEnumerable().Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines); File.WriteAllLines("Relatorio.txt", lines);
            MessageBox.Show("Foi gerado um arquivo chamado Relatorio.txt. Abra este arquivo com o Excel e marque o delimitador como ','");
        }
    }
}
