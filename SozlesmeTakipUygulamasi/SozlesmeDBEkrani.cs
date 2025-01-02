using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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



        private void VerileriGoster()
        {


            VeriDeposu depo = new VeriDeposu();
            DataTable veriTablosu = depo.VerileriTabloyaGetir();

            dataGridView1.DataSource = veriTablosu;

            dataGridView1.Columns["DosyaYolu"].Visible = false;


            dataGridView1.Columns["Baslik"].HeaderText = "Başlık";
            dataGridView1.Columns["Taraflar"].HeaderText = "Taraflar";
            dataGridView1.Columns["BaslangicTarihi"].HeaderText = "Başlangıç Tarihi";
            dataGridView1.Columns["BitisTarihi"].HeaderText = "Bitiş Tarihi";
            dataGridView1.Columns["Tutar"].HeaderText = "Tutar";
            dataGridView1.Columns["Gecerlilik"].HeaderText = "Geçerlilik";

        }

        private void btnSozlesmeEkle_Click(object sender, EventArgs e)
        {
            SozlesmeEkle sozlesmeEklemeEkrani = new SozlesmeEkle();

            sozlesmeEklemeEkrani.DuzenlemeMi = false;
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

                        depo.SozlesmeSil(id);

                        dataGridView1.Rows.Remove(satir);
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

        public void btnSozlesmeDuzenle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {

                DataGridViewRow seciliSatir = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(seciliSatir.Cells["Id"].Value);

                SozlesmeEkle sozlesmeEklemeEkrani = new SozlesmeEkle();

                sozlesmeEklemeEkrani.VeriCekenId = id;
                sozlesmeEklemeEkrani.DuzenlemeMi = true;

                sozlesmeEklemeEkrani.ShowDialog();


            }
            else
            {
                MessageBox.Show("Düzenlemek için bir satır seçmeniz gerekmektedir.");
            }
        }

        private void SozlesmeDBEkrani_Load(object sender, EventArgs e)
        {

        }
    }

}
