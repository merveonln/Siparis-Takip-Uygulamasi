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
    public partial class SiparisGuncelle : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-P4CB8E6\SQLEXPRESS;Initial Catalog=SiparisTakip;Integrated Security=True");
        public SiparisGuncelle()
        {
            InitializeComponent();
        }

        private void SiparisGuncelle_Load(object sender, EventArgs e)
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

            komut.CommandText = "update Siparis Set SiparisAdi = @sAdi, SiparisVerenAdi = @adi, SiparisVerenSoyadi = @soy, TelefonNumarasi = @telNo, AdresBilgisi = @adres, Eposta = @ePost where SiparisId = @sID";

            komut.Parameters.AddWithValue("@sAdi", txtSiparisAdi.Text);
            komut.Parameters.AddWithValue("@adi", txtAdi.Text);
            komut.Parameters.AddWithValue("@soy", txtSoyadi.Text);
            komut.Parameters.AddWithValue("@telNo", txtNumara.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@ePost", txtEposta.Text);
            komut.Parameters.AddWithValue("@sID", txtSiparisID.Text);

            int a = komut.ExecuteNonQuery();

            if (a > 0)
            {
                MessageBox.Show("Sipariş Güncellendi!", "Başarılı");
                txtAdi.Clear();
                txtAdres.Clear();
                txtEposta.Clear();
                txtNumara.Clear();
                txtSiparisAdi.Clear();
                txtSiparisID.Clear();
                txtSoyadi.Clear();
            }
            else
            {
                MessageBox.Show("Sipariş Güncellenemedi!", "Uyarı");
            }

            baglanti.Close();

            this.DialogResult = DialogResult.OK;
        }
        }
    }
