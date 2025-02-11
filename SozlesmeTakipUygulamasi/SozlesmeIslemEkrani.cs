using System.Xml.Linq;
using System.Data.SQLite;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Data;


namespace SozlesmeTakipUygulamasi
{
    public partial class SozlesmeIslemEkrani : Form
    {
        private SozlesmeDBEkrani anaForm;
        private string kokKlasoru = @"C:\Sozlesmeler\";
        private string secilenDosyaYolu = null;
        private VeriDeposu depo = new VeriDeposu();
        public int VeriCekenId { get; set; }
        public int islem;


        public SozlesmeIslemEkrani(int Islem, SozlesmeDBEkrani form)
        {
            islem = Islem;
            anaForm = form;

            /*
             * islem = 1; Ekle
             * islem = 2; Duzenle
             * islem = 3; Arama
             * 
             */

            InitializeComponent();
            if (islem == 1)
            {
                btnKaydet.Text = "Ekle";
                this.Refresh();
            }
            else if (islem == 2)
            {
                label7.Text = "Sözleşme Düzenle";
                this.Text = "Sözleşme Düzenle";
                btnKaydet.Text = "Kaydet";
                this.Refresh();
            }
            else if (islem == 3)
            {
                btnKaydet.Text = "Ara";
                label8.Text = "Sözleşme Id:";
                label4.Text = "Başlangıç Tarih Aralığı:";
                label5.Text = "Bitiş Tarih Aralığı:";
                dosyaEkle.Visible = false;
                txtAramaId.Visible = true;
                dtpBaslangicTarihi.ShowCheckBox = true;
                dtpBaslangicTarihi.Checked = false;
                dtpBaslangicSonu.Visible = true;
                dtpBaslangicSonu.ShowCheckBox = true;
                dtpBaslangicSonu.Checked = false;
                dtpBitisTarihi.ShowCheckBox = true;
                dtpBitisTarihi.Checked = false;
                dtpBitisSonu.ShowCheckBox = true;
                dtpBitisSonu.Checked = false;
                dtpBitisSonu.Visible = true;
                txtTutarMax.Visible= true;
                cmbDurum.Items.Add("Hepsi");
                cmbDurum.SelectedItem = "Hepsi";
                label7.Text = "Sözleşme Ara";
                this.Text = "Sözleşme Ara";
                this.MinimumSize = new Size(750, 400);
                this.Size = new Size(750, 400);
                this.Refresh();
            }
            depo.BaglantiOlustur();
            depo.TabloKontrolYoksaOlustur();

        }



        private void dosyaEkle_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "PDF Files|*.pdf|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    secilenDosyaYolu = ofd.FileName;
                }
            }
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {

            if (islem == 1)
            {
                sozlesmeEkle();
            }
            else if (islem == 2)
            {
                sozlesmeDuzenle();
            }
            else if (islem == 3)
            {
                sozlesmeAra();
            }

        }

        
        private void sozlesmeEkle()
        {

            Sozlesme sozlesme = new Sozlesme();
            int sayac = 1;
            string sozlesmeDosyaYolu = null;

            sozlesme.Id = depo.IdSayac() + 1;
            sozlesme.Baslik = txtBaslik.Text;
            sozlesme.Taraflar = txtTaraflar.Text;
            sozlesme.Tutar = Convert.ToDouble(txtTutar.Text);
            sozlesme.BaslangicTarihi = dtpBaslangicTarihi.Value;
            sozlesme.BitisTarihi = dtpBitisTarihi.Value;
            sozlesme.Durum = cmbDurum.SelectedItem.ToString();


            if (!Directory.Exists(kokKlasoru))
            {
                Directory.CreateDirectory(kokKlasoru);
            }
            string sozlesmeKlasoru = Path.Combine(kokKlasoru, sozlesme.Baslik);

            if (!Directory.Exists(sozlesmeKlasoru))
            {
                Directory.CreateDirectory(sozlesmeKlasoru);
            }
            else
            {
                while (Directory.Exists(sozlesmeKlasoru))
                {
                    sozlesmeKlasoru = Path.Combine($"{sozlesmeKlasoru} ({sayac})");
                    sayac++;
                }
            }
            

            
            if (!string.IsNullOrEmpty(secilenDosyaYolu))
            {
                sozlesmeDosyaYolu = DosyayiKopyala(secilenDosyaYolu, sozlesmeKlasoru);
            }

            

            sozlesme.DosyaYolu = sozlesmeKlasoru;

            depo.SozlesmeyiDByeEkle(sozlesme.Id, sozlesme.Baslik, sozlesme.Taraflar, sozlesme.BaslangicTarihi, sozlesme.BitisTarihi, (double)sozlesme.Tutar, sozlesme.Durum, sozlesme.DosyaYolu);

            secilenDosyaYolu = null;
            MessageBox.Show("Sözleşme eklenmiştir.");
            this.Close();
            anaForm.VerileriGoster();
        }
        private void sozlesmeDuzenle()
        {

            int sozlesmeId = VeriCekenId;
            string baslik = txtBaslik.Text;
            string taraflar = txtTaraflar.Text;
            DateTime baslangicTarihi = dtpBaslangicTarihi.Value;
            DateTime bitisTarihi = dtpBitisTarihi.Value;
            double tutar = Convert.ToDouble(txtTutar.Text);
            string durum = cmbDurum.SelectedItem.ToString();
            depo.SozlesmeGuncelle(sozlesmeId, baslik, taraflar, baslangicTarihi, bitisTarihi, tutar, durum);


            MessageBox.Show("Sözleşme başarıyla güncellendi.");

            this.Close();
        }
        private void sozlesmeAra()
        {
            string baslangicTarihiBasi;
            string baslangicTarihiSonu;
            string bitisTarihiBasi;
            string bitisTarihiSonu;
            string durum;
            string sozlesmeId = txtAramaId.Text;
            string baslik = txtBaslik.Text;
            string taraflar = txtTaraflar.Text;
            string tutarMin = null;
            string tutarMax = null;


            baslangicTarihiBasi = dtpBaslangicTarihi.Checked ? dtpBaslangicTarihi.Value.ToString("yyyy-MM-dd") : null;
            baslangicTarihiSonu = dtpBaslangicSonu.Checked ? dtpBaslangicSonu.Value.ToString("yyyy-MM-dd") : null;
            bitisTarihiBasi = dtpBitisTarihi.Checked ? dtpBitisTarihi.Value.ToString("yyyy-MM-dd") : null;
            bitisTarihiSonu = dtpBitisSonu.Checked ? dtpBitisSonu.Value.ToString("yyyy-MM-dd") : null;

            tutarMin = string.IsNullOrEmpty(txtTutar.Text) ? null : txtTutar.Text;
            tutarMax = string.IsNullOrEmpty(txtTutarMax.Text) ? null : txtTutarMax.Text;


            durum = cmbDurum.SelectedIndex >= 0 ? cmbDurum.SelectedItem.ToString() : "''";

            DataTable filtrelenmisTablo = depo.VeriFiltrele(sozlesmeId, baslik, taraflar, baslangicTarihiBasi, baslangicTarihiSonu, bitisTarihiBasi, bitisTarihiSonu, tutarMin, tutarMax, durum);

            anaForm.dataGridView1.DataSource = filtrelenmisTablo;
            anaForm.dataGridView1.Refresh();

        }




        private string DosyayiKopyala(string kaynakDosyaYolu, string hedefKlasör)
        {
            string sonucDosyaYolu = null;
            if (!string.IsNullOrEmpty(kaynakDosyaYolu) && File.Exists(kaynakDosyaYolu))
            {

                string dosyaAdi = Path.GetFileName(kaynakDosyaYolu);
                string hedefDosyaYolu = Path.Combine(hedefKlasör, dosyaAdi);
                File.Copy(kaynakDosyaYolu, hedefDosyaYolu, true);
                sonucDosyaYolu = Path.GetFullPath(hedefDosyaYolu);

                return sonucDosyaYolu;
            }
            return sonucDosyaYolu;
        }
        private void txtTutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        private void txtAramaId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        
        private void BitisTarihi_Validating(object sender, CancelEventArgs e)
        {
            if (dtpBaslangicTarihi.Checked == true && dtpBitisTarihi.Checked == true)
            {

                if (dtpBitisTarihi.Value < dtpBaslangicTarihi.Value)
                {
                    MessageBox.Show("Bitiş tarihi, başlangıç tarihinden önce olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }




        public void SozlesmeGetir(int sozlesmeId)
        {

            string yeniSozlesmeId = sozlesmeId.ToString();
            DataTable dataTable = depo.VeriFiltrele(yeniSozlesmeId);

            txtBaslik.Text = dataTable.Rows[0]["Baslik"].ToString();
            txtTaraflar.Text = dataTable.Rows[0]["Taraflar"].ToString();
            dtpBaslangicTarihi.Value = Convert.ToDateTime(dataTable.Rows[0]["BaslangicTarihi"]);
            dtpBitisTarihi.Value = Convert.ToDateTime(dataTable.Rows[0]["BitisTarihi"]);
            txtTutar.Text = dataTable.Rows[0]["Tutar"].ToString();
            string gecerlilikDurumu = dataTable.Rows[0]["Durum"].ToString();

            if (string.IsNullOrEmpty(gecerlilikDurumu) != null)
            {
                cmbDurum.Text = gecerlilikDurumu;
            }
        }
    }
}
