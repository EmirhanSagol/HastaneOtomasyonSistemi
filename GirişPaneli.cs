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
    public partial class GirişPaneli : Form
    {
        public GirişPaneli()
        {
            InitializeComponent();
        }
        // Hasta giriş butonu
        private void btn_hasta_Click(object sender, EventArgs e)
        {
            HastaGirişPaneli hst_giris = new HastaGirişPaneli();
            hst_giris.Show();
            this.Hide();
        }

        // Sekreter giriş butonu
        private void btn_sekreter_Click(object sender, EventArgs e)
        {
            SekreterGirişPaneli sek_giris = new SekreterGirişPaneli();
            sek_giris.Show();
            this.Hide();
        }

        // Doktor giriş butonu
        private void btn_doktor_Click(object sender, EventArgs e)
        {
            DoktorGiriş dok_giris = new DoktorGiriş();
            dok_giris.Show();
            this.Hide();
        }
    }
}
