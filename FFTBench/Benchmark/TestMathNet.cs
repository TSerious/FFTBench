using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Diagnostics;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestMathNet : BaseTest
    {
        public bool StretchInput { get; set; }

        public override void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            if (forward)
            {
                Fourier.Forward(copy, FourierOptions.Default);
            }
            else
            {
                Fourier.Inverse(copy, FourierOptions.Default);
            }
        }

        public override double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            Helper.ToComplex(input, out Complex[] data);
            Fourier.Forward(data, FourierOptions.Default);
            var spectrum = Helper.ComputeSpectrum(data);
            Fourier.Inverse(data, FourierOptions.Default);
            backwardResult = Helper.ToReal(data);
            Helper.Scale(ref backwardResult, scale);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "Math.NET";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}
