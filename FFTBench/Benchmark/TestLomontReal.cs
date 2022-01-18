using Lomont;
using System;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestLomontReal : ITest
    {
        int size;

        double[] data;
        double[] copy;

        LomontFFT fft = new LomontFFT();

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            int length = data.Length;

            this.copy = (double[])data.Clone();
            this.data = new double[length];

            size = Util.Log2(length);
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            fft.RealFFT(copy, forward);
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            var fft = new LomontFFT();

            fft.RealFFT(input, true);

            var spectrum = ComputeSpectrum(input);

            fft.RealFFT(input, false);

            return spectrum;
        }

        private double[] ComputeSpectrum(double[] fft)
        {
            int length = fft.Length / 2;

            var result = new double[length];

            for (int i = 0; i < length; i++)
            {
                double x = fft[2 * i];
                double y = fft[2 * i + 1];

                result[i] = Math.Sqrt(x * x + y * y);
            }

            return result;
        }

        public override string ToString()
        {
            return "Lomont (real)";
        }
    }
}
