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
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-P4CB8E6\SQLEXPRESS;Initial Catalog=SiparisTakip;Integrated Security=True");
        void listele()
        {
            listView1.Items.Clear();

            baglanti.Open();

            SqlCommand komut = baglanti.CreateCommand();
            komut.CommandText = "Select * from Siparis";

            SqlDataReader okuyucu = komut.ExecuteReader();

            while (okuyucu.Read())
            {
                ListViewItem eleman = new ListViewItem();
                eleman.Text = okuyucu["SiparisId"].ToString();
                eleman.SubItems.Add(okuyucu["SiparisAdi"].ToString());
                eleman.SubItems.Add(okuyucu["SiparisVerenAdi"].ToString());
                eleman.SubItems.Add(okuyucu["SiparisVerenSoyadi"].ToString());
                eleman.SubItems.Add(okuyucu["TelefonNumarasi"].ToString());
                eleman.SubItems.Add(okuyucu["AdresBilgisi"].ToString());
                eleman.SubItems.Add(okuyucu["Eposta"].ToString());
                listView1.Items.Add(eleman);
            }

            baglanti.Close();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            MessageBox.Show("Sipariş Takip Programıma Hoşgeldin!!!", "MERVE ÖNALAN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            baglanti.Open();

            SqlCommand komut = baglanti.CreateCommand();

            if (cmbArama.SelectedIndex == 0)
            {
                komut.CommandText = "Select * from Siparis where SiparisId LIKE '" + txtYazi.Text + "%'";
            }

            if (cmbArama.SelectedIndex == 1)
            {
                komut.CommandText = "Select * from Siparis where SiparisAdi LIKE '" + txtYazi.Text + "%'";
            }

            if (cmbArama.SelectedIndex == 2)
            {
                komut.CommandText = "Select * from Siparis where SiparisVerenAdi LIKE '" + txtYazi.Text + "%'";
            }

            if (cmbArama.SelectedIndex == 3)
            {
                komut.CommandText = "Select * from Siparis where Eposta LIKE '" + txtYazi.Text + "%'";
            }

            SqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                ListViewItem eleman = new ListViewItem();
                eleman.Text = okuyucu["SiparisId"].ToString();
                eleman.SubItems.Add(okuyucu["SiparisAdi"].ToString());
                eleman.SubItems.Add(okuyucu["SiparisVerenAdi"].ToString());
                eleman.SubItems.Add(okuyucu["SiparisVerenSoyadi"].ToString());
                eleman.SubItems.Add(okuyucu["TelefonNumarasi"].ToString());
                eleman.SubItems.Add(okuyucu["AdresBilgisi"].ToString());
                eleman.SubItems.Add(okuyucu["Eposta"].ToString());
                listView1.Items.Add(eleman);
            }

            baglanti.Close();
        }

        private void tsbSil_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int a = listView1.SelectedItems.Count;

                DialogResult sonuc = MessageBox.Show(a + " Kişinin, Siparişini İptal Edeceksiniz Emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo);

                if (sonuc == DialogResult.Yes)
                {

                    foreach (ListViewItem eleman in listView1.SelectedItems)
                    {

                        baglanti.Open();

                        string silinecek = eleman.SubItems[0].Text;
                        SqlCommand komut = baglanti.CreateCommand();

                        komut.CommandText = "Delete from Siparis where SiparisId=" + silinecek + "";
                        komut.ExecuteNonQuery();

                        baglanti.Close();

                        listele();
                    }

                }

            }
            else
            {
                MessageBox.Show("İptal Edilecek Siparişi Seçiniz.", "Uyarı!");
            }
        
        
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SiparisOlustur f1 = new SiparisOlustur();
            DialogResult sonuc = f1.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                listele();
            }
            f1.txtAdi.Clear();
            f1.txtAdres.Clear();
            f1.txtEposta.Clear();
            f1.txtNumara.Clear();
            f1.txtSiparisAdi.Clear();
            f1.txtSiparisID.Clear();
            f1.txtSoyadi.Clear();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SiparisGuncelle f2 = new SiparisGuncelle();
            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("SEÇİLİ SİPARİŞ YOK", "UYARI");
            }

            else
            {
                f2.txtSiparisID.Text = listView1.SelectedItems[0].SubItems[0].Text;
                f2.txtSiparisAdi.Text = listView1.SelectedItems[0].SubItems[1].Text;
                f2.txtAdi.Text = listView1.SelectedItems[0].SubItems[2].Text;
                f2.txtSoyadi.Text = listView1.SelectedItems[0].SubItems[3].Text;
                f2.txtNumara.Text = listView1.SelectedItems[0].SubItems[4].Text;
                f2.txtAdres.Text = listView1.SelectedItems[0].SubItems[5].Text;
                f2.txtEposta.Text = listView1.SelectedItems[0].SubItems[6].Text;

                DialogResult sonuc = f2.ShowDialog();

                if (sonuc == DialogResult.OK)
                {
                    listele();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (
            MessageBox.Show("Çıkmak istediğinizden emin misiniz???", "ÇIKIŞ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes

                )
                Application.Exit();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
