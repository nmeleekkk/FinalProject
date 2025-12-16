using System;
using System.Collections.Generic;
using System.Data;
using System.Net;

class Program
{
    static List<string> plakaList = new List<string>();
    static List<string> markaList = new List<string>();
    static List<string> modelList = new List<string>();
    static List<double> gunlukFiyatList = new List<double>();
    static List<string> aracTipList = new List<string>();

    static List<string> musteriAd = new List<string>();
    static List<string> rezervasyonPlaka = new List<string>();
    static List<DateTime> baslangicTarih = new List<DateTime>();
    static List<DateTime> bitisTarih = new List<DateTime>();
    static List<double> ucretListe = new List<double>();
    
    static void Main(string[] args)
    {
        Console.WriteLine("Araç Kiralama Sistemine Hoş Geldiniz!");
        
        plakaList.Add("35ABC123");
        markaList.Add("BMW");
        modelList.Add("M5");
        gunlukFiyatList.Add(3500);
        aracTipList.Add("Sedan");

        plakaList.Add("34XYZ789");
        markaList.Add("Mercedes");
        modelList.Add("C200");
        gunlukFiyatList.Add(2800);
        aracTipList.Add("Sedan");

        plakaList.Add("06MOTO45");
        markaList.Add("Honda");
        modelList.Add("CBR600");
        gunlukFiyatList.Add(800);
        aracTipList.Add("Motor");
        
        int secim;

        while (true)
        {
            Console.WriteLine("\n--- MENÜ İŞLEMLERİ ---\n");
            Console.WriteLine("1- Araçları Listele");
            Console.WriteLine("2- Müsait Araçları Göster");
            Console.WriteLine("3- Araç Ekle");
            Console.WriteLine("4- Rezervasyon Yap");
            Console.WriteLine("5- Rezervasyon İptal Et");
            Console.WriteLine("6- Belirli Müşterinin Rezervasyonunu Göster");
            Console.WriteLine("7- En Çok Kiralanan Aracı Göster");
            Console.WriteLine("8- Toplam Geliri Göster");
            Console.WriteLine("9- Çıkış");
            Console.Write("Seçim: ");
            
            secim = int.Parse(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    AraclariListele();
                    break;

                case 2:
                    // Müsait Araçları Göster
                    break;

                case 3:
                    // Araç Ekle
                    break;

                case 4:
                    // Rezervasyon Yap
                    break;

                case 5:
                    // Rezervasyon İptal Et
                    break;

                case 6:
                    // Müşteri Rezervasyonları
                    break;

                case 7:
                    // En çok kiralanan araç
                    break;

                case 8:
                    // Toplam gelir
                    break;

                case 0:
                    Console.WriteLine("Çıkış yapılıyor...");
                    return; // programdan çıkış

                default:
                    Console.WriteLine("Hatalı seçim, tekrar deneyin.");
                    break;
            }
        }
    }
    
    static void AraclariListele()
    {
        if (plakaList.Count == 0)
        {
            Console.WriteLine("Araç bulunamamaktadır.");
            return; //Hiç araç yoksa döngüye girip boş yere çalışmasın
        }

        for (int i = 0; i < plakaList.Count; i++)
        {
            Console.WriteLine(plakaList[i]);
            Console.WriteLine(markaList[i]);
            Console.WriteLine(modelList[i]);
            Console.WriteLine(gunlukFiyatList[i]);
            Console.WriteLine(aracTipList[i]);
            Console.WriteLine("-------------------------");
        }
    }

    static bool AracMusaitMi(string plaka, DateTime baslangic, DateTime bitis)
    {
        for (int i = 0; i < rezervasyonPlaka.Count; i++)
        {
            if (rezervasyonPlaka[i] == plaka)
            {
                DateTime eskiBaslangicTarihi = baslangicTarih[i];
                DateTime eskiBitisTarihi = bitisTarih[i];
                
                //Rezervasyonlar çakışıyor mu kontrol ediliyor
                if (baslangic <= eskiBitisTarihi && bitis >= eskiBaslangicTarihi)
                {
                    return false; //Çakışma VAR -> Müsait değil
                }
            }
        }
        return true; //Çakışma YOK -> Müsait
    }

    static List<string> MusaitAraclariGetir(DateTime baslangic, DateTime bitis)
    {
        List<string> musaitAraclar = new List<string>();

        for (int i = 0; i < plakaList.Count; i++)
        {
            string plaka = plakaList[i];
            if (AracMusaitMi(plaka, baslangic, bitis))
            {
                musaitAraclar.Add(plaka);
            }
        }
        return musaitAraclar;
    }

    static void musaitAraclariGoster()
    {
        Console.Write("Başlangıç Tarihi (yyyy-aa-gg): ");
        DateTime baslangicTarihi = DateTime.Parse(Console.ReadLine());
        
        Console.Write("Bitiş Tarihi (yyyy-aa-gg): ");
        DateTime bitisTarihi = DateTime.Parse(Console.ReadLine());
        
        var musaitAraclar = MusaitAraclariGetir(baslangicTarihi, bitisTarihi);

        if (musaitAraclar.Count == 0)
        {
            Console.WriteLine("Müsait bir araç bulunamadı!!!");
            return;
        }

        Console.WriteLine("\nMüsait araçlar: ");
        
        //Listedeki elemanları tek tek dolaşıp ekrana yazdırır
        foreach (var p in musaitAraclar)
            Console.WriteLine(p);
    }

    static void AracEkle()
    {
        Console.Write("Plaka: ");
        string plaka = Console.ReadLine();
        
        Console.Write("Marka: ");
        string marka = Console.ReadLine();
        
        Console.Write("Model: ");
        string model = Console.ReadLine();
        
        Console.Write("Günlük Fiyat: ");
        double fiyat = double.Parse(Console.ReadLine());
        
        Console.Write("Araç Tipi: ");
        string tip = Console.ReadLine();
        
        plakaList.Add(plaka);
        markaList.Add(marka);
        modelList.Add(model);
        gunlukFiyatList.Add(fiyat);
        aracTipList.Add(tip);

        Console.WriteLine("Araç listeye başarıyla eklendi :)");
    }

    /*kiralama tarihleri arasındaki gün sayısını alır ve 
    aracın günlük fiyatı ile çarparak toplam ücreti hesaplar*/
    static double RezervasyonUcreti(string plaka, DateTime baslangic, DateTime bitis)
    {
        int index = plakaList.IndexOf(plaka);
        double fiyat = gunlukFiyatList[index];
        int gun = (bitis - baslangic).Days + 1; // iki tarih arasındaki farkı vererek gün sayısına çevirir
        return fiyat * gun; 
    }
    
    static void RezervasyonEkle()
    {
        Console.Write("Müşteri adı: ");
        string musteri = Console.ReadLine();
        
        Console.Write("Plaka: ");
        string plaka = Console.ReadLine();
        
        Console.Write("Başlangıç Tarihi (yyyy-aa-gg): ");
        DateTime baslangic = DateTime.Parse(Console.ReadLine());
        
        Console.Write("Bitiş Tarihi (yyyy-aa-gg): ");
        DateTime bitis = DateTime.Parse(Console.ReadLine());

        if (!AracMusaitMi(plaka, baslangic, bitis))
        {
            Console.WriteLine("Araç müsait değil!!");
            return;
        }
        
        rezervasyonPlaka.Add(plaka);
        musteriAd.Add(musteri);
        baslangicTarih.Add(baslangic);
        bitisTarih.Add(bitis);
        double ucret = RezervasyonUcreti(plaka, baslangic, bitis);
        ucretListe.Add(ucret);

        Console.WriteLine("Rezervasyon yapıldı!!");
    }
}