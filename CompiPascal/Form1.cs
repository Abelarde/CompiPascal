
using CompiPascal.interprete.analizador;
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

namespace CompiPascal
{
    public partial class Form1 : Form
    {
        SintacticoInterprete sintacticoInter;

        public Form1()
        {
            InitializeComponent();
            sintacticoInter = new SintacticoInterprete();
        }

        private void Translate_Click(object sender, EventArgs e)
        {
            sintacticoInter.analizar(EditorOutput());
            ConsoleInput(sintacticoInter.Message());
            ErrorTable();       

        }

        private void Run_Click(object sender, EventArgs e)
        {

        }

        private void Load_Click(object sender, EventArgs e)
        {
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Read the contents of the file into a stream
                var fileStream = openFileDialog1.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    EditorInput(reader.ReadToEnd());
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, EditorOutput());
            }

        }

        private void ReportAST_Click(object sender, EventArgs e)
        {
            sintacticoInter.graficar(EditorOutput());
            ConsoleInput(sintacticoInter.Message());
            ErrorTable();

        }

        private void ReportTS_Click(object sender, EventArgs e)
        {

        }
        private void ErrorsTranslate_Click(object sender, EventArgs e)
        {

        }

        private void ErrorsRun_Click(object sender, EventArgs e)
        {

        }

        private void EditorInput(string msn) => textBox1.Text = msn;

        private string EditorOutput() => textBox1.Text;

        private void ConsoleInput(string msn) => textBox2.Text = msn;

        private void ClearConsole_Click(object sender, EventArgs e)
        {
            sintacticoInter.ClearMessage();
            ConsoleInput(string.Empty);
        }

        private void ErrorTable()
        {
            DataTable ss = new DataTable();
            ss.Columns.Add("Tipo");
            ss.Columns.Add("Descripcion");
            ss.Columns.Add("Linea");
            ss.Columns.Add("Columna");
            ss.Columns.Add("Extra");

            DataRow row;

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            for (int i = 0; i < sintacticoInter.Errores().GetLength(0); i++)
            {
                row = ss.NewRow();
                row["Tipo"] = sintacticoInter.Errores()[i, 0];
                row["Descripcion"] = sintacticoInter.Errores()[i, 1];
                row["Linea"] = sintacticoInter.Errores()[i, 2];
                row["Columna"] = sintacticoInter.Errores()[i, 3];
                row["Extra"] = sintacticoInter.Errores()[i, 4];
                ss.Rows.Add(row);

            }


            foreach (DataRow drow in ss.Rows)
            {
                int num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = drow["Tipo"].ToString();
                dataGridView1.Rows[num].Cells[1].Value = drow["Descripcion"].ToString();
                dataGridView1.Rows[num].Cells[2].Value = drow["Linea"].ToString();
                dataGridView1.Rows[num].Cells[3].Value = drow["Columna"].ToString();
                dataGridView1.Rows[num].Cells[4].Value = drow["Extra"].ToString();
            }

            sintacticoInter.ClearErrores();

        }
    }
}
