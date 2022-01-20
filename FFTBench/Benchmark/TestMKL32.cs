using mkl;
using MKL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FFTBench.Benchmark
{
    public class TestMKL32 : ITest
    {
        private IntPtr descriptor = new IntPtr();
        ComplexF[] input;
        ComplexF[] output;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            int res = DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.SINGLE,
                DFTI.COMPLEX,
                1,
                data.Length);

            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(descriptor, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(descriptor);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            ToComplex(data, out input);
            output = new ComplexF[data.Length];
        }

        public void FFT(bool forward)
        {
            if (DFTI.DftiComputeForward(descriptor, input, output) != DFTI.NO_ERROR)
            {
                throw new NotImplementedException(this + ": Can't run fft");
            }
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            IntPtr desc = new IntPtr();
            int res = DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.SINGLE,
                DFTI.COMPLEX,
                1,
                input.Length);

            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(desc, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(desc);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            ToComplex(input, out ComplexF[] data1);
            ComplexF[] data2 = new ComplexF[input.Length];

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute fft.");
            }

            var spectrum = ComputeSpectrum(data2);

            DFTI.DftiComputeBackward(desc, data2, data1);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute fft.");
            }

            backwardResult = ToReal(data1);
            Helper.Scale(ref backwardResult, scale);

            DFTI.DftiFreeDescriptor(ref desc);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "MKL32";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }

        public static void ToComplex(double[] data, out ComplexF[] result)
        {
            result = new ComplexF[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new ComplexF((float)data[i], 0);
            }
        }

        public static double[] ComputeSpectrum(ComplexF[] fft)
        {
            var result = new double[fft.Length];

            for (int i = 0; i < fft.Length; i++)
            {
                result[i] = Math.Sqrt(fft[i].Real * fft[i].Real + fft[i].Imaginary * fft[i].Imaginary);
            }

            return result;
        }

        public static double[] ToReal(ComplexF[] data)
        {
            double[] target = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                target[i] = data[i].Real;
            }

            return target;
        }
    }
}
