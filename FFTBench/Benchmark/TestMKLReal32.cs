using mkl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FFTBench.Benchmark
{
    public class TestMKLReal32 : ITest
    {
        private IntPtr descriptor = new IntPtr();
        float[] input;
        float[] output;

        public bool Enabled { get; set; }

        public void FFT(bool forward)
        {
            DFTI.DftiComputeForward(descriptor, input, output);
        }

        public void Initialize(double[] data)
        {
            DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.SINGLE,
                DFTI.REAL,
                1,
                data.Length);

            DFTI.DftiSetValue(descriptor, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            DFTI.DftiSetValue(descriptor, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            DFTI.DftiCommitDescriptor(descriptor);

            input = new float[data.Length];
            output = new float[data.Length];

            for (int i = 0; i<data.Length; i++)
            {
                input[i] = (float)data[i];
            }
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            IntPtr desc = new IntPtr();
            DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.SINGLE,
                DFTI.REAL,
                1,
                input.Length);

            DFTI.DftiSetValue(desc, DFTI.PLACEMENT, DFTI.NOT_INPLACE);
            DFTI.DftiSetValue(desc, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            DFTI.DftiCommitDescriptor(desc);

            float[] data1 = new float[input.Length];
            float[] data2 = new float[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                data1[i] = (float)input[i];
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
            return "MKL (real32)";
        }

        public static float[] ToComplex(Complex[] data)
        {
            float[] complex = new float[data.Length << 1];

            for (int i = 0; i < data.Length; i++)
            {
                complex[i * 2] = (float)data[i].Real;
                complex[(i * 2) + 1] = (float)data[i].Imaginary;
            }

            return complex;
        }
    }
}
