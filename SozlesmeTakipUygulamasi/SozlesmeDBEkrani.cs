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


            VeriDeposu depo = new VeriDeposu();
            DataTable veriTablosu = depo.VerileriTabloyaGetir();

            dataGridView1.DataSource = veriTablosu;


            dataGridView1.Columns["Baslik"].HeaderText = "Başlık";
            dataGridView1.Columns["Taraflar"].HeaderText = "Taraflar";
            dataGridView1.Columns["BaslangicTarihi"].HeaderText = "Başlangıç Tarihi";
            dataGridView1.Columns["BitisTarihi"].HeaderText = "Bitiş Tarihi";
            dataGridView1.Columns["Tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["Durum"].HeaderText = "Durum";
            dataGridView1.Columns["DosyaYolu"].HeaderText = "Dosya Yolu";

            dataGridView1.Columns["Id"].Width = 50;
            dataGridView1.Columns["BaslangicTarihi"].Width = 125;
            dataGridView1.Columns["BitisTarihi"].Width = 125;

            dataGridView1.Columns["DosyaYolu"].Width = 265;

        }

        private void btnSozlesmeEkle_Click(object sender, EventArgs e)
        {
            SozlesmeEkle sozlesmeEklemeEkrani = new SozlesmeEkle(false);


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

                SozlesmeEkle sozlesmeDuzenlemeEkrani = new SozlesmeEkle(true);
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
                int id = Convert.ToInt32(dataGridView1.SelectedRows [0].Cells["Id"].Value);
                string dosyaYolu = dataGridView1.SelectedRows[0].Cells["DosyaYolu"].Value.ToString();
                dosyaYolu = dosyaYolu.Replace(@"\\", @"\");
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


    }

}
