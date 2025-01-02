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
        public void TabloOlustur()
        {
            using (var baglanti = BaglantiOlustur())
            {
                string tabloOlusturmaKomutu = @"
                CREATE TABLE IF NOT EXISTS Sozlesme (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Baslik TEXT NOT NULL,
                    Taraflar TEXT,
                    BaslangicTarihi TEXT,
                    BitisTarihi TEXT,
                    Tutar REAL,
                    Gecerlilik BOOL
                );
            ";
                using (var komut = new SQLiteCommand(tabloOlusturmaKomutu, baglanti))
                {
                    komut.ExecuteNonQuery();
                }
            }

        }
        public void SozlesmeyiDByeEkle(int id, string baslik, string taraflar, DateTime baslangicTarihi, DateTime bitisTarihi, double tutar, bool gecerlilik)
        {
            using (var baglanti = BaglantiOlustur())
            {
                string ekleKomutu = @"
                    INSERT INTO Sozlesme (Baslik, Taraflar, BaslangicTarihi, BitisTarihi, Tutar, Gecerlilik)
                    VALUES (@Baslik, @Taraflar, @BaslangicTarihi, @BitisTarihi, @Tutar, @Gecerlilik);
                    ";
                using (var komut = new SQLiteCommand(ekleKomutu, baglanti))
                {
                    komut.Parameters.AddWithValue("@Id", id);
                    komut.Parameters.AddWithValue("@Baslik", baslik);
                    komut.Parameters.AddWithValue("@Taraflar", taraflar);
                    komut.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
                    komut.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);
                    komut.Parameters.AddWithValue("@Tutar", tutar);
                    komut.Parameters.AddWithValue("@Gecerlilik", gecerlilik);

                    komut.ExecuteNonQuery();

                }
            }
            
        }
        


        


        private void SozlesmeleriGuncelle(int id, string baslik)
        {
            using (var baglanti = BaglantiOlustur())
            {
                string guncelleKomutu = @"
                UPDATE Sozlesme
                SET Baslik = @Baslik
                WHERE Id = @Id;
            ";

                using (var komut = new SQLiteCommand(guncelleKomutu, baglanti))
                {
                    komut.Parameters.AddWithValue("@Baslik", baslik);
                    komut.Parameters.AddWithValue("@Id", id);

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
                                Gecerlilik = Convert.ToBoolean(reader["Gecerlilik"])
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
