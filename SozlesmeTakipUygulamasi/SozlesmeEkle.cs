using System.Xml.Linq;
using System.Data.SQLite;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;


namespace SozlesmeTakipUygulamasi
{
    public partial class SozlesmeEkle : Form
    {
        public int SozlesmeId { get; set; }
        private string kokKlasoru = @"C:\Sozlesmeler\";
        private string secilenDosyaYolu = null;
        private VeriDeposu depo = new VeriDeposu();
        public int VeriCekenId { get; set; }



        public SozlesmeEkle(bool DuzenlemeMi)
        {
            bool duzenlemeMi = DuzenlemeMi;

            InitializeComponent();
            if (duzenlemeMi)
            {
                btnEkle.Visible = false;
                btnKaydet.Visible = true;
                this.Refresh();
            }
            else
            {
                btnEkle.Visible = true;
                btnKaydet.Visible = false;
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



            VeriDeposu veriDeposu = new VeriDeposu();

            SQLiteConnection baglanti = veriDeposu.BaglantiOlustur();

            int sozlesmeId = VeriCekenId;
            string baslik = txtBaslik.Text;
            string taraflar = txtTaraflar.Text;
            DateTime baslangicTarihi = dtpBaslangicTarihi.Value;
            DateTime bitisTarihi = dtpBitisTarihi.Value;
            double tutar = Convert.ToDouble(txtTutar.Text);
            string durum = cmbDurum.SelectedIndex.ToString();

            string guncelleKomutu = @"
        UPDATE Sozlesme
        SET Baslik = @Baslik,
            Taraflar = @Taraflar,
            BaslangicTarihi = @BaslangicTarihi,
            BitisTarihi = @BitisTarihi,
            Tutar = @Tutar,
            Durum = @Durum
        WHERE Id = @Id;
    ";

            using (var komut = new SQLiteCommand(guncelleKomutu, baglanti))
            {
                komut.Parameters.AddWithValue("@Id", sozlesmeId);
                komut.Parameters.AddWithValue("@Baslik", baslik);
                komut.Parameters.AddWithValue("@Taraflar", taraflar);
                komut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi.ToString("yyyy-MM-dd"));
                komut.Parameters.AddWithValue("@BitisTarihi", bitisTarihi.ToString("yyyy-MM-dd"));
                komut.Parameters.AddWithValue("@Tutar", tutar);
                komut.Parameters.AddWithValue("@Durum", durum);

                komut.ExecuteNonQuery();
            }

            MessageBox.Show("Sözleşme başarıyla güncellendi.");

            this.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            Sozlesme sozlesme = new Sozlesme();

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

            if (!string.IsNullOrEmpty(secilenDosyaYolu))
            {
                sozlesmeDosyaYolu = DosyayiKopyala(secilenDosyaYolu, sozlesmeKlasoru);
            }



            sozlesme.DosyaYolu = sozlesmeKlasoru;

            depo.SozlesmeyiDByeEkle(sozlesme.Id, sozlesme.Baslik, sozlesme.Taraflar, sozlesme.BaslangicTarihi, sozlesme.BitisTarihi, (double)sozlesme.Tutar, sozlesme.Durum, sozlesme.DosyaYolu);

            secilenDosyaYolu = null;
            MessageBox.Show("Sözleşme eklenmiştir.");
            this.Close();

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
        private void BaslangicTarihi_ValueChanged(object sender, EventArgs e)
        {
            dtpBitisTarihi.MinDate = dtpBaslangicTarihi.Value;
        }
        private void BitisTarihi_Validating(object sender, CancelEventArgs e)
        {
            if (dtpBitisTarihi.Value < dtpBaslangicTarihi.Value)
            {
                MessageBox.Show("Bitiş tarihi, başlangıç tarihinden önce olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }



        public void VerileriYukle(int sozlesmeId)
        {

            Sozlesme sozlesme = depo.SozlesmeGetir(sozlesmeId);

            txtBaslik.Text = sozlesme.Baslik;
            txtTaraflar.Text = sozlesme.Taraflar;
            dtpBaslangicTarihi.Value = sozlesme.BaslangicTarihi;
            dtpBitisTarihi.Value = sozlesme.BitisTarihi;
            txtTutar.Text = sozlesme.Tutar.ToString();
            cmbDurum.SelectedItem = sozlesme.Durum;
        }
        public void SozlesmeGetir(int sozlesmeId)
        {
            VeriDeposu veriDeposu = new VeriDeposu();
            SQLiteConnection baglanti = veriDeposu.BaglantiOlustur();

            string sorgu = "SELECT * FROM Sozlesme WHERE Id = @Id";
            using (SQLiteCommand komut = new SQLiteCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@Id", sozlesmeId);

                using (SQLiteDataReader reader = komut.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtBaslik.Text = reader["Baslik"].ToString();
                        txtTaraflar.Text = reader["Taraflar"].ToString();
                        dtpBaslangicTarihi.Value = Convert.ToDateTime(reader["BaslangicTarihi"]);
                        dtpBitisTarihi.Value = Convert.ToDateTime(reader["BitisTarihi"]);
                        txtTutar.Text = reader["Tutar"].ToString();
                        string gecerlilikDurumu = reader["Durum"].ToString();

                        if (cmbDurum.Items.Contains(gecerlilikDurumu))
                        {
                            cmbDurum.SelectedItem = gecerlilikDurumu;
                        }
                        else
                        {
                            MessageBox.Show("Veritabanındaki geçerlilik durumu ComboBox'ta mevcut değil.");
                        }
                    }
                }
            }
        }

    }
}
