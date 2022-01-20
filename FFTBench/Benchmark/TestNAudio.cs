using NAudio.Dsp;
using System;

namespace FFTBench.Benchmark
{
    public class TestNAudio : ITest
    {
        int size;
        Complex[] data;

        public bool Enabled { get; set; }

        public void Initialize(double[] data)
        {
            if (Helper.GetNextPowerOf2(data.Length) != data.Length)
            {
                throw new NotSupportedException(this + ": size is not a power of 2.");
            }

            this.data = ToComplex(data);

            size = Util.Log2(data.Length);
        }

        public void FFT(bool forward)
        {
            FastFourierTransform.FFT(forward, size, data);
        }

        public double[] Spectrum(double[] input, bool scale, out double[] backwardResult)
        {
            if (Helper.GetNextPowerOf2(input.Length) != input.Length)
            {
                throw new NotSupportedException(this + ": size is not a power of 2.");
            }

            var data = ToComplex(input);
            int size = Util.Log2(input.Length);

            FastFourierTransform.FFT(true, size, data);
            var spectrum = ComputeSpectrum(data);
            FastFourierTransform.FFT(false, size, data);
            backwardResult = ToDouble(data);

            return spectrum;
        }

        protected double[] ComputeSpectrum(Complex[] fft)
        {
            int length = fft.Length;

            var result = new double[length];

            for (int i = 0; i < length; i++)
            {
                float x = fft[i].X;
                float y = fft[i].Y;

                result[i] = Math.Sqrt(x * x + y * y);
            }

            return result;
        }

        protected Complex[] ToComplex(double[] data)
        {
            var result = new Complex[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = new Complex
                {
                    X = (float)data[i],
                    Y = 0f
                };
            }

            return result;
        }

        protected double[] ToDouble(Complex[] data)
        {
            var target = new double[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                target[i] = data[i].X;
            }

            return target;
        }

        public override string ToString()
        {
            return "NAudio";
        }
    }
}
