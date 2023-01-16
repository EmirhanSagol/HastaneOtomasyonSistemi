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
    public partial class HastaKayıtPaneli : Form
    {
        public HastaKayıtPaneli()
        {
            InitializeComponent();
        }
        Baglanti baglanti = new Baglanti();
        private void HastaKayıtPaneli_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kadın");
            label8.Visible = false;
        }

        // Geri butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            HastaGirişPaneli giris = new HastaGirişPaneli();
            giris.Show();
            this.Close();
        }

        // Kayıt ol butonu
        private void btn_kayıt_Click(object sender, EventArgs e)
        {
            baglanti.baglan();
            // Bilgileri veri tabanına kayıt eden sorgu
            OleDbCommand cmd = new OleDbCommand("insert into Tbl_Hasta(hasta_ad, hasta_soyad, hasta_telefon, hasta_sifre, " +
                "hasta_cinsiyet, hasta_TC)" + "values(@p1, @p2, @p3, @p4, @p5, @p6)", baglanti.baglan());
            cmd.Parameters.AddWithValue("@p1", textBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@p2", textBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@p3", maskedTextBox2.Text.ToString());
            cmd.Parameters.AddWithValue("@p4", msktxt_sifre.Text.ToString());
            cmd.Parameters.AddWithValue("@p5", comboBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@p6", maskedTextBox1.Text.ToString());

            cmd.ExecuteNonQuery();

            MessageBox.Show("Kayıt oluşturulmuştur");

            baglanti.baglan().Close();
        }
    }
}
