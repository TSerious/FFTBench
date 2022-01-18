using Lomont;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestLomont : ITest
    {
        int size;

        double[] copy;
        double[] data;

        LomontFFT fft = new LomontFFT();

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            int length = data.Length;

            this.data = new double[2 * length];
            this.copy = new double[2 * length];

            for (int i = 0; i < length; i++)
            {
                this.data[2 * i] = data[i];
                this.data[2 * i + 1] = 0.0;
            }

            size = Util.Log2(data.Length);
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            fft.FFT(copy, forward);
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            var fft = new LomontFFT();

            var data = Helper.ToComplex(input);

            fft.FFT(data, true);

            var spectrum = Helper.ComputeSpectrum(data);

            fft.FFT(data, false);

            Helper.ToDouble(data, input);

            return spectrum;
        }

        public override string ToString()
        {
            return "Lomont";
        }
    }
}
