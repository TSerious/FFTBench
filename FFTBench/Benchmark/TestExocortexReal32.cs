using Exocortex.DSP;
using System;
using System.Numerics;

namespace FFTBench.Benchmark
{
    public class TestExocortexReal32 : ITest
    {
        float[] data;
        float[] copy;

        public bool Enabled { get; set; }

        public bool StretchInput { get; set; }

        public void Initialize(double[] data)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref data);
            }

            this.copy = Helper.ConvertToFloat(data);
            this.data = new float[data.Length];
        }

        public void FFT(bool forward)
        {
            data.CopyTo(copy, 0);

            Fourier.RFFT(copy, copy.Length, forward ?
                FourierDirection.Forward :
                FourierDirection.Backward);
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (StretchInput)
            {
                Helper.StretchToNextPowerOf2(ref input);
            }

            ToComplex(Helper.ConvertToFloat(input), out ComplexF[] data);
            Fourier.FFT(data, data.Length, FourierDirection.Forward);
            var spectrum = ComputeSpectrum(data);
            Fourier.FFT(data, data.Length, FourierDirection.Backward);
            backwardResult = ToReal(data);

            return spectrum;
        }        

        public override string ToString()
        {
            string name = "Exocortex (real32)";

            if (StretchInput)
            {
                name += "(stretched)";
            }

            return name;
        }

        public static void ToComplex(float[] data, out ComplexF[] result)
        {
            result = new ComplexF[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new ComplexF(data[i], 0);
            }
        }

        public static double[] ComputeSpectrum(ComplexF[] fft)
        {
            var result = new double[fft.Length];

            for (int i = 0; i < fft.Length; i++)
            {
                result[i] = Math.Sqrt(fft[i].Re * fft[i].Re + fft[i].Im * fft[i].Im);
            }

            return result;
        }

        public static double[] ToReal(ComplexF[] data)
        {
            double[] target = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                target[i] = data[i].Re;
            }

            return target;
        }
    }
}
