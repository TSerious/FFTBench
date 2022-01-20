using Lomont;
using System;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestLomontReal : ITest
    {
        double[] data;
        double[] copy;

        LomontFFT fft = new LomontFFT();

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            this.data = (double[])data.Clone();
            this.copy = new double[data.Length];

            if (Helper.GetNextPowerOf2(data.Length) != data.Length)
            {
                throw new NotImplementedException(this + ": Size must be a power of 2.");
            }
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);
            fft.RealFFT(copy, forward);
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (Helper.GetNextPowerOf2(input.Length) != input.Length)
            {
                throw new NotImplementedException(this + ": Size must be a power of 2.");
            }

            var fft = new LomontFFT();
            fft.RealFFT(input, true);
            var spectrum = ComputeSpectrum(input);
            fft.RealFFT(input, false);
            backwardResult = Helper.ToReal(input);
            Helper.Scale(ref backwardResult, scale);

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
