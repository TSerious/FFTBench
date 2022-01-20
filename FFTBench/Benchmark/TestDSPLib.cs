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

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            this.copy = (double[])data.Clone();
            this.data = new double[data.Length];

            fft.Initialize((uint)data.Length);
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            var result = fft.Execute(copy);
            
            dummy = result.Length;
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            var fft = new FFT();
            fft.Initialize((uint)input.Length);
            var result = fft.Execute(input);
            var spectrum = DSP.ConvertComplex.ToMagnitude(result);
            backwardResult = new double[input.Length];

            return spectrum;
        }

        public override string ToString()
        {
            string name = "DSPLib";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}
