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
        public override void FFT(bool forward)
        {
            //Control.UseNativeMKL();
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

        public override double[] Spectrum(double[] input, bool scale)
        {
            var data = ToComplex(input);

            Fourier.Radix2Forward(data, FourierOptions.Default);

            var spectrum = ComputeSpectrum(data);

            Fourier.Radix2Inverse(data, FourierOptions.Default);

            ToDouble(data, input);

            return spectrum;
        }

        public override string ToString()
        {
            return "Math.NET";
        }
    }
}
