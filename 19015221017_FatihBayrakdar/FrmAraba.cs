using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _19015221017_FatihBayrakdar
{
    public partial class FrmAraba : Form
    {
        public FrmAraba()
        {
            InitializeComponent();
        }

        string resimlinki;
        private void ResimSeç(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                resimlinki = openFileDialog1.FileName;
            }
        }

        private void ResimKaldır(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image = null;
            resimlinki = null;
        }

        public Araba ArabaBilgi { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            ArabaBilgi = new Araba() { IlanNo = Guid.NewGuid() };

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("İl Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("İlçe Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Ad Soyad Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }
            if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("KM Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                return;
            }
            if (comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Araba Markası Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
            }
            if (comboBox2.Text.Trim() == "")
            {
                MessageBox.Show("Araba Serisi Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Focus();
                return;
            }
            if (comboBox3.Text.Trim() == "")
            {
                MessageBox.Show("Araba Modeli Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox3.Focus();
                return;
            }
            if (comboBox4.Text.Trim() == "")
            {
                MessageBox.Show("Araç Rengi Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox4.Focus();
                return;
            }
            if (comboBox5.Text.Trim() == "")
            {
                MessageBox.Show("Araba Vitesi Boş Bırakılamaz.", "Araba Kayıt Edilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox5.Focus();
                return;
            }

            ArabaBilgileriniGüncelle();

            DialogResult = DialogResult.OK;
        }

       
        private void ArabaBilgileriniGüncelle()
        {
            ArabaBilgi.AdSoyad = textBox2.Text;
            ArabaBilgi.Marka = comboBox1.SelectedItem.ToString();
            ArabaBilgi.Seri = comboBox2.SelectedItem.ToString();
            ArabaBilgi.Model = comboBox3.SelectedItem.ToString();
            ArabaBilgi.İl = textBox1.Text;
            ArabaBilgi.İlçe = textBox3.Text;
            ArabaBilgi.KM = textBox4.Text;
            ArabaBilgi.Renk = comboBox4.SelectedItem.ToString();
            ArabaBilgi.Vites = comboBox5.SelectedItem.ToString();
            ArabaBilgi.ArabaYılı = dateTimePicker1.Value;
            ArabaBilgi.Resim = resimlinki;
            ArabaBilgi.ArabaFiyat = numericUpDown1.Value;
            ArabaBilgi.Bilgi = textBox5.Text;
        }
        public void ArabaBilgileriniYükle(Araba ArabaBilgi)
        {
            textBox2.Text = ArabaBilgi.AdSoyad;
            textBox1.Text = ArabaBilgi.İl;
            textBox3.Text = ArabaBilgi.İlçe;
            textBox4.Text = ArabaBilgi.KM;
            dateTimePicker1.Value = ArabaBilgi.ArabaYılı;
            numericUpDown1.Value = ArabaBilgi.ArabaFiyat;
            textBox5.Text = ArabaBilgi.Bilgi;

            if (!string.IsNullOrEmpty(ArabaBilgi.Resim))
                pictureBox1.Load(ArabaBilgi.Resim);

            foreach (var marka in ArabaMarka.MarkaListe)
                comboBox1.Items.Add(marka);

            comboBox1.SelectedItem = ArabaBilgi.Marka;
            comboBox4.SelectedItem = ArabaBilgi.Renk;
            comboBox5.SelectedItem = ArabaBilgi.Vites;

        }
        private void Yükle(object sender, EventArgs e)
        {
            foreach (var marka in ArabaMarka.MarkaListe)
                comboBox1.Items.Add(marka);
        }
    }
}
