using Accord.Math;

namespace FFTBench.Benchmark
{
    public class TestAForge : BaseTest
    {
        public override void FFT(bool forward)
        {
            data.CopyTo(copy, 0);
            
            FourierTransform.FFT(copy, forward ?
                FourierTransform.Direction.Forward :
                FourierTransform.Direction.Backward);
        }

        public override double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            Helper.ToComplex(input, out data);
            FourierTransform.FFT(data, FourierTransform.Direction.Forward);
            var spectrum = Helper.ComputeSpectrum(data);
            FourierTransform.FFT(data, FourierTransform.Direction.Backward);
            backwardResult = Helper.ToReal(data);

            return spectrum;
        }

        public override string ToString()
        {
            return "AForge";
        }
    }
}
