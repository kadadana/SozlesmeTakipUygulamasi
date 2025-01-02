namespace SozlesmeTakipUygulamasi
{
    partial class SozlesmeDBEkrani
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SozlesmeDBEkrani));
            dataGridView1 = new DataGridView();
            veriDeposuBindingSource = new BindingSource(components);
            veriDeposuBindingSource1 = new BindingSource(components);
            btnSozlesmeSil = new Button();
            btnSozlesmeEkle = new Button();
            btnSozlesmeDuzenle = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)veriDeposuBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)veriDeposuBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.BackgroundColor = SystemColors.GradientActiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.MenuHighlight;
            resources.ApplyResources(dataGridView1, "dataGridView1");
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            // 
            // veriDeposuBindingSource
            // 
            veriDeposuBindingSource.DataSource = typeof(VeriDeposu);
            // 
            // veriDeposuBindingSource1
            // 
            veriDeposuBindingSource1.DataSource = typeof(VeriDeposu);
            // 
            // btnSozlesmeSil
            // 
            resources.ApplyResources(btnSozlesmeSil, "btnSozlesmeSil");
            btnSozlesmeSil.Name = "btnSozlesmeSil";
            btnSozlesmeSil.UseVisualStyleBackColor = true;
            btnSozlesmeSil.Click += btnSozlesmeSil_Click;
            // 
            // btnSozlesmeEkle
            // 
            resources.ApplyResources(btnSozlesmeEkle, "btnSozlesmeEkle");
            btnSozlesmeEkle.Name = "btnSozlesmeEkle";
            btnSozlesmeEkle.UseVisualStyleBackColor = true;
            btnSozlesmeEkle.Click += btnSozlesmeEkle_Click;
            // 
            // btnSozlesmeDuzenle
            // 
            resources.ApplyResources(btnSozlesmeDuzenle, "btnSozlesmeDuzenle");
            btnSozlesmeDuzenle.Name = "btnSozlesmeDuzenle";
            btnSozlesmeDuzenle.UseVisualStyleBackColor = true;
            btnSozlesmeDuzenle.Click += btnSozlesmeDuzenle_Click;
            // 
            // SozlesmeDBEkrani
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            Controls.Add(btnSozlesmeDuzenle);
            Controls.Add(btnSozlesmeEkle);
            Controls.Add(btnSozlesmeSil);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "SozlesmeDBEkrani";
            Load += SozlesmeDBEkrani_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)veriDeposuBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)veriDeposuBindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private BindingSource veriDeposuBindingSource;
        private BindingSource veriDeposuBindingSource1;
        private Button btnSozlesmeSil;
        private Button btnSozlesmeEkle;
        private Button btnSozlesmeDuzenle;
    }
}