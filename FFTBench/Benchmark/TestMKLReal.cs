using System;
using System.Numerics;
using oneMKL.FFT.NET;

namespace FFTBench.Benchmark
{
    public class TestMKLReal : ITest
    {
        private IntPtr descriptor = new IntPtr();
        double[] input;
        double[] output;

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
                DFTI.CONFIG_VALUE.DOUBLE,
                DFTI.CONFIG_VALUE.REAL,
                1,
                data.Length);

            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(descriptor, DFTI.CONFIG_PARAM.PLACEMENT, DFTI.CONFIG_VALUE.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(descriptor, DFTI.CONFIG_PARAM.PACKED_FORMAT, DFTI.CONFIG_VALUE.PACK_FORMAT);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(descriptor);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            input = data;
            output = new double[data.Length];
        }

        public void FFT(bool forward)
        {
            if (DFTI.DftiComputeForward(descriptor, input, output) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
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
                DFTI.CONFIG_VALUE.DOUBLE,
                DFTI.CONFIG_VALUE.REAL,
                1,
                input.Length);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PLACEMENT, DFTI.CONFIG_VALUE.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PACKED_FORMAT, DFTI.CONFIG_VALUE.PACK_FORMAT);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(desc);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            double[] data1 = input;
            double[] data2 = new double[input.Length];

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute fft.");
            }

            var result = Utils.PackedRealToComplex(data2);
            var spectrum = Helper.ComputeSpectrum(result);

            res = DFTI.DftiComputeBackward(desc, data2, data1);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute fft.");
            }

            backwardResult = data1;
            Helper.Scale(ref backwardResult, scale);


            DFTI.DftiFreeDescriptor(ref desc);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "MKL (real)";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
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
