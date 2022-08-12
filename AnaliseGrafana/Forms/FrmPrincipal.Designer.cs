﻿
namespace AnaliseGrafana.Forms
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.btnAnalisar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwResultados = new System.Windows.Forms.ListView();
            this.dtpInicial = new System.Windows.Forms.DateTimePicker();
            this.dtpFinal = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCriterio = new System.Windows.Forms.TextBox();
            this.lvwRequests = new System.Windows.Forms.ListView();
            this.txtMetodo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnDetalhar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAnalisar
            // 
            this.btnAnalisar.Location = new System.Drawing.Point(756, 38);
            this.btnAnalisar.Name = "btnAnalisar";
            this.btnAnalisar.Size = new System.Drawing.Size(75, 23);
            this.btnAnalisar.TabIndex = 4;
            this.btnAnalisar.Text = "Analisar";
            this.btnAnalisar.UseVisualStyleBackColor = true;
            this.btnAnalisar.Click += new System.EventHandler(this.btnAnalisar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data Inicial";
            // 
            // lvwResultados
            // 
            this.lvwResultados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvwResultados.HideSelection = false;
            this.lvwResultados.Location = new System.Drawing.Point(12, 67);
            this.lvwResultados.Name = "lvwResultados";
            this.lvwResultados.Size = new System.Drawing.Size(488, 531);
            this.lvwResultados.TabIndex = 5;
            this.lvwResultados.UseCompatibleStateImageBehavior = false;
            this.lvwResultados.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwResultados_MouseDoubleClick);
            // 
            // dtpInicial
            // 
            this.dtpInicial.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInicial.Location = new System.Drawing.Point(12, 38);
            this.dtpInicial.Name = "dtpInicial";
            this.dtpInicial.Size = new System.Drawing.Size(149, 23);
            this.dtpInicial.TabIndex = 0;
            // 
            // dtpFinal
            // 
            this.dtpFinal.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFinal.Location = new System.Drawing.Point(167, 38);
            this.dtpFinal.Name = "dtpFinal";
            this.dtpFinal.Size = new System.Drawing.Size(149, 23);
            this.dtpFinal.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Data Final";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Critério";
            // 
            // txtCriterio
            // 
            this.txtCriterio.Location = new System.Drawing.Point(322, 38);
            this.txtCriterio.Name = "txtCriterio";
            this.txtCriterio.Size = new System.Drawing.Size(279, 23);
            this.txtCriterio.TabIndex = 2;
            // 
            // lvwRequests
            // 
            this.lvwRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwRequests.HideSelection = false;
            this.lvwRequests.Location = new System.Drawing.Point(506, 67);
            this.lvwRequests.Name = "lvwRequests";
            this.lvwRequests.Size = new System.Drawing.Size(719, 531);
            this.lvwRequests.TabIndex = 6;
            this.lvwRequests.UseCompatibleStateImageBehavior = false;
            // 
            // txtMetodo
            // 
            this.txtMetodo.Location = new System.Drawing.Point(607, 38);
            this.txtMetodo.Name = "txtMetodo";
            this.txtMetodo.Size = new System.Drawing.Size(143, 23);
            this.txtMetodo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(607, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Método";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnDetalhar
            // 
            this.btnDetalhar.Location = new System.Drawing.Point(837, 38);
            this.btnDetalhar.Name = "btnDetalhar";
            this.btnDetalhar.Size = new System.Drawing.Size(75, 23);
            this.btnDetalhar.TabIndex = 14;
            this.btnDetalhar.Text = "Detalhar";
            this.btnDetalhar.UseVisualStyleBackColor = true;
            this.btnDetalhar.Click += new System.EventHandler(this.btnDetalhar_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 610);
            this.Controls.Add(this.btnDetalhar);
            this.Controls.Add(this.txtMetodo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lvwRequests);
            this.Controls.Add(this.txtCriterio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpInicial);
            this.Controls.Add(this.lvwResultados);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAnalisar);
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAnalisar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvwResultados;
        private System.Windows.Forms.DateTimePicker dtpInicial;
        private System.Windows.Forms.DateTimePicker dtpFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCriterio;
        private System.Windows.Forms.ListView lvwRequests;
        private System.Windows.Forms.TextBox txtMetodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnDetalhar;
    }
}

