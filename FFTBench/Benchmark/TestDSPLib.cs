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
            var fft = new FFT();
            fft.Initialize((uint)input.Length);
            var result = fft.Execute(input);
            System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(Helper.Multiply(result, input.Length), SignalGenerator.TestArrayFFTresult()));
            var spectrum = DSP.ConvertComplex.ToMagnitude(result);
            backwardResult = new double[input.Length];

            return spectrum;
        }

        public override string ToString()
        {
            return "DSPLib";
        }
    }
}
