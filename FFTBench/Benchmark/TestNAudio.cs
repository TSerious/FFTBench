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
            int length = data.Length;

            this.data = ToComplex(data);

            size = Util.Log2(length);
        }

        public void FFT(bool forward)
        {
            NAudio.Dsp.FastFourierTransform.FFT(forward, size, data);
        }

        public double[] Spectrum(double[] input, bool scale)
        {
            var data = ToComplex(input);

            int size = Util.Log2(input.Length);

            NAudio.Dsp.FastFourierTransform.FFT(true, size, data);

            var spectrum = ComputeSpectrum(data);

            NAudio.Dsp.FastFourierTransform.FFT(false, size, data);

            ToDouble(data, input);

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
            int length = data.Length;

            var result = new Complex[length];

            Complex z;

            for (int i = 0; i < length; i++)
            {
                z = new Complex();

                z.X = (float)data[i];
                z.Y = 0f;

                result[i] = z;
            }

            return result;
        }

        protected void ToDouble(Complex[] data, double[] target)
        {
            int length = data.Length;

            for (int i = 0; i < length; i++)
            {
                target[i] = data[i].X;
            }
        }

        public override string ToString()
        {
            return "NAudio";
        }
    }
}
