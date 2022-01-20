using Exocortex.DSP;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestExocortex : BaseTest
    {
        public override void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            Fourier.FFT(copy, copy.Length, forward ?
                FourierDirection.Forward :
                FourierDirection.Backward);
        }

        public override double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            Helper.ToComplex(input, out data);

            Fourier.FFT(data, data.Length, FourierDirection.Forward);
            var spectrum = Helper.ComputeSpectrum(data);
            Fourier.FFT(data, data.Length, FourierDirection.Backward);
            backwardResult = Helper.ToReal(data);

            return spectrum;
        }

        public override string ToString()
        {
            return "Exocortex";
        }
    }
}
