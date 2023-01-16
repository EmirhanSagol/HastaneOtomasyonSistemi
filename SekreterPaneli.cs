using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProgramlamaFinal
{
    public partial class SekreterPaneli : Form
    {
        public SekreterPaneli()
        {
            InitializeComponent();
        }
        HastaPaneli hasta_paneli = new HastaPaneli();
        DataTable tablo = new DataTable();
        Baglanti baglanti = new Baglanti();
        public string sekreter_tc;

        public string sekreter_ad_soyad;

        // Geri Butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            SekreterGirişPaneli geri = new SekreterGirişPaneli();
            geri.Show();
            this.Hide();
        }

        // Tablo güncelleme
        public void randevu_bilgi()
        {
            tablo.Clear();
            string sorgu5 = "Select * from Tbl_Randevu"; // Tüm randevuları listeleyen sorgu
            OleDbCommand cmd1 = new OleDbCommand(sorgu5, baglanti.baglan());
            OleDbDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                tablo.Rows.Add(reader1[0], reader1[6], reader1[3], reader1[4], reader1[1], reader1[2], reader1[5], reader1[7]);
            }
            dataGridView1.DataSource = tablo;
        }
        private void SekreterPaneli_Load(object sender, EventArgs e)
        {
            label3.Text = sekreter_tc;
            label4.Text = sekreter_ad_soyad;
            
            // Tablo sütun başlıkları
            tablo.Columns.Add("Randevu No", typeof(int));
            tablo.Columns.Add("Hasta TC", typeof(string));
            tablo.Columns.Add("Poliklinik", typeof(string));
            tablo.Columns.Add("Doktor", typeof(string));
            tablo.Columns.Add("Tarih", typeof(string));
            tablo.Columns.Add("Saat", typeof(string));
            tablo.Columns.Add("Durum", typeof(bool));
            tablo.Columns.Add("Şikayet", typeof(string));

            baglanti.baglan();
            string sorgu1 = "select * from Tbl_Brans"; // Branşları listeleyen sorgu
            OleDbCommand cmd = new OleDbCommand(sorgu1, baglanti.baglan());
            OleDbDataReader reader;
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                combo_poliklinik.Items.Add(reader[1]);
            }
            randevu_bilgi();

            baglanti.baglan().Close();
        }
        // Branş ve doktora göre randevu sorgulama
        private void btn_sorgula_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            if (combo_poliklinik.Text == "" && combo_poliklinik.Text == null && combo_doktor.Text == "" &&
                combo_doktor.Text == null)
            {
                MessageBox.Show("Lütfen poliklinik ve doktor seçiniz");
            }
            else if (combo_doktor.SelectedIndex > -1 && combo_doktor.SelectedIndex > -1)
            {
                tablo.Clear();
                string sorgu4 = "select * from Tbl_Randevu where Randevu_Doktor = @p1 and randevu_brans = @p2"; // Seçilen poliklinik
                // ve doktor için randevuları sorgular
                OleDbCommand cmd = new OleDbCommand(sorgu4, baglanti.baglan());
                cmd.Parameters.AddWithValue("@p1", combo_doktor.Text);
                cmd.Parameters.AddWithValue("@p2", combo_poliklinik.Text);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tablo.Rows.Add(reader[0], reader[6], reader[3], reader[4], reader[1], reader[2], reader[5], reader[7]);
                }
            }          
            else if (combo_doktor.SelectedIndex > -1)
            {
                tablo.Clear();
                string sorgu5 = "select * from Tbl_Randevu where Randevu_Doktor = @p1"; // Seçilen doktorun randevularını sorgular
                OleDbCommand cmd = new OleDbCommand(sorgu5, baglanti.baglan());
                cmd.Parameters.AddWithValue("@p1", combo_doktor.Text);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tablo.Rows.Add(reader[0], reader[6], reader[3], reader[4], reader[1], reader[2], reader[5], reader[7]);
                }
            }
            else if (combo_poliklinik.SelectedIndex > -1)
            {
                tablo.Clear();
                string sorgu6 = "select * from Tbl_Randevu where Randevu_Brans = @p1"; // Seçilen polikliniğin randevularını sorgular
                OleDbCommand cmd = new OleDbCommand(sorgu6, baglanti.baglan());
                cmd.Parameters.AddWithValue("@p1", combo_poliklinik.Text);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tablo.Rows.Add(reader[0], reader[6], reader[3], reader[4], reader[1], reader[2], reader[5], reader[7]);
                }
            }
            dataGridView1.DataSource = tablo;
            baglanti.baglan().Close();
        }

        // Poliklinik seçildiğinde polikliniğe ait doktorların combobox'da belirmesi
        private void combo_poliklinik_SelectedValueChanged(object sender, EventArgs e)
        {
            combo_doktor.Items.Clear();
            combo_doktor.Text = "";
            baglanti.baglan();
            string sorgu6 = "select * from Tbl_Doktor where doktor_brans = @p1"; // Poliklinik seçildiğinde o polikliniğe ait
            // doktorların bulan sorgu
            OleDbCommand cmd = new OleDbCommand(sorgu6, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", combo_poliklinik.Text);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                combo_doktor.Items.Add(reader[2].ToString() + " " + reader[3].ToString());
            }
            baglanti.baglan().Close();
        }

        // Belirlenen kişinin randevularını listelemek
        private void btn_bul_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            baglanti.baglan();
            string hasta_tc_sorgu = "Select * from Tbl_Randevu where Hasta_TC = @p1"; // Hasta TC'sine göre randevu sorgusu
            OleDbCommand cmd = new OleDbCommand(hasta_tc_sorgu, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", msktxt_hastatc.Text);
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tablo.Rows.Add(reader[0], reader[6], reader[3], reader[4], reader[1], reader[2], reader[5], reader[7]);
            }
            dataGridView1.DataSource = tablo;
            baglanti.baglan().Close();
        }
        // Tabloya çift tıklama
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                combo_poliklinik.Text = dataGridView1.SelectedRows[0].Cells["Poliklinik"].Value.ToString();
                combo_doktor.Text = dataGridView1.SelectedRows[0].Cells["Doktor"].Value.ToString();
                msktxt_hastatc.Text = dataGridView1.SelectedRows[0].Cells["Hasta TC"].Value.ToString();
            }
        }

        // Tümünü Listele
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            randevu_bilgi();
            baglanti.baglan().Close();
        }

        // Duyuru oluştur butonu
        private void btn_olustur_Click(object sender, EventArgs e)
        {
            if (richtxt_duyuru.Text == null || richtxt_duyuru.Text == "")
            {
                MessageBox.Show("Boş duyuru oluşturamassınız!");
                richtxt_duyuru.Focus();
            }
            else
            {
                baglanti.baglan();
                string sorgu7 = "insert into Tbl_Duyurular(Duyuru, Sekreter, Tarih) values (@p1, @p2, @p3)";
                OleDbCommand cmd = new OleDbCommand(sorgu7, baglanti.baglan());
                cmd.Parameters.AddWithValue("@p1", richtxt_duyuru.Text);
                cmd.Parameters.AddWithValue("@p2", sekreter_ad_soyad);
                cmd.Parameters.AddWithValue("@p3", DateTime.Now.Date);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Duyuru oluşturulmuştur!");
                baglanti.baglan().Close();
            }
            richtxt_duyuru.Clear();
        }

        // Duyurular butonu
        private void btn_duyurular_Click(object sender, EventArgs e)
        {
            Duyurular duyurular = new Duyurular();
            duyurular.Show();
        }
    }
}
