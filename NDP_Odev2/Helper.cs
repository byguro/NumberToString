using System;

namespace NDP_Odev2
{
    public static class Helper
    {
        public static string YaziyaCevir(decimal tutar)
        {
            var sTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için            
            var lira = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
            var yazi = "";

            string[] birler = { "", "BİR ", "İKİ ", "ÜÇ ", "DÖRT ", "BEŞ ", "ALTI ", "YEDİ ", "SEKİZ ", "DOKUZ " };
            string[] onlar = { "", "ON ", "YİRMİ ", "OTUZ ", "KIRK ", "ELLİ ", "ALTMIŞ ", "YETMİŞ ", "SEKSEN ", "DOKSAN " };
            string[] binler = { "KATRİLYON ", "TRİLYON ", "MİLYAR ", "MİLYON ", "BİN ", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            const int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
            //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            lira = lira.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.            

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                string grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ "; //yüzler                

                if (grupDegeri == "BİR YÜZ ") //biryüz düzeltiliyor.
                    grupDegeri = "YÜZ ";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler                

                if (grupDegeri != "") //binler
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BİR BİN ") //birbin düzeltiliyor.
                    grupDegeri = "BİN ";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " ";

            var yaziUzunlugu = yazi.Length;

            if (yazi.Length > yaziUzunlugu)
                yazi += ",";
            else
                yazi += "";

            return yazi;
        }

        public static int ToInt(this object nesne)
        {
            var sonuc = 0;
            try
            {
                sonuc = Convert.ToInt32(nesne);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            // ReSharper disable once UnusedVariable
            catch (Exception ex) { }
            return sonuc;
        }
    }
}
