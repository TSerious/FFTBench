using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Diagnostics;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestMathNetBase : BaseTest
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
            Fourier.Forward(data, FourierOptions.AsymmetricScaling);
            Debug.WriteLine(this + " Error = " + Helper.CalculateError(data, SignalGenerator.TestArrayFFTresult()));
            var spectrum = Helper.ComputeSpectrum(data);
            Fourier.Inverse(data, FourierOptions.Default);
            backwardResult = Helper.ToReal(data);
            Helper.Scale(ref backwardResult, scale);

            return spectrum;
        }
    }

    public class TestMathNet : TestMathNetBase
    {
        public override void Initialize(double[] data)
        {
            //Control.UseSingleThread();
            Control.UseMultiThreading();
            Control.UseManaged();

            // Gives identical results
            //Control.UseManagedReference();

            base.Initialize(data);
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

    public class TestMathNetPlusMkl : TestMathNetBase
    {
        public override void Initialize(double[] data)
        {
            //Control.UseSingleThread();
            Control.UseMultiThreading();
            Control.UseNativeMKL();

            base.Initialize(data);
        }

        public override string ToString()
        {
            string name = "Math.NET+MKL";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}
