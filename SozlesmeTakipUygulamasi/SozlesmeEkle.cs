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
        private int idSayac = 1;
        private bool gecerlilikBool = false;
        public int VeriCekenId;

        public bool DuzenlemeMi;

        public SozlesmeEkle()
        {

            InitializeComponent();

            depo.BaglantiOlustur();
            depo.TabloOlustur();

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

        private void btnEkle_Click(object sender, EventArgs e)
        {

            if (cmbGecerlilik.SelectedItem.ToString() == "Geçerli")
            {
                gecerlilikBool = true;
            }
            else
            {
                gecerlilikBool = false;
            }

            Sozlesme sozlesme = new Sozlesme
            {


                Id = idSayac++,
                Baslik = txtBaslik.Text,
                Taraflar = txtTaraflar.Text,
                Tutar = Convert.ToDouble(txtTutar.Text),
                BaslangicTarihi = dtpBaslangicTarihi.Value,
                BitisTarihi = dtpBitisTarihi.Value,
                Gecerlilik = gecerlilikBool,
                DosyaYolu = secilenDosyaYolu,

            };




            if (!Directory.Exists(kokKlasoru))
            {
                Directory.CreateDirectory(kokKlasoru);
            }
            string sozlesmeKlasoru = Path.Combine(kokKlasoru, sozlesme.Baslik);

            if (!Directory.Exists(sozlesmeKlasoru))
            {
                Directory.CreateDirectory(sozlesmeKlasoru);
            }

            if (!string.IsNullOrEmpty(sozlesme.DosyaYolu))
            {
                DosyayiKopyala(sozlesme.DosyaYolu, sozlesmeKlasoru);
            }

            depo.SozlesmeyiDByeEkle(sozlesme.Id, sozlesme.Baslik, sozlesme.Taraflar, sozlesme.BaslangicTarihi, sozlesme.BitisTarihi, (double)sozlesme.Tutar, sozlesme.Gecerlilik);

            secilenDosyaYolu = null;

        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {

        }


        private void DosyayiKopyala(string kaynakDosyaYolu, string hedefKlasör)
        {
            if (!string.IsNullOrEmpty(kaynakDosyaYolu) && File.Exists(kaynakDosyaYolu))
            {

                string dosyaAdi = Path.GetFileName(kaynakDosyaYolu);
                string hedefDosyaYolu = Path.Combine(hedefKlasör, dosyaAdi);

                File.Copy(kaynakDosyaYolu, hedefDosyaYolu, true);

            }
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

        private void SozlesmeEkle_Load(object sender, EventArgs e)
        {

            this.Controls.Add(btnDuzenle);
            if (DuzenlemeMi)
            {

                Sozlesme sozlesme = depo.SozlesmeGetir(SozlesmeId);

                if (sozlesme != null)
                {
                    txtBaslik.Text = sozlesme.Baslik;
                    txtTaraflar.Text = sozlesme.Taraflar;
                    dtpBaslangicTarihi.Value = sozlesme.BaslangicTarihi;
                    dtpBitisTarihi.Value = sozlesme.BitisTarihi;
                    txtTutar.Text = sozlesme.Tutar.ToString();
                    cmbGecerlilik.SelectedItem = sozlesme.Gecerlilik;

                    btnEkle.Visible = false;
                    btnDuzenle.Visible = true;

                }
                else
                {
                    MessageBox.Show("Sözleşme bilgileri bulunamadı.");

                }

            }
            else
            {

                btnEkle.Visible = true;
                btnDuzenle.Visible = false;


            }
            this.Refresh();

        }

        private void txtBaslik_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
