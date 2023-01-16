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
using System.Xml;

namespace GörselProgramlamaFinal
{
    public partial class DoktorGiriş : Form
    {
        public DoktorGiriş()
        {
            InitializeComponent();
        }
        Baglanti baglanti = new Baglanti();
        
        // Geri butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            GirişPaneli geri = new GirişPaneli();
            geri.Show();
            this.Close();
        }

        // Giriş butonu
        private void btn_giris_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            string onay = "Select * from Tbl_Doktor where Doktor_TC = @tc and Doktor_Sifre = @sifre"; // Giriş bilgilerini
            // kontrol eden sorgu
            OleDbCommand cmd = new OleDbCommand(onay, baglanti.baglan());
            OleDbDataReader reader;
            cmd.Parameters.AddWithValue("@tc", msktxt_tc.Text.ToString());
            cmd.Parameters.AddWithValue("@sifre", msktxt_sifre.Text.ToString());
            reader = cmd.ExecuteReader();

            if (reader.Read()) // Giriş bilgiler doğruysa
            {
                MessageBox.Show("Giriş Yapıldı");
                DoktorPaneli giris = new DoktorPaneli();
                giris.doktor_tc = msktxt_tc.Text.ToString();
                giris.doktor_adsoyad = reader[2].ToString() + " " + reader[3].ToString();
                giris.doktor_ID = reader[0].ToString();
                giris.Show();
                this.Close();
            }
            else // Giriş bilgileri yanlışsa
            {
                MessageBox.Show("TC veya şifre yanlış. Tekrar deneyiniz");
                msktxt_tc.Clear();
                msktxt_sifre.Clear();
                msktxt_tc.Focus();
            }
            baglanti.baglan().Close();
        }
    }
}
