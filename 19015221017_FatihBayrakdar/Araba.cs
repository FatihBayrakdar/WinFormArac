using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace _19015221017_FatihBayrakdar
{
    public class Araba
    {
        public Guid IlanNo { get; set; }
        public string AdSoyad { get; set; }
        public string Marka { get; set; }
        public string Seri { get; set; }


        [XmlIgnore]
        [JsonIgnore]
        public string Model { get; set; }

        public string İl { get; set; }
        public string İlçe { get; set; }
        public string KM { get; set; }
        public string Vites { get; set; }

        public string Renk { get; set; }
        public DateTime ArabaYılı { get; set; }
        public decimal ArabaFiyat { get; set; }

        public string Bilgi { get; set; }
        public string Resim { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public string Detay
        {
            get
            {
                return
                    $"Ilan Numarası     :{IlanNo}\r\n" +
                    $"Ad Soyad          :{AdSoyad}\r\n" +
                    $"Araba Markası     :{Marka}\r\n" +
                    $"Araba Serisi      :{Seri}\r\n" +
                    $"Araba Modeli      :{Model}\r\n" +
                    $"İl                :{İl}\r\n" +
                    $"İlçe              :{İlçe}\r\n" +
                    $"KM                :{KM}" +
                    $"Vites             :{Vites}\r\n" +
                    $"Renk              :{Renk}\r\n" +
                    $"Araba Yılı        :{ArabaYılı}\r\n" +
                    $"Araba Fiyatı      :{ArabaFiyat}\r\n" +
                    $"**************************************\r\n" +
                    $"Bigi              :{Bilgi}";
            }
        }

    }
    public static class ArabaList
    {
        public static List<Araba> Arabalar = new List<Araba>();
    }

}
