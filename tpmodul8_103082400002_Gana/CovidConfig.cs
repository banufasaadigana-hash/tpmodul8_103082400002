using Newtonsoft.Json;
using System;
using System.IO;

namespace tpmodul8_103082400002
{
    public class CovidConfig
    {
        // Properties sesuai format JSON
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        // Constructor default (menggunakan nilai default)
        public CovidConfig()
        {
            satuan_suhu = "celcius";
            batas_hari_deman = 14;
            pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
            pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
        }

        // Method untuk membaca konfigurasi dari file JSON
        public static CovidConfig LoadConfig()
        {
            string fileName = "covid_config.json";

            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<CovidConfig>(json);
            }
            else
            {
                CovidConfig defaultConfig = new CovidConfig();
                defaultConfig.SaveConfig();
                return defaultConfig;
            }
        }

        // Method untuk menyimpan konfigurasi ke file JSON
        public void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText("covid_config.json", json);
        }

        // Method untuk mengubah satuan suhu
        public void UbahSatuan()
        {
            if (satuan_suhu.ToLower() == "celcius")
            {
                satuan_suhu = "fahrenheit";
                Console.WriteLine("Satuan suhu telah diubah menjadi FAHRENHEIT");
            }
            else if (satuan_suhu.ToLower() == "fahrenheit")
            {
                satuan_suhu = "celcius";
                Console.WriteLine("Satuan suhu telah diubah menjadi CELCIUS");
            }
            else
            {
                satuan_suhu = "celcius";
                Console.WriteLine("Satuan suhu tidak dikenal, diubah ke celcius");
            }

            SaveConfig();
        }

        // Method validasi suhu
        public bool IsSuhuValid(double suhu)
        {
            if (satuan_suhu.ToLower() == "celcius")
            {
                return suhu >= 36.5 && suhu <= 37.5;
            }
            else if (satuan_suhu.ToLower() == "fahrenheit")
            {
                return suhu >= 97.7 && suhu <= 99.5;
            }
            return false;
        }

        // Method validasi hari demam
        public bool IsHariDemanValid(int hari)
        {
            return hari < batas_hari_deman;
        }
    }
}