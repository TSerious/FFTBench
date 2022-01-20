using Accord.Math;
using Accord.Math.Transforms;

namespace FFTBench.Benchmark
{
    public class TestAccord : BaseTest
    {
        public override void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            FourierTransform2.FFT(copy, forward ?
                FourierTransform.Direction.Forward :
                FourierTransform.Direction.Backward);
        }

        public override double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            Helper.ToComplex(input, out data);
            FourierTransform2.FFT(data, FourierTransform.Direction.Forward);
            var spectrum = Helper.ComputeSpectrum(data);
            FourierTransform2.FFT(data, FourierTransform.Direction.Backward);
            backwardResult = Helper.ToReal(data);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "Accord";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}
