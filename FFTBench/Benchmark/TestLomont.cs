using Lomont;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestLomont : ITest
    {
        double[] copy;
        double[] data;

        LomontFFT fft = new LomontFFT();

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            this.data = Helper.ToComplex(data);
            this.copy = new double[data.Length << 1];
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);
            fft.FFT(copy, forward);
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            var fft = new LomontFFT();
            var data = Helper.ToComplex(input);
            fft.FFT(data, true);
            var spectrum = Helper.ComputeSpectrum(data);
            fft.FFT(data, false);
            backwardResult = Helper.ToReal(data);
            Helper.Scale(ref backwardResult, scale);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "Lomont";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}
