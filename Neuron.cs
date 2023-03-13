using System;

namespace Proje2
{
    public class Neuron
    {
        public const double OGR_KATSAYI = 0.05; //eğitim kısmında ağırlıkları değiştirdiğimizde kulandığımız constant değişken olanöğrenme katsayısı 
        public double[,] girdiler = {{6,5},{2,4},{-3,-5},{-1,-1},{1,1},{-2,7}, {-4,-2},{-6,3}}; //girdileri tutmak için nx2lik double tipinde matris
        
        //-1,1 aralığında rastgele üretilen ağırlıklar
        public double agirlik1 = (new Random().NextDouble() * 2) -1; 
        public  double agirlik2 = (new Random().NextDouble() * 2) -1;

        //bu metotda verilere istenen epok sayisi kadar öğrenme kuralı uygulanarak uygun ağırlıklar eğitiliyor
        //daha sonra istenen epok sonunda başarı değeri bulunuyor
        public double[] Egitim(double[,] girdilerMatrisi, double w1, double w2,int epokSayi)
        {

            double[] agirliklar = new double [2]; //teste gönderilmek üzere ağırlıkları tutmak ve döndürmek için double tipinde 2 uzunluklu liste

            for (int v = 0; v < epokSayi; v++)
            {

                int target = 0; //eğitim kuralı ulgularken target değerleri için değişken
                int output =0; //kural uygularken veriler uygun ağırlıklarla çarpıldıktan sonra elde edilen değer için değişken

                for (int i = 0; i < girdilerMatrisi.GetLength(0); i++)
                {
                    //sırasıyla veri setindeki her bir veri için target ve output değerleri hesaplanıyor

                    //girdilerin toplamı artı sayı ise 1, aksi halde -1e eşitleniyor
                    if (girdilerMatrisi[i, 0] + girdilerMatrisi[i, 1] > 0)
                    {
                        target = 1;
                    }
                    else if (girdilerMatrisi[i, 0] + girdilerMatrisi[i, 1] < 0)
                    {
                        target = -1;
                    }

                    //elde edilen output değeri 0.5ten küçükse -1, büyükse 1 oluyor
                    if (girdilerMatrisi[i, 0] / 10 * w1 + girdilerMatrisi[i, 1] / 10 * w2 >= 0.5)
                    {
                        output = 1;
                    }
                    else if (girdilerMatrisi[i, 0] / 10 * w1 + girdilerMatrisi[i, 1] / 10 * w2 < 0.5)
                    {
                        output = -1;
                        
                    }

                    //daha sonra öğrenme kuralı uygulanarak target ve output değerleri farklı olduğunda aşağıdakı kurala uygun şekilde aöırlıklar değişiyor
                    if (target != output)
                    {
                        w1 += OGR_KATSAYI * (target - output) * girdilerMatrisi[i, 0] / 10;
                        w2 += OGR_KATSAYI * (target - output) * girdilerMatrisi[i, 1] / 10;

                    }
                }
            }

            
            double dogruluk =0;//veri seti üzerindeki doğru değerlerin sayını tutacak değişken

            for (int i = 0; i < girdilerMatrisi.GetLength(0); i++)
            {
                //tüm epoklar bittikten(istenen epok sayi kadar)sonra değişilmiş ağırlıklar veri setine gönderilerek elde edilen doğru  veri sayısı hesaplanıyor
                
                if ( ((girdilerMatrisi[i, 0] + girdilerMatrisi[i, 1])>0 && (girdilerMatrisi[i, 0] / 10 * w1  + girdilerMatrisi[i, 1] / 10 * w2)>=0.5)
                     || ((girdilerMatrisi[i, 0] + girdilerMatrisi[i, 1])<0 && (girdilerMatrisi[i, 0] / 10 * w1  + girdilerMatrisi[i, 1] / 10 * w2)<0.5 ))
                {
                    dogruluk += 1;
                }
            }

            //doğru olan veri sayısı toplam veri sayısına bölerek doğruluk değeri hesaplanıyor
            double acc = dogruluk / girdilerMatrisi.GetLength(0)*100;
            Console.WriteLine(epokSayi +" epok sonunda veri seti üzerindeki doğruluk degeri: "+acc +"%");

            //en sonda ise eğitilmiş ağırlıklar test edilmek amaçlı listeye atanarak metotdan döndürülüyor
            agirliklar[0] = w1;
            agirliklar[1] = w2;
            return agirliklar;

        }
            

        //yöntemizin döğruluğunu öğrenmek için Test methodu
        //parametre olarak testMatrisini ve eğitilmiş ağırlıkları alıyor
        public void Test(double[,] testMat , double[] egitilmisAgirliklar)
        {

            double dogruluk = 0; //test verilerinin döğruluk sayısını tutacak değişken
            
            //gönderilen test matrisindeki değerlere belirtilen sayda epok uygulandıktan sonra eğitilmiş ağırlıkalar kullanılarak eğitim kuralı uygulanıyor
            for (int i = 0; i < testMat.GetLength(0); i++)
            {
                //eğitim kuralı uygulanıyor ve sonucu doğru olan verilerin sayısı bulunuyor
                if ( ((testMat[i, 0] + testMat[i, 1])>0 && 
                      (testMat[i, 0] / 10 * egitilmisAgirliklar[0]  + testMat[i, 1] / 10 * egitilmisAgirliklar[1])>=0.5)
                     || ((testMat[i, 0] + testMat[i, 1])<0 && 
                         (testMat[i, 0] / 10 * egitilmisAgirliklar[0]  + testMat[i, 1] / 10 * egitilmisAgirliklar[1])<0.5 ))
                {
                    dogruluk += 1; //uygun şartlar karşılanıyorsa doğruluk 1 artırılıyor
                }
            }
            
            //doğru olan veri sayısı toplam veri sayısına bölerek doğruluk değeri hesaplanıyor
            double testAcc = dogruluk / testMat.GetLength(0)*100;

            
            Console.WriteLine("Test verisinin doğruluk degeri: "+testAcc + "%");
            
            
        }
        
        
               
        
    }
}