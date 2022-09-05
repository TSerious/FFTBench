using System;
using System.Numerics;
using oneMKL.FFT.NET;

namespace FFTBench.Benchmark
{
    public class TestMKLReal32 : ITest
    {
        private IntPtr descriptor = new IntPtr();
        float[] input;
        float[] output;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public bool UsePackedFormat { get; set; }

        public bool UseComplexStorage { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            int res = DFTI.DftiCreateDescriptor(
                ref descriptor,
                DFTI.CONFIG_VALUE.SINGLE,
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

            res = DFTI.DftiSetValue(descriptor, DFTI.CONFIG_PARAM.PACKED_FORMAT, UsePackedFormat ? DFTI.CONFIG_VALUE.PACK_FORMAT : DFTI.CONFIG_VALUE.CCS_FORMAT);
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
            output = new float[GetOutPutLength(data.Length, this.UsePackedFormat, this.UseComplexStorage)];
        }

        public void FFT(bool forward)
        {
            if (DFTI.DftiComputeForward(descriptor, input, output) != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't run FFT.");
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
                DFTI.CONFIG_VALUE.SINGLE,
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

            res = DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.CONJUGATE_EVEN_STORAGE, UseComplexStorage ? DFTI.CONFIG_VALUE.COMPLEX_COMPLEX : DFTI.CONFIG_VALUE.COMPLEX_REAL);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            if (UseComplexStorage)
            {
                res = DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PACKED_FORMAT, DFTI.CONFIG_VALUE.CCE_FORMAT);
                if (res != DFTI.NO_ERROR)
                {
                    throw new Exception(this + ": Can't initialize");
                }
            }
            else
            {
                res = DFTI.DftiSetValue(desc, DFTI.CONFIG_PARAM.PACKED_FORMAT, UsePackedFormat ? DFTI.CONFIG_VALUE.PACK_FORMAT : DFTI.CONFIG_VALUE.CCS_FORMAT);
                if (res != DFTI.NO_ERROR)
                {
                    throw new Exception(this + ": Can't initialize");
                }
            }

            res = DFTI.DftiCommitDescriptor(desc);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't initialize");
            }

            float[] data1 = Helper.ConvertToFloat(input);
            float[] data2 = new float[GetOutPutLength(input.Length, this.UsePackedFormat, this.UseComplexStorage)];

            res = DFTI.DftiComputeForward(desc, data1, data2);
            if (res != DFTI.NO_ERROR)
            {
                throw new Exception(this + ": Can't compute fft.");
            }

            Complex[] result;
            if (UsePackedFormat && !UseComplexStorage)
            {
                result = Utils.PackedRealToComplex(data2);
            }
            else
            {
                result = ToComplex(data2);
            }

            System.Diagnostics.Debug.WriteLine(this + " Error = " + Helper.CalculateError(result, SignalGenerator.TestArrayFFTresult()));
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
            string name = "MKL (real32)";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            if (UsePackedFormat)
            {
                name += "(packed)";
            }

            if (UseComplexStorage)
            {
                name += "(complex)";
            }

            return name;
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

        public static Complex[] ToComplex(float[] data)
        {
            Complex[] complex = new Complex[data.Length >> 1];

            for (int i = 0; i < complex.Length; i++)
            {
                complex[i] = new Complex(
                    data[(i << 1)],
                    data[(i << 1) + 1]);
            }

            return complex;
        }

        public static int GetOutPutLength(int inputLength, bool packed, bool complexStorage)
        {
            if (complexStorage)
            {
                return inputLength%2 == 0
                    ? inputLength + 2
                    : inputLength + 1;
            }

            if (packed)
            {
                return inputLength;
            }

            return inputLength + 2;
        }
    }
}
