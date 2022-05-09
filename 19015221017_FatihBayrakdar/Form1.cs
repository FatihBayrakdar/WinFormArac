using BenchmarkDotNet.Exporters.Csv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace _19015221017_FatihBayrakdar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ArabaEkle(object sender, EventArgs e)
        {
            FrmAraba form = new FrmAraba() { Text = "Araba Ekle" };

            if (form.ShowDialog() == DialogResult.OK)
            {
                ArabaList.Arabalar.Add(form.ArabaBilgi);

                ListeyiGüncelle();
            }
        }

        private void ListeyiGüncelle()
        {
            listView1.Items.Clear();
            foreach (var ArabaBilgi in ArabaList.Arabalar)
            {
                ArabaEkle(ArabaBilgi);
            }
        }
        private void ArabaEkle(Araba ArabaBilgi)
        {
            ListViewItem k = new ListViewItem(new string[]
            {
                ArabaBilgi.AdSoyad,
                ArabaBilgi.Marka,
                ArabaBilgi.İl,                
                ArabaBilgi.İlçe,
                ArabaBilgi.ArabaFiyat.ToString("C"),
        });

            k.Tag = ArabaBilgi;
            k.ToolTipText = ArabaBilgi.Detay;

            listView1.Items.Add(k);
        }

        private void ArabaGüncelle(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            Araba ArabaBilgi = listView1.SelectedItems[0].Tag as Araba;
            FrmAraba form = new FrmAraba()
            {
                Text = "Araba Güncelle",
            };
            form.ArabaBilgileriniYükle(ArabaBilgi);

            if (form.ShowDialog() == DialogResult.OK)
            {
                Araba p = ArabaList.Arabalar.Find(x => x.IlanNo == ArabaBilgi.IlanNo);
                p.AdSoyad = form.ArabaBilgi.AdSoyad;
                p.Marka = form.ArabaBilgi.Marka;
                p.Seri = form.ArabaBilgi.Seri;
                p.Model = form.ArabaBilgi.Model;
                p.İl = form.ArabaBilgi.İl;
                p.İlçe = form.ArabaBilgi.İlçe;
                p.KM = form.ArabaBilgi.KM;
                p.Renk = form.ArabaBilgi.Renk;
                p.Vites = form.ArabaBilgi.Vites;
                p.ArabaYılı = form.ArabaBilgi.ArabaYılı;
                p.Resim = form.ArabaBilgi.Resim;
                p.Bilgi = form.ArabaBilgi.Bilgi;
                p.ArabaFiyat = form.ArabaBilgi.ArabaFiyat;

                ListeyiGüncelle();
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                pictureBox1.Image = null;
                textBox1.Text = "";
                return;
            }

            Araba ArabaBilgi = listView1.SelectedItems[0].Tag as Araba;
            if (!string.IsNullOrEmpty(ArabaBilgi.Resim))
                pictureBox1.Load(ArabaBilgi.Resim);
            textBox1.Text = ArabaBilgi.Detay;
        }

        private void JsonKaydet(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = saveFileDialog1.FileName;

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(ArabaList.Arabalar);
                File.WriteAllText(dosya, json);
            }
        }

        private void JsonAç(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = openFileDialog1.FileName;

                string json = File.ReadAllText(dosya);
                ArabaList.Arabalar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Araba>>(json);

                ListeyiGüncelle();
            }
        }

        private void XmlKaydet(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = saveFileDialog1.FileName;

                XmlSerializer xml = new XmlSerializer(typeof(List<Araba>));
                StreamWriter sw = new StreamWriter(dosya);
                xml.Serialize(sw, ArabaList.Arabalar);

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        private void XmlAç(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = openFileDialog1.FileName;

                XmlSerializer xml = new XmlSerializer(typeof(List<Araba>));
                StreamReader sr = new StreamReader(dosya);
                ArabaList.Arabalar = (List<Araba>)xml.Deserialize(sr);

                sr.Close();
                sr.Dispose();

                ListeyiGüncelle();
            }
        }

        private void CsvAç(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = openFileDialog1.FileName;

                StreamReader sr = new StreamReader(dosya);
                CsvHelper.CsvReader csv = new CsvHelper.CsvReader(sr, System.Globalization.CultureInfo.CurrentCulture);
                ArabaList.Arabalar = csv.GetRecords<Araba>().ToList();

                sr.Close();
                sr.Dispose();

                ListeyiGüncelle();

            }
        }

        private void CsvKaydet(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = saveFileDialog1.FileName;

                StreamWriter sw = new StreamWriter(dosya);
                CsvHelper.CsvWriter csv = new CsvHelper.CsvWriter(sw, System.Globalization.CultureInfo.CurrentCulture);
                csv.WriteRecords(ArabaList.Arabalar);

                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        private void BinaryKaydet(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = saveFileDialog1.FileName;
                Stream fl = File.Open(dosya, FileMode.Create);
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(fl, ArabaList.Arabalar);

                fl.Flush();
                fl.Close();
                fl.Dispose();
            }
        }

        private void BinaryAç(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosya = openFileDialog1.FileName;

                Stream fl = File.Open(dosya, FileMode.Open);

                IFormatter frm = new BinaryFormatter();
                ArabaList.Arabalar = (List<Araba>)frm.Deserialize(fl);

                fl.Close();
                fl.Dispose();

                ListeyiGüncelle();

            }
        }

        private void ArabaSil_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            if (MessageBox.Show(
                "Silinsin mi",
                "Silmeyi onayla",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) != DialogResult.OK)
                return;

            Araba ArabaBilgi = listView1.SelectedItems[0].Tag as Araba;
            Araba p = ArabaList.Arabalar.Find(x => x.IlanNo == ArabaBilgi.IlanNo);
            ArabaList.Arabalar.Remove(p);

            ListeyiGüncelle();
        }

    }
}
