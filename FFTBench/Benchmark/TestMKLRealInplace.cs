using System;
using oneMKL.FFT.NET;

namespace FFTBench.Benchmark
{
    public class TestMKLRealInplace : ITest
    {
        private IntPtr descriptor = new IntPtr();
        double[] data;

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
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

            res = DFTI.DftiSetValue(descriptor, DFTI.CONFIG_PARAM.PLACEMENT, DFTI.CONFIG_VALUE.INPLACE);
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

            this.data = data;
        }

        public void FFT(bool forward)
        {
            if( DFTI.DftiComputeForward(descriptor, data) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't calculate fft.");
            }
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            IntPtr desc = new IntPtr();
            int res = DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.CONFIG_VALUE.DOUBLE,
                DFTI.CONFIG_VALUE.REAL,
                1,
                input.Length);

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

            double[] data1 = new double[input.Length];
            double[] data2 = new double[input.Length];
            input.CopyTo(data1, 0);

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't run fft.");
            }

            var result = Utils.PackedRealToComplex(data2);
            var spectrum = Helper.ComputeSpectrum(result);

            res = DFTI.DftiComputeBackward(desc, data2, data1);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't run inverse fft.");
            }

            backwardResult = data1;
            Helper.Scale(ref backwardResult, scale);

            DFTI.DftiFreeDescriptor(ref desc);

            return spectrum;
        }

        public override string ToString()
        {
            return "MKL (real,inplace)";
        }
    }
}
