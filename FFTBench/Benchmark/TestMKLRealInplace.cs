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

        public void Initialize(double[] data)
        {
            int res = DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.DOUBLE,
                DFTI.REAL,
                1,
                data.Length);

            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(descriptor, DFTI.PLACEMENT, DFTI.INPLACE);
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
                DFTI.DOUBLE,
                DFTI.REAL,
                1,
                input.Length);

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

            double[] data1 = new double[input.Length];
            double[] data2 = new double[input.Length];
            input.CopyTo(data1, 0);

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't run fft.");
            }

            var result = mkl.Util.PackedRealToComplex(data2);
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
