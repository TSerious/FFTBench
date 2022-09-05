using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Diagnostics;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestMathNet : TestMathNetBase
    {
        public override void Initialize(double[] data)
        {
            this.Initialize(true);
            base.Initialize(data);
        }

        public override string ToString()
        {
            string name = "Math.NET";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            if (!this.SingleThreaded)
            {
                name += "(multithreaded)";
            }

            return name;
        }
    }

    public class TestMathNetReal32 : TestMathNetBaseReal32
    {
        public override void Initialize(double[] data)
        {
            this.Initialize(true);
            base.Initialize(data);
        }

        public override string ToString()
        {
            string name = "Math.NETreal32";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            if (!this.SingleThreaded)
            {
                name += "(multithreaded)";
            }

            return name;
        }

        public override double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }


            double[] data = new double[TestMKLReal32.GetOutPutLength(input.Length, false, true)];
            input.CopyTo(data, 0);

            Fourier.ForwardReal(data, input.Length, FourierOptions.AsymmetricScaling);
            Helper.ComplexToComplex(data, out Complex[] complexData);
            Debug.WriteLine(this + " Error = " + Helper.CalculateError(complexData, SignalGenerator.TestArrayFFTresult()));
            var spectrum = Helper.ComputeSpectrum(data);
            Fourier.InverseReal(data, input.Length, FourierOptions.Default);
            backwardResult = Helper.ToReal(data);
            Helper.Scale(ref backwardResult, scale);

            return spectrum;
        }
    }

    public class TestMathNetPlusMkl : TestMathNetBase
    {
        public override void Initialize(double[] data)
        {
            this.Initialize(false);
            base.Initialize(data);
        }

        public override string ToString()
        {
            string name = "Math.NET+MKL";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            if (!this.SingleThreaded)
            {
                name += "(multithreaded)";
            }

            return name;
        }
    }

    public class TestMathNetPlusMklReal32 : TestMathNetBaseReal32
    {
        public override void Initialize(double[] data)
        {
            this.Initialize(false);
            base.Initialize(data);
        }

        public override string ToString()
        {
            string name = "Math.NET+MKLreal32";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            if (!this.SingleThreaded)
            {
                name += "(multithreaded)";
            }

            return name;
        }
    }
}
