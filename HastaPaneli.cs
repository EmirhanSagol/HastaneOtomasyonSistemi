using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProgramlamaFinal
{
    public partial class HastaPaneli : Form
    {
        public HastaPaneli()
        {
            InitializeComponent();
        }

        public string tc;
        public string ad_soyad;
        DataTable tablo = new DataTable();
        Baglanti baglanti = new Baglanti(); // Bağlantı adresi
        
        // Hasta bilgilerini tabloya yazdıran metot
        public void hasta_bilgi()
        {
            tablo.Clear(); // önceki bilgileri tablodan temizlemek için
            string sorgu5 = "Select * from Tbl_Randevu where Hasta_TC = @p1";
            OleDbCommand cmd = new OleDbCommand(sorgu5, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", tc);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // reader'dan gelen değerler tablo nesnesine eklenir
                tablo.Rows.Add(reader[0], reader[3], reader[4], reader[1], reader[2], reader[5], reader[7]);
            }

            dataGridView1.DataSource = tablo;
        }

        // Geri butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            HastaGirişPaneli hst_giris = new HastaGirişPaneli();
            hst_giris.Show();
            this.Close();
        }

        private void HastaPaneli_Load(object sender, EventArgs e)
        {
            // Tabloda bulunan sütunlar
            tablo.Columns.Add("Randevu No", typeof(int));
            tablo.Columns.Add("Poliklinik", typeof(string));
            tablo.Columns.Add("Doktor", typeof(string));
            tablo.Columns.Add("Tarih", typeof(string));
            tablo.Columns.Add("Saat", typeof(string));
            tablo.Columns.Add("Durum", typeof(bool));
            tablo.Columns.Add("Şikayet", typeof(string));

            label8.Text = tc;
            label9.Text = ad_soyad;
            baglanti.baglan();
            string sorgu1 = "select * from Tbl_Brans"; // Veritabanındaki branşları bulan sorgu
            OleDbCommand cmd = new OleDbCommand(sorgu1, baglanti.baglan());
            OleDbDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                combo_poliklinik.Items.Add(reader[1]); // Branşları combobox'a ekler
            }

            hasta_bilgi(); // tablo yenilenir

            baglanti.baglan().Close();
        }

        // Poliklinik 
        private void combo_poliklinik_SelectedValueChanged(object sender, EventArgs e)
        {
            combo_doktor.Items.Clear();
            combo_doktor.Text = "";
            baglanti.baglan();
            string sorgu2 = "select * from Tbl_Doktor where doktor_brans = @p1";
            OleDbCommand cmd = new OleDbCommand(sorgu2, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", combo_poliklinik.Text);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                combo_doktor.Items.Add(reader[2].ToString() + " " + reader[3].ToString());
            }

            baglanti.baglan().Close();
        }

        private void combo_doktor_SelectedValueChanged(object sender, EventArgs e)
        {
            txt_no.Clear();
            baglanti.baglan();
            string sorgu3 = "Select * from Tbl_Doktor where doktor_brans = @p1";
            OleDbCommand cmd = new OleDbCommand(sorgu3, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", combo_poliklinik.Text);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txt_no.Text = reader[0].ToString();
            }
            baglanti.baglan().Close();
        }
        // Randevu oluştur butonu
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            string ekle = "insert into Tbl_Randevu(randevu_tarih, randevu_saat, randevu_brans, randevu_doktor," +
                "randevu_durum, Hasta_TC, hasta_sikayet) values(@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
            OleDbCommand cmd = new OleDbCommand(ekle, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", dateTimePicker1.Text.ToString());
            cmd.Parameters.AddWithValue("@p2", msktxt_saat.Text.ToString());
            cmd.Parameters.AddWithValue("@p3", combo_poliklinik.Text.ToString());
            cmd.Parameters.AddWithValue("@p4", combo_doktor.Text.ToString());
            cmd.Parameters.AddWithValue("@p5", true);
            cmd.Parameters.AddWithValue("@p6", tc.ToString());
            cmd.Parameters.AddWithValue("@p7", richtxt_sikayet.Text.ToString());

            cmd.ExecuteNonQuery();
            MessageBox.Show("Randevu bilgileri kaydedilmiştir.");

            hasta_bilgi();
            
            baglanti.baglan().Close();
        }

        // tabloya çift tıklama
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
             combo_poliklinik.Text = dataGridView1.SelectedRows[0].Cells["Poliklinik"].Value.ToString();
             combo_doktor.Text = dataGridView1.SelectedRows[0].Cells["Doktor"].Value.ToString();
             dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells["Tarih"].Value.ToString();
             msktxt_saat.Text = dataGridView1.SelectedRows[0].Cells["Saat"].Value.ToString();
             richtxt_sikayet.Text = dataGridView1.SelectedRows[0].Cells["Şikayet"].Value.ToString();
            }
        }

        // Randevu iptal butonu
        private void btn_iptal_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            string sorgu6 = "delete from Tbl_Randevu where randevu_ID = @p1";
            OleDbCommand cmd = new OleDbCommand(sorgu6, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", dataGridView1.SelectedRows[0].Cells["Randevu No"].Value);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Seçili randevu silinmiştir!");
            hasta_bilgi();
            baglanti.baglan().Close();
        }

        // Randevu güncelle butonu
        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            string sorgu7 = "update Tbl_Randevu set randevu_tarih = @p1, randevu_saat = @p2, randevu_brans = @p3 " +
                ", randevu_doktor = @p4, hasta_sikayet = @p5 where randevu_ID = @p6";
            OleDbCommand cmd = new OleDbCommand(sorgu7, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", dateTimePicker1.Text.ToString());
            cmd.Parameters.AddWithValue("@p2", msktxt_saat.Text.ToString());
            cmd.Parameters.AddWithValue("@p3", combo_poliklinik.Text.ToString());
            cmd.Parameters.AddWithValue("@p4", combo_doktor.Text.ToString());
            cmd.Parameters.AddWithValue("@p5", richtxt_sikayet.Text);
            cmd.Parameters.AddWithValue("@p6", dataGridView1.SelectedRows[0].Cells["Randevu No"].Value);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Randevu bilgileri düzenlenmiştir!");
            hasta_bilgi();
            baglanti.baglan().Close();
        }
    }
}
