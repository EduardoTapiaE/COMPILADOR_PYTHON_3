﻿namespace LenguajesyAutomatas
{
    partial class frmEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrEjecutar = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrEjecutarAnalizadorLexico = new System.Windows.Forms.ToolStripMenuItem();
            this.tsrEjecutarAnalizadorSintactico = new System.Windows.Forms.ToolStripMenuItem();
            this.semanticoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recorrerArbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprobacionDeTiposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codigoPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.editorTexto1 = new LenguajesyAutomatas.EditorTexto();
            this.dgvListaTokens = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.rtbCodigoP = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTokens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.tsrEjecutar,
            this.tsrEjecutarAnalizadorLexico,
            this.tsrEjecutarAnalizadorSintactico,
            this.semanticoToolStripMenuItem,
            this.recorrerArbolToolStripMenuItem,
            this.comprobacionDeTiposToolStripMenuItem,
            this.codigoPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1060, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.abrirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // tsrEjecutar
            // 
            this.tsrEjecutar.Name = "tsrEjecutar";
            this.tsrEjecutar.Size = new System.Drawing.Size(61, 20);
            this.tsrEjecutar.Text = "Ejecutar";
            this.tsrEjecutar.Click += new System.EventHandler(this.ejecutarToolStripMenuItem_Click);
            // 
            // tsrEjecutarAnalizadorLexico
            // 
            this.tsrEjecutarAnalizadorLexico.Enabled = false;
            this.tsrEjecutarAnalizadorLexico.Name = "tsrEjecutarAnalizadorLexico";
            this.tsrEjecutarAnalizadorLexico.Size = new System.Drawing.Size(53, 20);
            this.tsrEjecutarAnalizadorLexico.Text = "Lexico";
            this.tsrEjecutarAnalizadorLexico.Click += new System.EventHandler(this.tsrEjecutarAnalizadorLexico_Click);
            // 
            // tsrEjecutarAnalizadorSintactico
            // 
            this.tsrEjecutarAnalizadorSintactico.Enabled = false;
            this.tsrEjecutarAnalizadorSintactico.Name = "tsrEjecutarAnalizadorSintactico";
            this.tsrEjecutarAnalizadorSintactico.Size = new System.Drawing.Size(71, 20);
            this.tsrEjecutarAnalizadorSintactico.Text = "Sintactico";
            this.tsrEjecutarAnalizadorSintactico.Click += new System.EventHandler(this.tsrEjecutarAnalizadorSintactico_Click);
            // 
            // semanticoToolStripMenuItem
            // 
            this.semanticoToolStripMenuItem.Enabled = false;
            this.semanticoToolStripMenuItem.Name = "semanticoToolStripMenuItem";
            this.semanticoToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.semanticoToolStripMenuItem.Text = "Semantico";
            this.semanticoToolStripMenuItem.Click += new System.EventHandler(this.semanticoToolStripMenuItem_Click);
            // 
            // recorrerArbolToolStripMenuItem
            // 
            this.recorrerArbolToolStripMenuItem.Name = "recorrerArbolToolStripMenuItem";
            this.recorrerArbolToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.recorrerArbolToolStripMenuItem.Text = "Recorrer Arbol";
            this.recorrerArbolToolStripMenuItem.Click += new System.EventHandler(this.recorrerArbolToolStripMenuItem_Click);
            // 
            // comprobacionDeTiposToolStripMenuItem
            // 
            this.comprobacionDeTiposToolStripMenuItem.Name = "comprobacionDeTiposToolStripMenuItem";
            this.comprobacionDeTiposToolStripMenuItem.Size = new System.Drawing.Size(144, 20);
            this.comprobacionDeTiposToolStripMenuItem.Text = "Comprobacion de tipos";
            this.comprobacionDeTiposToolStripMenuItem.Click += new System.EventHandler(this.comprobacionDeTiposToolStripMenuItem_Click);
            // 
            // codigoPToolStripMenuItem
            // 
            this.codigoPToolStripMenuItem.Name = "codigoPToolStripMenuItem";
            this.codigoPToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.codigoPToolStripMenuItem.Text = "Codigo P";
            this.codigoPToolStripMenuItem.Click += new System.EventHandler(this.codigoPToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(27, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 647);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.editorTexto1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(619, 621);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // editorTexto1
            // 
            this.editorTexto1.Location = new System.Drawing.Point(0, 0);
            this.editorTexto1.Name = "editorTexto1";
            this.editorTexto1.Size = new System.Drawing.Size(623, 603);
            this.editorTexto1.TabIndex = 0;
            // 
            // dgvListaTokens
            // 
            this.dgvListaTokens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvListaTokens.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvListaTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaTokens.Location = new System.Drawing.Point(711, 54);
            this.dgvListaTokens.Name = "dgvListaTokens";
            this.dgvListaTokens.Size = new System.Drawing.Size(317, 125);
            this.dgvListaTokens.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(797, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lista de tokens";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(835, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Errores";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(711, 209);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(317, 158);
            this.dataGridView2.TabIndex = 4;
            // 
            // rtbCodigoP
            // 
            this.rtbCodigoP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCodigoP.Location = new System.Drawing.Point(711, 383);
            this.rtbCodigoP.Name = "rtbCodigoP";
            this.rtbCodigoP.Size = new System.Drawing.Size(317, 287);
            this.rtbCodigoP.TabIndex = 6;
            this.rtbCodigoP.Text = "";
            // 
            // frmEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1060, 684);
            this.Controls.Add(this.rtbCodigoP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvListaTokens);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1076, 723);
            this.MinimumSize = new System.Drawing.Size(1076, 723);
            this.Name = "frmEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "El mejor compilador de python 3";
            this.Load += new System.EventHandler(this.frmEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaTokens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsrEjecutarAnalizadorLexico;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private EditorTexto editorTexto1;
        private System.Windows.Forms.DataGridView dgvListaTokens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripMenuItem tsrEjecutarAnalizadorSintactico;
        private System.Windows.Forms.ToolStripMenuItem semanticoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsrEjecutar;
        private System.Windows.Forms.ToolStripMenuItem recorrerArbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprobacionDeTiposToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codigoPToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbCodigoP;
    }
}