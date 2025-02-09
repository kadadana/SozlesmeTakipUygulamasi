using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SozlesmeTakipUygulamasi
{
    public partial class SozlesmeDBEkrani : Form
    {
        VeriDeposu depo = new VeriDeposu();

        public SozlesmeDBEkrani()
        {
            InitializeComponent();
            VerileriGoster();
        }



        public void VerileriGoster()
        {

            DataTable veriTablosu = depo.VerileriTabloyaGetir();

            dataGridView1.DataSource = veriTablosu;

            dataGridView1.Columns["Baslik"].HeaderText = "Başlık";
            dataGridView1.Columns["Taraflar"].HeaderText = "Taraflar";
            dataGridView1.Columns["BaslangicTarihi"].HeaderText = "Başlangıç Tarihi";
            dataGridView1.Columns["BitisTarihi"].HeaderText = "Bitiş Tarihi";
            dataGridView1.Columns["Tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["Durum"].HeaderText = "Durum";
            dataGridView1.Columns["DosyaYolu"].HeaderText = "Dosya Yolu";

            SozlesmeDBEkrani_Resize(this,EventArgs.Empty);

            this.Resize += SozlesmeDBEkrani_Resize;

        }

        private void btnSozlesmeEkle_Click(object sender, EventArgs e)
        {
            SozlesmeIslemleri sozlesmeEklemeEkrani = new SozlesmeIslemleri(1, this);


            sozlesmeEklemeEkrani.ShowDialog();


        }

        private void btnSozlesmeSil_Click(object sender, EventArgs e)
        {


            if (dataGridView1.SelectedRows.Count > 0)
            {

                DialogResult sonuc = MessageBox.Show("Seçilen sözleşmeyi silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    foreach (DataGridViewRow satir in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(satir.Cells["Id"].Value);
                        string dosyaYolu = satir.Cells["DosyaYolu"].Value.ToString();

                        try
                        {
                            if (!string.IsNullOrEmpty(dosyaYolu))
                            {
                                Directory.Delete(dosyaYolu, true);

                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Bir hata oluştu."); ;
                        }

                        depo.SozlesmeSil(id);



                        VerileriGoster();
                    }

                    MessageBox.Show("Sözleşme başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("En az bir satır seçmeniz gerekmektedir.");
            }
        }

        private void btnSozlesmeDuzenle_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow seciliSatir = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(seciliSatir.Cells["Id"].Value);

                SozlesmeIslemleri sozlesmeDuzenlemeEkrani = new SozlesmeIslemleri(2, this);
                sozlesmeDuzenlemeEkrani.VeriCekenId = id;
                sozlesmeDuzenlemeEkrani.SozlesmeGetir(id);
                sozlesmeDuzenlemeEkrani.ShowDialog();
            }
            else
            {
                MessageBox.Show("Düzenlemek için bir satır seçmeniz gerekmektedir.");
            }


        }

        private void btnSozlesmeleriGuncelle_Click(object sender, EventArgs e)
        {
            VerileriGoster();
        }

        private void btnKlasorYolunuAc_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                string dosyaYolu = @dataGridView1.SelectedRows[0].Cells["DosyaYolu"].Value.ToString();
                if (Directory.Exists(dosyaYolu))
                {
                    Process.Start("explorer.exe", dosyaYolu);
                }
                else
                {

                    MessageBox.Show(dosyaYolu + " yolunda klasör bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("En az bir satır seçmeniz gerekmektedir.");
            }

        }

        private void btnAra_Click(object sender, EventArgs e)
        {

            SozlesmeIslemleri sozlesmeAramaEkrani = new SozlesmeIslemleri(3, this);
            btnAra.Enabled = false;
            sozlesmeAramaEkrani.FormClosed += (s, args) => { btnAra.Enabled = true; };

            sozlesmeAramaEkrani.ShowDialog();
        }


        private void SozlesmeDBEkrani_Resize(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count >= 3)
            {
                int toplamGenislik = dataGridView1.ClientSize.Width;

                // Minimum genişlikler
                //dataGridView1.Columns["Id"].Width = 50;
                //dataGridView1.Columns["BaslangicTarihi"].Width = 121;
                //dataGridView1.Columns["BitisTarihi"].Width = 121;
                //dataGridView1.Columns["DosyaYolu"].Width = 265;

                
                int col0Width = 50;
                int col1Width = (int)(toplamGenislik * 0.05);
                int col2Width = (int)(toplamGenislik * 0.1);
                int col3Width = (int)(toplamGenislik * 0.1);
                int col4Width = (int)(toplamGenislik * 0.1);
                int col5Width = (int)(toplamGenislik * 0.1);
                int col6Width = (int)(toplamGenislik * 0.1);
                int col7Width = (int)(toplamGenislik * 0.1);
                int col8Width = (int)(toplamGenislik - (col0Width + col1Width + col2Width + col3Width + col4Width + col5Width + col6Width + col7Width) +7);

                dataGridView1.Columns[0].Width = col0Width;
                dataGridView1.Columns[0].Width = col1Width;
                dataGridView1.Columns[1].Width = col2Width;
                dataGridView1.Columns[2].Width = col3Width;
                dataGridView1.Columns[3].Width = col4Width;
                dataGridView1.Columns[4].Width = col5Width;
                dataGridView1.Columns[5].Width = col6Width;
                dataGridView1.Columns[6].Width = col7Width;
                dataGridView1.Columns[7].Width = col8Width;
            }
        }
    }
}