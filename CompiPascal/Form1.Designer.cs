
namespace CompiPascal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Run = new System.Windows.Forms.Button();
            this.Translate = new System.Windows.Forms.Button();
            this.ReportAST = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Save = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ReportErrors = new System.Windows.Forms.Button();
            this.ReportTS = new System.Windows.Forms.Button();
            this.ClearConsole = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Run);
            this.groupBox1.Controls.Add(this.Translate);
            this.groupBox1.Location = new System.Drawing.Point(22, 323);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compiler";
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(95, 19);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 1;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Translate
            // 
            this.Translate.Location = new System.Drawing.Point(14, 19);
            this.Translate.Name = "Translate";
            this.Translate.Size = new System.Drawing.Size(75, 23);
            this.Translate.TabIndex = 0;
            this.Translate.Text = "Translate";
            this.Translate.UseVisualStyleBackColor = true;
            this.Translate.Click += new System.EventHandler(this.Translate_Click);
            // 
            // ReportAST
            // 
            this.ReportAST.Location = new System.Drawing.Point(6, 22);
            this.ReportAST.Name = "ReportAST";
            this.ReportAST.Size = new System.Drawing.Size(75, 23);
            this.ReportAST.TabIndex = 2;
            this.ReportAST.Text = "AST";
            this.ReportAST.UseVisualStyleBackColor = true;
            this.ReportAST.Click += new System.EventHandler(this.ReportAST_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Save);
            this.groupBox2.Controls.Add(this.Load);
            this.groupBox2.Location = new System.Drawing.Point(222, 323);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 59);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(98, 22);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(17, 22);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 0;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 26);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(443, 277);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(481, 57);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(391, 246);
            this.textBox2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(617, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "CompiPascal";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ReportAST);
            this.groupBox3.Controls.Add(this.ReportErrors);
            this.groupBox3.Controls.Add(this.ReportTS);
            this.groupBox3.Location = new System.Drawing.Point(424, 323);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 59);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reports";
            // 
            // ReportErrors
            // 
            this.ReportErrors.Location = new System.Drawing.Point(168, 22);
            this.ReportErrors.Name = "ReportErrors";
            this.ReportErrors.Size = new System.Drawing.Size(75, 23);
            this.ReportErrors.TabIndex = 1;
            this.ReportErrors.Text = "Errors";
            this.ReportErrors.UseVisualStyleBackColor = true;
            this.ReportErrors.Click += new System.EventHandler(this.ReportErrors_Click);
            // 
            // ReportTS
            // 
            this.ReportTS.Location = new System.Drawing.Point(87, 22);
            this.ReportTS.Name = "ReportTS";
            this.ReportTS.Size = new System.Drawing.Size(75, 23);
            this.ReportTS.TabIndex = 0;
            this.ReportTS.Text = "TS";
            this.ReportTS.UseVisualStyleBackColor = true;
            this.ReportTS.Click += new System.EventHandler(this.ReportTS_Click);
            // 
            // ClearConsole
            // 
            this.ClearConsole.Location = new System.Drawing.Point(6, 22);
            this.ClearConsole.Name = "ClearConsole";
            this.ClearConsole.Size = new System.Drawing.Size(75, 23);
            this.ClearConsole.TabIndex = 6;
            this.ClearConsole.Text = "Clear";
            this.ClearConsole.UseVisualStyleBackColor = true;
            this.ClearConsole.Click += new System.EventHandler(this.ClearConsole_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ClearConsole);
            this.groupBox4.Location = new System.Drawing.Point(695, 323);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 59);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Console";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 396);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ReportAST;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Button Translate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ReportErrors;
        private System.Windows.Forms.Button ReportTS;
        private System.Windows.Forms.Button ClearConsole;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

