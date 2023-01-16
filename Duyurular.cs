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
    public partial class Duyurular : Form
    {
        public Duyurular()
        {
            InitializeComponent();
        }
        Baglanti baglanti = new Baglanti();
        DataTable tablo = new DataTable();

        private void Duyurular_Load(object sender, EventArgs e)
        {
            // Tablo sütun başlıkları
            tablo.Columns.Add("Gönderen", typeof(string));
            tablo.Columns.Add("Duyuru", typeof(string));
            tablo.Columns.Add("Tarih", typeof(string));

            baglanti.baglan();
            string duyuru_sorgu = "Select * from Tbl_Duyurular"; // Tüm duyuruları listeleyen sorgu
            OleDbCommand cmd = new OleDbCommand(duyuru_sorgu, baglanti.baglan());
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tablo.Rows.Add(reader[2], reader[1], reader[3].ToString());
            }
            dataGridView1.DataSource = tablo;
            baglanti.baglan().Close();
        }

        // Geri butonu
        private void btn_geri_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
