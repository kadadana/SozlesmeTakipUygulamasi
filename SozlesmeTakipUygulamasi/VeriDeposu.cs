using System;
using System.Data;
using System.Data.SQLite;


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
                    
                    komut.ExecuteNonQuery() ;
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

        public Sozlesme SozlesmeBilgileriniGetir(int sozlesmeId)
        {
            using (var baglanti = BaglantiOlustur())
            {
                string sorgu = "SELECT * FROM Sozlesme WHERE Id = @SozlesmeId";

                using (var komut = new SQLiteCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@SozlesmeId", sozlesmeId);
                    using (var reader = komut.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Sozlesme sozlesme = new Sozlesme
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Baslik = reader["Baslik"].ToString(),
                                Taraflar = reader["Taraflar"].ToString(),
                                BaslangicTarihi = DateTime.TryParseExact(
                                    reader["BaslangicTarihi"].ToString(),
                                    "dd-MM-yyyy",
                                    null,
                                    System.Globalization.DateTimeStyles.None,
                                    out var baslangicTarih) ? baslangicTarih : DateTime.MinValue,
                                BitisTarihi = DateTime.TryParseExact(
                                    reader["BitisTarihi"].ToString(),
                                    "dd-MM-yyyy",
                                    null,
                                    System.Globalization.DateTimeStyles.None,
                                    out var bitisTarih) ? bitisTarih : DateTime.MinValue,
                                Tutar = Convert.ToDouble(reader["Tutar"]),
                                Durum = reader["Durum"].ToString(),
                                DosyaYolu = reader["DosyaYolu"].ToString()
                            };

                            return sozlesme;
                        }
                    }
                }
            }
            return null;
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

        public Sozlesme SozlesmeGetir(int sozlesmeId)
        {
            using (var baglanti = BaglantiOlustur())
            {
                string sorgu = "SELECT * FROM Sozlesme WHERE Id = @SozlesmeId";

                using (var komut = new SQLiteCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@SozlesmeId", sozlesmeId);
                    using (var reader = komut.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Sozlesme sozlesme = new Sozlesme
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Baslik = reader["Baslik"].ToString(),
                                Taraflar = reader["Taraflar"].ToString(),
                                BaslangicTarihi = Convert.ToDateTime(reader["BaslangicTarihi"]),
                                BitisTarihi = Convert.ToDateTime(reader["BitisTarihi"]),
                                Tutar = Convert.ToDouble(reader["Tutar"]),
                                Durum = reader["Durum"].ToString(),
                                DosyaYolu = reader["DosyaYolu"].ToString()
                            };

                            return sozlesme;
                        }
                    }
                }
            }
            return null;
        }



    }

}
