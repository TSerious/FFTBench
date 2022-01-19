using mkl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FFTBench.Benchmark
{
    public class TestMKL : ITest
    {
        private IntPtr descriptor = new IntPtr();
        double[] input;
        double[] output;

        public bool Enabled { get; set; }

        public void FFT(bool forward)
        {
            DFTI.DftiComputeForward(descriptor, input, output);
        }

        public void Initialize(double[] data)
        {
            DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.DOUBLE,
                DFTI.REAL,
                1,
                data.Length);

            DFTI.DftiSetValue(descriptor, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            DFTI.DftiSetValue(descriptor, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            DFTI.DftiCommitDescriptor(descriptor);

            input = data;
            output = new double[data.Length];
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            IntPtr desc = new IntPtr();
            if (DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.DOUBLE,
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
            return "MKL";
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
