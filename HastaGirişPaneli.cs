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
    public partial class HastaGirişPaneli : Form
    {
        public HastaGirişPaneli()
        {
            InitializeComponent();
        }

        Baglanti baglanti = new Baglanti();
        
        // Kayıt ol butonu
        private void btn_kayit_Click(object sender, EventArgs e)
        {
            HastaKayıtPaneli kayıt = new HastaKayıtPaneli();
            kayıt.Show();
            this.Close();
        }

        // Giriş butonu
        private void btn_giris_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            string onay = "Select * from Tbl_Hasta where Hasta_TC = @tc and Hasta_Sifre = @sifre"; // Giiriş bilgilerini kontrol
            //eden sorgu
            OleDbCommand sorgu = new OleDbCommand(onay, baglanti.baglan());
            OleDbDataReader reader;
            sorgu.Parameters.AddWithValue("@tc", msktxt_tc.Text.ToString());
            sorgu.Parameters.AddWithValue("@sifre", msktxt_sifre.Text.ToString());
            reader = sorgu.ExecuteReader();

            if (reader.Read()) // Giriş bilgileri doğruysa
            {
                MessageBox.Show("Giriş Yapıldı");
                HastaPaneli hst_panel = new HastaPaneli();
                hst_panel.tc = msktxt_tc.Text.ToString();
                hst_panel.ad_soyad = reader[1] + " " + reader[2];
                hst_panel.Show();
                this.Close();
            }
            else // Giriş bilgileri yanlışsa
            {
                MessageBox.Show("TC veya şifre hatalı. Tekrar deneyiniz.");
            }
            baglanti.baglan().Close();
        }

        // Geri butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            GirişPaneli giris = new GirişPaneli();
            giris.Show();
            this.Close();
        }
    }
}
