using System;

namespace Proje2
{
    internal class Program
    {
        
        
        //static Random random;
        public static void Main(string[] args)
        {

            Neuron neuron = new Neuron();
            double[,] girdiDegeri = neuron.girdiler;
            double agr1 = neuron.agirlik1;
            double agr2 = neuron.agirlik2;

            
            double[,] testMatrisi1 = {{-4,-8},{5,2},{-6,4},{4,-7},{5,5}};

            double[] egitilmisagirliklar = neuron.Egitim(girdiDegeri, agr1, agr2, 10);
            neuron.Test(testMatrisi1,egitilmisagirliklar);

            double[] egitilmisagirliklar1 = neuron.Egitim(girdiDegeri, agr1, agr2, 100);
            neuron.Test(testMatrisi1,egitilmisagirliklar1);

        }
    }
}