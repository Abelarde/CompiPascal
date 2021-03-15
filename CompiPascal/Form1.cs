
using CompiPascal.interprete.analizador;
using CompiPascal.interprete.util;
using CompiPascal.traductor.analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompiPascal
{
    public partial class Form1 : Form
    {
        SintacticoInterprete sintacticoInter;
        SintacticoTraductor sintacticoTraductor;

        public Form1()
        {
            InitializeComponent();
            sintacticoInter = new SintacticoInterprete();
            sintacticoTraductor = new SintacticoTraductor();
        }

        private void Translate_Click(object sender, EventArgs e)
        {
            sintacticoTraductor.analizar(EditorOutput(), this);
            ConsoleInput(sintacticoTraductor.Message());
            ErrorTableTraductor();
            foreach (string number in ErrorPascal.cola)
            {
                ConsoleInput(number+ Environment.NewLine);
            }
            ErrorPascal.cola.Clear();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            sintacticoInter.analizar(EditorOutput(), this);
            ConsoleInput(sintacticoInter.Message());
            ErrorTable();
            foreach (string number in ErrorPascal.cola)
            {
                ConsoleInput(number + Environment.NewLine);
            }
            ErrorPascal.cola.Clear();
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


        private void EditorInput(string msn) => textBox1.Text = msn;

        private string EditorOutput() => textBox1.Text;

        private void ConsoleInput(string msn) => textBox2.Text += msn;

        private void ClearConsole_Click(object sender, EventArgs e)
        {
            sintacticoInter.ClearMessage();
            sintacticoTraductor.ClearMessage();
            textBox2.Text = string.Empty;
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
        private void ErrorTableTraductor()
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

            for (int i = 0; i < sintacticoTraductor.Errores().GetLength(0); i++)
            {
                row = ss.NewRow();
                row["Tipo"] = sintacticoTraductor.Errores()[i, 0];
                row["Descripcion"] = sintacticoTraductor.Errores()[i, 1];
                row["Linea"] = sintacticoTraductor.Errores()[i, 2];
                row["Columna"] = sintacticoTraductor.Errores()[i, 3];
                row["Extra"] = sintacticoTraductor.Errores()[i, 4];
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

            sintacticoTraductor.ClearErrores();

        }


        public void Write(string contenido)
        {
            this.textBox2.Text += contenido;
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

        private void ASTTraduccion_Click(object sender, EventArgs e)
        {
            sintacticoTraductor.graficar(EditorOutput());
            ConsoleInput(sintacticoTraductor.Message());
            ErrorTableTraductor();
        }

        private void TSTraduccion_Click(object sender, EventArgs e)
        {

        }
    }
}
