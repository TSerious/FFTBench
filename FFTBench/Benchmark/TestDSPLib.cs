using DSPLib;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestDSPLib : ITest
    {
        int dummy;

        double[] copy;
        double[] data;

        FFT fft = new FFT();

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            int length = data.Length;

            this.copy = (double[])data.Clone();
            this.data = new double[length];

            fft.Initialize((uint)length);
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            var result = fft.Execute(copy);
            
            dummy = result.Length;
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            var fft = new FFT();

            fft.Initialize((uint)input.Length);

            var result = fft.Execute(input);

            var spectrum = DSP.ConvertComplex.ToMagnitude(result);

            return spectrum;
        }

        public override string ToString()
        {
            return "DSPLib";
        }
    }
}
