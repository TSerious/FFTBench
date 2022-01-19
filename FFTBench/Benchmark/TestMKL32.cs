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

        public void FFT(bool forward)
        {
            if (DFTI.DftiComputeForward(descriptor, input, output) != DFTI.NO_ERROR)
            {
                throw new NotImplementedException(this + ": Can't run fft");
            }
        }

        public void Initialize(double[] data)
        {
            DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.SINGLE,
                DFTI.COMPLEX,
                1,
                data.Length);

            DFTI.DftiSetValue(descriptor, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            DFTI.DftiCommitDescriptor(descriptor);

            input = new ComplexF[data.Length];
            output = new ComplexF[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                input[i] = new ComplexF((float)data[i], 0);
            }
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            IntPtr desc = new IntPtr();
            if (DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.SINGLE,
                DFTI.COMPLEX,
                1,
                input.Length) != DFTI.NO_ERROR)
            {
                return input;
            }

            DFTI.DftiSetValue(desc, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            if(DFTI.DftiCommitDescriptor(desc) != DFTI.NO_ERROR)
            {
                return input;
            }

            Complex[] data1 = new Complex[input.Length];
            Complex[] data2 = new Complex[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                data1[i] = new Complex(input[i],0);
            }

            if(DFTI.DftiComputeForward(desc, data1, data2) != DFTI.NO_ERROR)
            {
                return input;
            }
            
            var spectrum = Helper.ComputeSpectrum(ToComplex(data2));

            DFTI.DftiComputeBackward(desc, data2, data1);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = data1[i].Real;
            }

            if (scale)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    input[i] /= input.Length;
                }
            }

            DFTI.DftiFreeDescriptor(ref desc);

            return spectrum;
        }

        public override string ToString()
        {
            return "MKL32";
        }

        public static double[] ToComplex(Complex[] data)
        {
            double[] complex = new double[data.Length << 1];

            for (int i = 0; i < data.Length; i++)
            {
                complex[i * 2] = data[i].Real;
                complex[(i * 2) + 1] = data[i].Imaginary;
            }

            return complex;
        }
    }
}
