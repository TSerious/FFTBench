using mkl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FFTBench.Benchmark
{
    public class TestMKLRealInplace : ITest
    {
        private IntPtr descriptor = new IntPtr();
        double[] data;

        public bool Enabled { get; set; }

        public void FFT(bool forward)
        {
            DFTI.DftiComputeForward(descriptor, data);
        }

        public void Initialize(double[] data)
        {
            DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.DOUBLE,
                DFTI.REAL,
                1,
                data.Length);

            DFTI.DftiSetValue(descriptor, DFTI.PLACEMENT, DFTI.INPLACE);
            DFTI.DftiSetValue(descriptor, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            DFTI.DftiCommitDescriptor(descriptor);

            this.data = data;
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            IntPtr desc = new IntPtr();
            DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.DOUBLE,
                DFTI.REAL,
                1,
                input.Length);

            DFTI.DftiSetValue(desc, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            DFTI.DftiSetValue(desc, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            DFTI.DftiCommitDescriptor(desc);

            double[] data1 = new double[input.Length];
            double[] data2 = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                data1[i] = input[i];
            }

            DFTI.DftiComputeForward(desc, data1, data2);
            var result = mkl.Util.PackedRealToComplex(data2);
            var spectrum = Helper.ComputeSpectrum(ToComplex(result));

            DFTI.DftiComputeBackward(desc, data2, data1);
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = data1[i];
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
            return "MKL (real,inplace)";
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
