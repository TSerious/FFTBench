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
            int res = DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.SINGLE,
                DFTI.REAL,
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

            res = DFTI.DftiSetValue(descriptor, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(descriptor);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            input = Helper.ConvertToFloat(data);
            output = new float[data.Length];
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            IntPtr desc = new IntPtr();
            int res = DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.SINGLE,
                DFTI.REAL,
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

            res = DFTI.DftiSetValue(desc, DFTI.PACKED_FORMAT, DFTI.PACK_FORMAT);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(desc);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            float[] data1 = Helper.ConvertToFloat(input);
            float[] data2 = new float[input.Length];

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute fft.");
            }

            var result = mkl.Util.PackedRealToComplex(data2);
            var spectrum = Helper.ComputeSpectrum(result);

            res = DFTI.DftiComputeBackward(desc, data2, data1);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute inverse fft.");
            }

            backwardResult = Helper.ConvertToDouble(data1);
            Helper.Scale(ref backwardResult, scale);

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
