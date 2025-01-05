namespace SozlesmeTakipUygulamasi
{
    partial class SozlesmeEkle
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
            txtBaslik = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtTaraflar = new TextBox();
            label3 = new Label();
            txtTutar = new TextBox();
            dtpBaslangicTarihi = new DateTimePicker();
            dtpBitisTarihi = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            cmbDurum = new ComboBox();
            label6 = new Label();
            btnEkle = new Button();
            label7 = new Label();
            label8 = new Label();
            dosyaEkle = new Button();
            btnKaydet = new Button();
            SuspendLayout();
            // 
            // txtBaslik
            // 
            txtBaslik.BackColor = SystemColors.Window;
            txtBaslik.Font = new Font("Segoe UI", 12F);
            txtBaslik.ForeColor = SystemColors.ActiveCaptionText;
            txtBaslik.Location = new Point(287, 72);
            txtBaslik.Name = "txtBaslik";
            txtBaslik.Size = new Size(217, 29);
            txtBaslik.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(52, 72);
            label1.Name = "label1";
            label1.Size = new Size(128, 21);
            label1.TabIndex = 1;
            label1.Text = "Sözleşme Başlığı:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(52, 107);
            label2.Name = "label2";
            label2.Size = new Size(160, 21);
            label2.TabIndex = 3;
            label2.Text = "Sözleşmenin Tarafları:";
            // 
            // txtTaraflar
            // 
            txtTaraflar.BackColor = SystemColors.Window;
            txtTaraflar.Font = new Font("Segoe UI", 12F);
            txtTaraflar.ForeColor = SystemColors.ActiveCaptionText;
            txtTaraflar.Location = new Point(287, 107);
            txtTaraflar.Name = "txtTaraflar";
            txtTaraflar.Size = new Size(217, 29);
            txtTaraflar.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(52, 142);
            label3.Name = "label3";
            label3.Size = new Size(145, 21);
            label3.TabIndex = 5;
            label3.Text = "Sözleşmenin Tutarı:";
            // 
            // txtTutar
            // 
            txtTutar.BackColor = SystemColors.Window;
            txtTutar.Font = new Font("Segoe UI", 12F);
            txtTutar.ForeColor = SystemColors.ActiveCaptionText;
            txtTutar.Location = new Point(287, 142);
            txtTutar.Name = "txtTutar";
            txtTutar.Size = new Size(217, 29);
            txtTutar.TabIndex = 4;
            txtTutar.KeyPress += txtTutar_KeyPress;
            // 
            // dtpBaslangicTarihi
            // 
            dtpBaslangicTarihi.CalendarFont = new Font("Segoe UI", 12F);
            dtpBaslangicTarihi.Location = new Point(287, 177);
            dtpBaslangicTarihi.Name = "dtpBaslangicTarihi";
            dtpBaslangicTarihi.Size = new Size(217, 23);
            dtpBaslangicTarihi.TabIndex = 6;
            // 
            // dtpBitisTarihi
            // 
            dtpBitisTarihi.CalendarFont = new Font("Segoe UI", 12F);
            dtpBitisTarihi.Location = new Point(287, 209);
            dtpBitisTarihi.Name = "dtpBitisTarihi";
            dtpBitisTarihi.Size = new Size(217, 23);
            dtpBitisTarihi.TabIndex = 7;
            dtpBitisTarihi.Validating += BitisTarihi_Validating;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(52, 176);
            label4.Name = "label4";
            label4.Size = new Size(119, 21);
            label4.TabIndex = 8;
            label4.Text = "Başlangıç Tarihi:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(52, 207);
            label5.Name = "label5";
            label5.Size = new Size(83, 21);
            label5.TabIndex = 9;
            label5.Text = "Bitiş Tarihi:";
            // 
            // cmbDurum
            // 
            cmbDurum.FormattingEnabled = true;
            cmbDurum.Items.AddRange(new object[] { "Geçerli", "Sona ermiş" });
            cmbDurum.Location = new Point(287, 238);
            cmbDurum.Name = "cmbDurum";
            cmbDurum.Size = new Size(217, 23);
            cmbDurum.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(52, 236);
            label6.Name = "label6";
            label6.Size = new Size(138, 21);
            label6.TabIndex = 11;
            label6.Text = "Sözleşme Durumu";
            // 
            // btnEkle
            // 
            btnEkle.BackColor = SystemColors.MenuHighlight;
            btnEkle.FlatStyle = FlatStyle.Popup;
            btnEkle.Font = new Font("Segoe UI", 18F);
            btnEkle.Location = new Point(287, 307);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(217, 44);
            btnEkle.TabIndex = 15;
            btnEkle.Text = "Ekle";
            btnEkle.UseVisualStyleBackColor = false;
            btnEkle.Click += btnEkle_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 28F);
            label7.Location = new Point(147, 9);
            label7.Name = "label7";
            label7.Size = new Size(256, 51);
            label7.TabIndex = 13;
            label7.Text = "Sözleşme Ekle";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(52, 275);
            label8.Name = "label8";
            label8.Size = new Size(173, 21);
            label8.TabIndex = 14;
            label8.Text = "Sözleşme Dökümanları:";
            // 
            // dosyaEkle
            // 
            dosyaEkle.Font = new Font("Segoe UI", 12F);
            dosyaEkle.Location = new Point(287, 270);
            dosyaEkle.Name = "dosyaEkle";
            dosyaEkle.Size = new Size(217, 31);
            dosyaEkle.TabIndex = 12;
            dosyaEkle.Text = "Dosya ekle";
            dosyaEkle.UseVisualStyleBackColor = true;
            dosyaEkle.Click += dosyaEkle_Click;
            // 
            // btnKaydet
            // 
            btnKaydet.BackColor = SystemColors.MenuHighlight;
            btnKaydet.FlatStyle = FlatStyle.Popup;
            btnKaydet.Font = new Font("Segoe UI", 18F);
            btnKaydet.Location = new Point(287, 307);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(217, 44);
            btnKaydet.TabIndex = 16;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = false;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // SozlesmeEkle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(543, 364);
            Controls.Add(btnKaydet);
            Controls.Add(label7);
            Controls.Add(dosyaEkle);
            Controls.Add(label8);
            Controls.Add(btnEkle);
            Controls.Add(label6);
            Controls.Add(cmbDurum);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dtpBitisTarihi);
            Controls.Add(dtpBaslangicTarihi);
            Controls.Add(label3);
            Controls.Add(txtTutar);
            Controls.Add(label2);
            Controls.Add(txtTaraflar);
            Controls.Add(label1);
            Controls.Add(txtBaslik);
            Name = "SozlesmeEkle";
            Text = "Sözleşme Ekle";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBaslik;
        private Label label1;
        private Label label2;
        private TextBox txtTaraflar;
        private Label label3;
        private TextBox txtTutar;
        private DateTimePicker dtpBaslangicTarihi;
        private DateTimePicker dtpBitisTarihi;
        private Label label4;
        private Label label5;
        private ComboBox cmbDurum;
        private Label label6;
        private Button btnEkle;
        
        private Label label7;
        private Label label8;
        private Button dosyaEkle;
        private Button btnKaydet;
    }
}
