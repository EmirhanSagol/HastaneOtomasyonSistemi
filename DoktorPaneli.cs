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
    public partial class DoktorPaneli : Form
    {
        public DoktorPaneli()
        {
            InitializeComponent();
        }

        Baglanti baglanti = new Baglanti();
        public string doktor_tc;
        public string doktor_adsoyad;
        public string doktor_ID;
        DataTable tablo = new DataTable();
        private void DoktorGirişPaneli_Load(object sender, EventArgs e)
        {
            label3.Text = doktor_tc;
            label5.Text = doktor_adsoyad;
            baglanti.baglan();
            
            // Tablo sütunlarındaki başlıklar
            tablo.Columns.Add("Randevu No", typeof(string));
            tablo.Columns.Add("Tarih", typeof(string));
            tablo.Columns.Add("Saat", typeof(string));
            tablo.Columns.Add("Poliklinik", typeof(string));
            tablo.Columns.Add("Doktor", typeof(string));
            tablo.Columns.Add("Durum", typeof(bool));
            tablo.Columns.Add("Hasta TC", typeof(string));
            tablo.Columns.Add("Şikayet", typeof(string));

            string sorgu1 = "select * from Tbl_Randevu where randevu_doktor = @p1"; // Doktora ait randevuları bulan sorgu
            OleDbCommand cmd = new OleDbCommand(sorgu1, baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", doktor_adsoyad);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tablo.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]);
            }
            dataGridView1.DataSource = tablo;
            baglanti.baglan().Close();
        }
        // Geri Butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            DoktorGiriş geri = new DoktorGiriş();
            geri.Show();
            this.Close();
        }

        // Randevu tablosuna çift tıklama
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( dataGridView1.SelectedRows.Count > 0)
            {
                richTextBox1.Text = dataGridView1.SelectedRows[0].Cells["Şikayet"].Value.ToString();
            }
        }
        // Duyuru butonu
        private void button1_Click(object sender, EventArgs e)
        {
            Duyurular duyuru = new Duyurular();
            duyuru.Show();
        }
    }
}
