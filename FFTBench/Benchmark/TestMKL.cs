using System;
using System.Numerics;
using oneMKL.FFT.NET;

namespace FFTBench.Benchmark
{
    public class TestMKL : ITest
    {
        private IntPtr descriptor = new IntPtr();
        Complex[] input;
        Complex[] output;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            int res = DFTI.DftiCreateDescriptor(ref descriptor, DFTI.CONFIG_VALUE.DOUBLE, DFTI.CONFIG_VALUE.COMPLEX, 1, data.Length);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiSetValue(descriptor, DFTI.CONFIG_PARAM.PLACEMENT, DFTI.CONFIG_VALUE.NOT_INPLACE);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            res = DFTI.DftiCommitDescriptor(descriptor);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't commit desciptor.");
            }

            Helper.ToComplex(data, out input);
            output = new Complex[data.Length];
        }

        public void FFT(bool forward)
        {
            if (DFTI.DftiComputeForward(descriptor, input, output) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't run fft");
            }
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            IntPtr desc = new IntPtr();
            if (DFTI.DftiCreateDescriptor(
                ref desc,
                DFTI.CONFIG_VALUE.DOUBLE,
                DFTI.CONFIG_VALUE.COMPLEX,
                1,
                input.Length) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            if( DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PLACEMENT, DFTI.CONFIG_VALUE.NOT_INPLACE) != DFTI.NO_ERROR ||
                DFTI.DftiCommitDescriptor(desc) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize descriptor for spectrum.");
            }

            Helper.ToComplex(input, out Complex[] data1);
            Complex[] data2 = new Complex[input.Length];

            if(DFTI.DftiComputeForward(desc, data1, data2) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't run fft");
            }

            System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(data2, SignalGenerator.TestArrayFFTresult()));
            var spectrum = Helper.ComputeSpectrum(data2);

            DFTI.DftiComputeBackward(desc, data2, data1);
            backwardResult = Helper.ToReal(data1);
            Helper.Scale(ref backwardResult, scale);

            DFTI.DftiFreeDescriptor(ref desc);

            return spectrum;
        }

        public override string ToString()
        {
            string name = "MKL";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }
    }
}
