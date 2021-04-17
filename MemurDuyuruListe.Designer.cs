namespace BankaHesapUygulaması
{
    partial class MemurDuyuruListe
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
            this.LstBxDuyuru = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LstBxDuyuru
            // 
            this.LstBxDuyuru.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LstBxDuyuru.Font = new System.Drawing.Font("Myanmar Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LstBxDuyuru.FormattingEnabled = true;
            this.LstBxDuyuru.ItemHeight = 27;
            this.LstBxDuyuru.Location = new System.Drawing.Point(30, 12);
            this.LstBxDuyuru.Name = "LstBxDuyuru";
            this.LstBxDuyuru.Size = new System.Drawing.Size(533, 436);
            this.LstBxDuyuru.TabIndex = 0;
            // 
            // MemurDuyuruListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(596, 494);
            this.Controls.Add(this.LstBxDuyuru);
            this.Name = "MemurDuyuruListe";
            this.Text = "MemurDuyuruListe";
            this.Load += new System.EventHandler(this.MemurDuyuruListe_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LstBxDuyuru;
    }
}