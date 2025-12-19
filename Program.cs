using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        
        // ===== ARAÇ + SABİT REZERVASYON 1 =====
        plakaList.Add("35ABC123");
        markaList.Add("BMW");
        modelList.Add("M5");
        gunlukFiyatList.Add(3500);
        aracTipList.Add("Sedan");
        
        
        rezervasyonPlaka.Add("35ABC123");
        musteriAd.Add("Elif");
        baslangicTarih.Add(new DateTime(2025, 7, 4));
        bitisTarih.Add(new DateTime(2025, 7, 12));
        ucretListe.Add(
            RezervasyonUcreti(
                "35ABC123",
                new DateTime(2025, 7, 4),
                new DateTime(2025, 7, 12)
            )
        );

        // ===== ARAÇ + SABİT REZERVASYON 2 =====
        plakaList.Add("34XYZ789");
        markaList.Add("Mercedes");
        modelList.Add("C200");
        gunlukFiyatList.Add(2800);
        aracTipList.Add("Sedan");
        
        
        rezervasyonPlaka.Add("34XYZ789");
        musteriAd.Add("Ceylin");
        baslangicTarih.Add(new DateTime(2025, 7, 10));
        bitisTarih.Add(new DateTime(2025, 7, 15));
        ucretListe.Add(
            RezervasyonUcreti(
                "34XYZ789",
                new DateTime(2025, 7, 10),
                new DateTime(2025, 7, 15)
            )
        );

        // ===== ARAÇ + SABİT REZERVASYON 3 =====
        plakaList.Add("06MOTO45");
        markaList.Add("Honda");
        modelList.Add("CBR600");
        gunlukFiyatList.Add(800);
        aracTipList.Add("Motor");
        
        
        rezervasyonPlaka.Add("06MOTO45");
        musteriAd.Add("Zeynep");
        baslangicTarih.Add(new DateTime(2025, 7, 5));
        bitisTarih.Add(new DateTime(2025, 7, 8));
        ucretListe.Add(
            RezervasyonUcreti(
                "06MOTO45",
                new DateTime(2025, 7, 5),
                new DateTime(2025, 7, 8)
            )
        );
        
        for (int i = 0; i < plakaList.Count; i++)
        {
            Console.WriteLine("Plaka: " + plakaList[i]);
            Console.WriteLine("Marka: " + markaList[i]);
            Console.WriteLine("Model: " + modelList[i]);
            Console.WriteLine("Günlük Fiyat: " + gunlukFiyatList[i] + " TL");
            Console.WriteLine("Araç Tipi: " + aracTipList[i]);
            Console.WriteLine("----------------------");
        }
        
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
            Console.WriteLine("0- Çıkış");
            Console.Write("Seçim: ");
            
            if (!int.TryParse(Console.ReadLine(), out secim))
            {
                Console.WriteLine("Lütfen geçerli bir sayı giriniz!");
                continue;
            }
            
            switch (secim)
            {
                case 1: AraclariListele(); break;
                case 2: musaitAraclariGoster(); break;
                case 3: AracEkle(); break;
                case 4: RezervasyonEkle(); break;
                case 5: RezervasyonIptal(); break;
                case 6: MusteriRezervasyonlari(); break;
                case 7: EnCokKiralananArac(); break;
                case 8: ToplamGeliriGoster(); break;
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
            Console.WriteLine($"Plaka: {plakaList[i]}");
            Console.WriteLine($"Marka: {markaList[i]}");
            Console.WriteLine($"Model: {modelList[i]}");
            Console.WriteLine($"Günlük Fiyat: {gunlukFiyatList[i]} TL");
            Console.WriteLine($"Araç Tipi: {aracTipList[i]}");
            Console.WriteLine("-------------------");
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
        
        if (plakaList.Contains(plaka))
        {
            Console.WriteLine("Bu plaka zaten mevcut!");
            return;
        }
        
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
        
        if (!plakaList.Contains(plaka))
        {
            Console.WriteLine("Plaka bulunamadı!");
            return;
        }
        
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

    static void RezervasyonIptal()
    {
        Console.Write("Plaka: ");
        string plaka = Console.ReadLine();
        
        int index = rezervasyonPlaka.IndexOf(plaka);

        if (index == -1)
        {
            Console.WriteLine("Rezervasyon bulunamadı!!!");
            return;
        }
        
        rezervasyonPlaka.RemoveAt(index);
        musteriAd.RemoveAt(index);
        baslangicTarih.RemoveAt(index);
        bitisTarih.RemoveAt(index);
        ucretListe.RemoveAt(index);

        Console.WriteLine("Rezervasyon İptal Edilmiştir!!!");
    }

    //Girilen ada göre o müşteriye ait tüm rezervasyonları ekrana yazdırır
    static void MusteriRezervasyonlari()
    {
        Console.Write("Müşteri Adı: ");
        string musteri = Console.ReadLine();

        for (int i = 0; i < musteriAd.Count; i++)
        {
            if (musteriAd[i] == musteri)
            {
                Console.WriteLine($"{musteri} - {rezervasyonPlaka[i]} - {baslangicTarih[i]} → {bitisTarih[i]}");
            }
        }
    }

    static void EnCokKiralananArac()
    {
        Dictionary<string, int> sayac = new Dictionary<string, int>();

        //rezervasyon plakaları tek tek sayılıyor
        for (int i = 0; i < rezervasyonPlaka.Count; i++)
        {
            string p = rezervasyonPlaka[i];
            
            //plaka daha önce eklenmediyse başlangıç değeri 0 oluyor
            if (!sayac.ContainsKey(p))
            {
                sayac[p] = 0;
            }
            sayac[p]++;
        }

        //en çok kiralananı bulmak için
        string enCok = "";
        int max = 0;

        foreach (var item in sayac)
        {
            if (item.Value > max)
            {
                max = item.Value;
                enCok = item.Key;
            }
        }

        if (max == 0)
        {
            Console.WriteLine("Henüz rezervasyon yok!!");
        }
        else
        {
            Console.WriteLine($"En çok kiralanan araç: {enCok}"); //En çok kiralanan aracın plakası yazdırılır
        }
    }

    static void ToplamGeliriGoster()
    {
        double toplam = 0;

        for (int i = 0; i < ucretListe.Count; i++)
        {
            toplam += ucretListe[i];
        }

        Console.WriteLine($"Toplam Gelir: {toplam} TL");
    }
}