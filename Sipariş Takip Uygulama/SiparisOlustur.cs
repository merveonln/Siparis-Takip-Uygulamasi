using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sipariş_Takip_Uygulama
{
    public partial class SiparisOlustur : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-P4CB8E6\SQLEXPRESS;Initial Catalog=SiparisTakip;Integrated Security=True");
        public SiparisOlustur()
        {
            InitializeComponent();
        }

        private void SiparisOlustur_Load(object sender, EventArgs e)
        {

        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = baglanti.CreateCommand();

            komut.CommandText = "insert into Siparis(SiparisAdi, SiparisVerenAdi, SiparisVerenSoyadi, TelefonNumarasi, AdresBilgisi, Eposta) values(@sAdi, @adi, @soy, @telNo, @adres, @ePost)";

            //komut.Parameters.AddWithValue("@sID", txtSiparisID.Text);
            komut.Parameters.AddWithValue("@sAdi", txtSiparisAdi.Text);
            komut.Parameters.AddWithValue("@adi", txtAdi.Text);
            komut.Parameters.AddWithValue("@soy", txtSoyadi.Text);
            komut.Parameters.AddWithValue("@telNo", txtNumara.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@ePost", txtEposta.Text);

            int a = komut.ExecuteNonQuery();

            if (a > 0)
            {
                MessageBox.Show("Sipariş Eklendi!", "Başarılı");
            }
            else
            {
                MessageBox.Show("Sipariş Eklenemedi!", "Uyarı");
            }

            baglanti.Close();

            this.DialogResult = DialogResult.OK;
        }
    }
}
