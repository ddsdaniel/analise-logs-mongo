namespace AnaliseGrafana.Forms
{
    partial class FrmDetalhe
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
            this.lvwLogs = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvwLogs
            // 
            this.lvwLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwLogs.HideSelection = false;
            this.lvwLogs.Location = new System.Drawing.Point(12, 12);
            this.lvwLogs.Name = "lvwLogs";
            this.lvwLogs.Size = new System.Drawing.Size(776, 426);
            this.lvwLogs.TabIndex = 6;
            this.lvwLogs.UseCompatibleStateImageBehavior = false;
            this.lvwLogs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwLogs_MouseDoubleClick);
            // 
            // FrmDetalhe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvwLogs);
            this.KeyPreview = true;
            this.Name = "FrmDetalhe";
            this.Text = "FrmDetalhe";
            this.Load += new System.EventHandler(this.FrmDetalhe_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDetalhe_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwLogs;
    }
}