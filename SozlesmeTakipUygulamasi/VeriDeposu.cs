using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace SozlesmeTakipUygulamasi
{
    public class VeriDeposu
    {

        public SQLiteConnection BaglantiOlustur()
        {
            string dbYolu = @"C:\Sozlesmeler\sozlesmeler.db";

            if (!File.Exists(dbYolu))
            {
                SQLiteConnection.CreateFile(dbYolu);
            }

            SQLiteConnection baglanti = new SQLiteConnection($"Data Source={dbYolu};Version=3;");
            baglanti.Open();

            return baglanti;
        }
        public void TabloKontrolYoksaOlustur()
        {
            using (var baglanti = BaglantiOlustur())
            {


                string tabloKontrolSorgusu = @"
                SELECT name
                FROM sqlite_master
                WHERE type='table' AND name='sozlesme';";




                using (var komut = new SQLiteCommand(tabloKontrolSorgusu, baglanti))
                {
                    var sonuc = komut.ExecuteScalar();
                    if (sonuc == null)
                    {

                        string tabloOlusturmaKomutu = @"
                        CREATE TABLE IF NOT EXISTS Sozlesme (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Baslik TEXT NOT NULL,
                        Taraflar TEXT,
                        BaslangicTarihi TEXT,
                        BitisTarihi TEXT,
                        Tutar DOUBLE,
                        Durum TEXT,
                        DosyaYolu TEXT
                    );
                ";

                        using (var tabloKomut = new SQLiteCommand(tabloOlusturmaKomutu, baglanti))
                        {
                            tabloKomut.ExecuteNonQuery();
                        }
                    }
                    else
                    {

                    }

                }


            }

        }
        public void SozlesmeyiDByeEkle(int id, string baslik, string taraflar, DateTime baslangicTarihi, DateTime bitisTarihi, double tutar, string durum, string dosyaYolu)
        {
            using (var baglanti = BaglantiOlustur())
            {
                string ekleKomutu = @"
                    INSERT INTO Sozlesme (Baslik, Taraflar, BaslangicTarihi, BitisTarihi, Tutar, Durum, DosyaYolu)
                    VALUES (@Baslik, @Taraflar, @BaslangicTarihi, @BitisTarihi, @Tutar, @Durum, @DosyaYolu);
                    ";
                using (var komut = new SQLiteCommand(ekleKomutu, baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", id);
                    komut.Parameters.AddWithValue("@Baslik", baslik);
                    komut.Parameters.AddWithValue("@Taraflar", taraflar);
                    komut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi.ToString("dd-MM-yyyy"));
                    komut.Parameters.AddWithValue("@BitisTarihi", bitisTarihi.ToString("dd-MM-yyyy"));
                    komut.Parameters.AddWithValue("@Tutar", tutar);
                    komut.Parameters.AddWithValue("@Durum", durum);
                    komut.Parameters.AddWithValue("@DosyaYolu", dosyaYolu);

                    komut.ExecuteNonQuery();

                }
            }

        }



        public void SozlesmeSil(int id)
        {
            using (var baglanti = BaglantiOlustur())
            {
                string silKomutu = "DELETE FROM Sozlesme WHERE Id = @Id";

                using (var komut = new SQLiteCommand(silKomutu, baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", id);

                    komut.ExecuteNonQuery();
                }
            }
        }
        public DataTable VerileriTabloyaGetir()
        {
            TabloKontrolYoksaOlustur();
            DataTable dataTable = new DataTable();

            using (var baglanti = BaglantiOlustur())
            {

                string sorgu = "SELECT * FROM Sozlesme";

                using (var komut = new SQLiteCommand(sorgu, baglanti))
                {
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(komut);
                    dataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }



        public int IdSayac()
        {
            int idSayac = 1;

            using (var baglanti = BaglantiOlustur())
            {
                string sorgu = "SELECT MAX(Id) FROM Sozlesme";

                using (var komut = new SQLiteCommand(sorgu, baglanti))
                {
                    object sonuc = komut.ExecuteScalar();

                    if (sonuc != DBNull.Value)
                    {
                        idSayac = Convert.ToInt32(sonuc) + 1;
                    }
                }
            }

            return idSayac;
        }

        public DataTable VeriFiltrele(string sozlesmeId)
        {

            DataTable dataTable = new DataTable();


            using (var baglanti = BaglantiOlustur())
            {
                string sorgu = $"SELECT * FROM Sozlesme WHERE Id LIKE \"{sozlesmeId}\"";

                using (var komut = new SQLiteCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@deger", sozlesmeId);
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(komut);
                    dataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public DataTable VeriFiltrele(string sozlesmeId, string baslik, string taraflar, string baslangicTarihiBasi, string baslangicTarihiSonu, string bitisTarihiBasi, string bitisTarihiSonu, string tutarMin, string tutarMax, string durum)
        {
            string yeniDurum = durum == "Hepsi" ? "" : durum;
            DataTable dataTable = new DataTable();

            using (var baglanti = BaglantiOlustur())
            {
                string sorgu = "SELECT * FROM Sozlesme WHERE @degerId = '' OR Id LIKE @degerId\n"
                    + "INTERSECT\n" +
                    "SELECT * FROM Sozlesme WHERE @degerBaslik = '' OR Baslik LIKE @degerBaslik\n"
                    + "INTERSECT\n" +
                    "SELECT * FROM Sozlesme WHERE @degerTaraflar = '' OR Taraflar LIKE @degerTaraflar\n"
                    + "INTERSECT\n" +
                    "SELECT * FROM Sozlesme WHERE (@degerBaslangicTarihiBasi IS NULL OR BaslangicTarihi >= @degerBaslangicTarihiBasi) AND (@degerBaslangicSonu IS NULL OR BaslangicTarihi <= @degerBaslangicSonu)\n"
                    + "INTERSECT\n" +
                    "SELECT * FROM Sozlesme WHERE (@degerBitisTarihiBasi IS NULL OR BitisTarihi >= @degerBitisTarihiBasi) AND (@degerBitisSonu IS NULL OR BitisTarihi <= @degerBitisSonu)\n"
                    + "INTERSECT\n" +
                    "SELECT * FROM Sozlesme WHERE (@degerTutarMin IS NULL OR Tutar >= @degerTutarMin) AND (@degerTutarMax IS NULL OR Tutar <= @degerTutarMax)\n"
                    + "INTERSECT\n" +
                    "SELECT * FROM Sozlesme WHERE @degerDurum = '' OR Durum LIKE @degerDurum\n";

                using (var komut = new SQLiteCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@degerId", sozlesmeId);
                    komut.Parameters.AddWithValue("@degerBaslik", "%" + baslik + "%");
                    komut.Parameters.AddWithValue("@degerTaraflar", "%" + taraflar + "%");
                    komut.Parameters.AddWithValue("@degerBaslangicTarihiBasi", baslangicTarihiBasi);
                    komut.Parameters.AddWithValue("@degerBaslangicSonu", baslangicTarihiSonu);
                    komut.Parameters.AddWithValue("@degerBitisTarihiBasi", bitisTarihiBasi);
                    komut.Parameters.AddWithValue("@degerBitisSonu", bitisTarihiSonu);
                    komut.Parameters.AddWithValue("@degerTutarMin", tutarMin);
                    komut.Parameters.AddWithValue("@degerTutarMax", tutarMax);
                    komut.Parameters.AddWithValue("@degerDurum", "%" + yeniDurum  + "%");

                    

                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(komut);
                    dataAdapter.Fill(dataTable);


                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Hiçbir veri bulunamadı.");
                    }
                }
            }
            return dataTable;

        }
        public void SozlesmeGuncelle(int sozlesmeId, string baslik, string taraflar, DateTime baslangicTarihi, DateTime bitisTarihi, double tutar, string durum)
        {
            SQLiteConnection baglanti = BaglantiOlustur();
            string guncelleKomutu = @"
            UPDATE Sozlesme
            SET 
                Baslik = @Baslik,
                Taraflar = @Taraflar,
                BaslangicTarihi = @BaslangicTarihi,
                BitisTarihi = @BitisTarihi,
                Tutar = @Tutar,
                Durum = @Durum
            WHERE Id = @Id;";

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
        }


    }
}
