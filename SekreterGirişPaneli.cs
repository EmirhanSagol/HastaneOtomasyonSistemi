using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProgramlamaFinal
{
    public partial class SekreterGirişPaneli : Form
    {
        public SekreterGirişPaneli()
        {
            InitializeComponent();
        }

        Baglanti baglanti = new Baglanti();
        
        // Geri butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            GirişPaneli giris = new GirişPaneli();
            giris.Show();
            this.Close();
        }
        // Giriş butonu
        private void btn_giris_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            string onay = "Select * from Tbl_Sekreter where Sekreter_TC = @tc and Sekreter_Sifre = @sifre"; // Giriş bilgilerini
            // kontrol eden sorgu
            OleDbCommand command = new OleDbCommand(onay, baglanti.baglan());
            command.Parameters.AddWithValue("@tc", msktxt_tc.Text.ToString());
            command.Parameters.AddWithValue("@sifre", msktxt_sifre.Text.ToString());
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.Read()) // Giriş bilgileri doğruysa
            {
                MessageBox.Show("Giriş Başarılı");
                SekreterPaneli giris = new SekreterPaneli();
                giris.sekreter_tc = msktxt_tc.Text.ToString();
                giris.sekreter_ad_soyad = reader[2].ToString() + " " + reader[3].ToString();
                giris.Show();
                this.Close();

            }
            else // Giriş bilgileri yanlışsa
            {
                MessageBox.Show("Tc veya şifre hatalı. Tekrar deneyiniz");
            }
            baglanti.baglan().Close();
        }
    }
}
